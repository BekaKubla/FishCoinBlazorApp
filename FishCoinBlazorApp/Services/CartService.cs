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
        (i.Product.DiscountPrice > 0 ? i.Product.DiscountPrice : i.Product.Price) * i.Quantity);
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

    private async Task SaveToLocalStorage() => await _localStorage.SetItemAsync("cartItems", CartItems);
    private void NotifyStateChanged() => OnChange?.Invoke();

    public string SelectedDelivery { get; set; } = "";
    public decimal DeliveryFee { get; set; } = 0;
}
