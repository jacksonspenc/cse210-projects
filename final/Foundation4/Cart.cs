public class Cart
{
    private List<CartItem> _items = new List<CartItem>();
 
    public void AddItem(CartItem item)
    {
        _items.Add(item);
    }
 
    public decimal GetSubtotal()
    {
        decimal subtotal = 0;
        foreach (CartItem item in _items)
        {
            subtotal += item.GetFullPrice();
        }
        return subtotal;
    }
 
    public decimal GetTotal()
    {
        decimal total = 0;
        foreach (CartItem item in _items)
        {
            total += item.GetFinalPrice();
        }
        return total;
    }
 
    public void PrintReceipt()
    {
        Console.WriteLine("RECEIPT");
        Console.WriteLine(new string('-', 52));
 
        foreach (CartItem item in _items)
        {
            string deal = item.Discount == null ? "no discount" : item.Discount.GetDescription();
            Console.WriteLine($"{item.Quantity} x {item.Name} @ ${item.Price:F2}");
            Console.WriteLine($"   {deal,-25} ${item.GetFullPrice(),8:F2} -> ${item.GetFinalPrice(),8:F2}");
        }
 
        Console.WriteLine(new string('-', 52));
        decimal subtotal = GetSubtotal();
        decimal total = GetTotal();
        Console.WriteLine($"Subtotal:   ${subtotal:F2}");
        Console.WriteLine($"You saved:  ${subtotal - total:F2}");
        Console.WriteLine($"Total:      ${total:F2}");
    }
}