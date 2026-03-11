using Blazored.LocalStorage;
using FishCoinBlazorApp.Services.Models;
namespace FishCoinBlazorApp.Services;
public class CartService
{
    private readonly ILocalStorageService _localStorage;
    // ინახავს პროდუქტს და მის რაოდენობას
    public List<CartItemModel> CartItems { get; private set; } = new();
    public int CartCount => CartItems.Sum(i => i.Quantity);
    public decimal TotalPrice => CartItems.Sum(i =>
        (i.Product.DiscountPrice.HasValue ? i.Product.DiscountPrice.Value : i.Product.Price) * i.Quantity);
    public event Action OnChange;

    public CartService(ILocalStorageService localStorage) => _localStorage = localStorage;

    public async Task AddToCart(ProductDetailModel product, int quantity = 1)
    {
        var existingItem = CartItems.FirstOrDefault(x => x.Product.Id == product.Id);
        if (existingItem != null)
            existingItem.Quantity += quantity;
        else
            CartItems.Add(new CartItemModel { Product = product, Quantity = quantity });

        await SaveToLocalStorage();
        NotifyStateChanged();
    }

    public async Task LoadCart()
    {
        var savedItems = await _localStorage.GetItemAsync<List<CartItemModel>>("cartItems");
        if (savedItems != null) CartItems = savedItems;
        NotifyStateChanged();
    }

    public async Task UpdateQuantity(int productId, int change)
    {
        var item = CartItems.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            item.Quantity += change;

            // თუ რაოდენობა 1-ზე ნაკლები გახდა, საერთოდ წავშალოთ
            if (item.Quantity <= 0)
            {
                CartItems.Remove(item);
            }

            await SaveCart();
            NotifyStateChanged();
        }
    }

    public async Task RemoveFromCart(int productId)
    {
        var item = CartItems.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            CartItems.Remove(item);
            await SaveCart();
            NotifyStateChanged();
        }
    }

    public async Task ClearCart()
    {
        CartItems.Clear();
        await SaveCart();
        NotifyStateChanged();
    }

    private async Task SaveCart()
    {
        await _localStorage.SetItemAsync("cartItems", CartItems);
    }

    public async Task RefreshCartItems(ProductService productService)
    {
        if (CartItems == null || !CartItems.Any()) return;

        bool isChanged = false;

        // ToList() საჭიროა, რომ ციკლის დროს ელემენტის წაშლამ შეცდომა არ გამოიწვიოს
        foreach (var item in CartItems.ToList())
        {
            // ბაზიდან ვიღებთ აქტუალურ მონაცემებს Id-ით
            var dbProduct = await productService.GetProductById(item.Product.Id);

            if (dbProduct == null)
            {
                // თუ პროდუქტი ბაზიდან წაიშალა
                CartItems.Remove(item);
                isChanged = true;
                continue;
            }

            // 1. მარაგის შემოწმება (StockQuantity)
            if (dbProduct.StockQuantity <= 0)
            {
                CartItems.Remove(item);
                isChanged = true;
                continue;
            }
            else if (item.Quantity > dbProduct.StockQuantity)
            {
                // თუ იუზერს მეტი უდევს ვიდრე მარაგშია, ჩამოვუყვანოთ მაქსიმუმზე
                item.Quantity = dbProduct.StockQuantity;
                isChanged = true;
            }

            // 2. ფასის და ფასდაკლების სინქრონიზაცია
            // გამოვთვალოთ ახალი ფასდაკლებული ფასი ბაზის მონაცემებით
            decimal freshDiscountPrice = dbProduct.Price;
            if (dbProduct.DiscountPrecentage.HasValue && dbProduct.DiscountPrecentage > 0)
            {
                freshDiscountPrice = dbProduct.Price - (dbProduct.Price * dbProduct.DiscountPrecentage.Value / 100);
            }

            // შედარება ძველ მონაცემებთან
            if (item.Product.Price != dbProduct.Price ||
                item.Product.DiscountPrecentage != dbProduct.DiscountPrecentage)
            {
                item.Product.Price = dbProduct.Price;
                item.Product.DiscountPrecentage = dbProduct.DiscountPrecentage;
                item.Product.DiscountPrice = freshDiscountPrice; // დაგვჭირდება UI-სთვის
                isChanged = true;
            }

            // 3. სხვა მნიშვნელოვანი ველების განახლება (სახელი, სურათი, ქულები)
            item.Product.Name = dbProduct.Name;
            item.Product.ImageUrl = dbProduct.ImageUrl;
            item.Product.PointsReward = dbProduct.PointsReward;
        }

        if (isChanged)
        {
            await SaveCart();
            NotifyStateChanged();
        }
    }

    private async Task SaveToLocalStorage() => await _localStorage.SetItemAsync("cartItems", CartItems);
    private void NotifyStateChanged() => OnChange?.Invoke();

    public string SelectedDelivery { get; set; } = "";
    public decimal DeliveryFee { get; set; } = 0;
}
