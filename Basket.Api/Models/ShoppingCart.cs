namespace Basket.Api.Models;

public class ShoppingCart()
{
    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCardItem> Items { get; set; } = [];
    public decimal TotalPrice { get; set; } = 0;
    
    public ShoppingCart(string userName) : this()
    {
        UserName = userName;
    }
    
    public void AddItem(ShoppingCardItem item)
    {
        Items.Add(item);
        TotalPrice += item.Price * item.Quantity;
    }
    
    public void RemoveItem(Guid productId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
            return;
        
        Items.Remove(item);
        TotalPrice -= item.Price * item.Quantity;
    }
}