public class Checkout
{
    private List<(string Name, decimal Price)> _items = new List<(string, decimal)>();

    public void AddItem(string name, decimal price)
    {
        _items.Add((name, price));
    }

    public decimal GetTotal()
    {
        decimal total = 0;
        foreach (var item in _items)
        {
            total += item.Price;
        }
        return total;
    }

    public void Pay(PaymentMethod method)
    {
        Console.WriteLine($"Paying ${GetTotal():F2} with: {method.GetPaymentDetails()}");
        bool success = method.ProcessPayment(GetTotal());

        if (success)
        {
            Console.WriteLine("  Order complete!\n");
        }
        else
        {
            Console.WriteLine("  Please try another payment method.\n");
        }
    }
}