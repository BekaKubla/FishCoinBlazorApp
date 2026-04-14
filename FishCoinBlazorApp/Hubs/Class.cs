using Microsoft.AspNetCore.SignalR;

namespace FishCoinBlazorApp.Hubs
{
    public class NotificationHub : Hub
    {
        // ეს მეთოდი გამოიძახება საიტიდან, რომ სიგნალი დაეგზავნოს POS-ებს
        public async Task NotifyNewOrder()
        {
            await Clients.All.SendAsync("RefreshOrders");
        }
    }
}
