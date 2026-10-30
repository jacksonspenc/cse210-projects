class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Discount Calculator (Polymorphism Demo) ===\n");
 
        // One list holding three different discount types.
        // The loop calls the same two methods on each one and gets different results.
        List<Discount> discounts = new List<Discount>
        {
            new PercentOff(20),
            new FlatAmount(5),
            new BuyOneGetOne()
        };
 
        decimal price = 10.00m;
        int quantity = 3;
        Console.WriteLine($"Same purchase ({quantity} items at ${price:F2}) with each discount:");
        foreach (Discount discount in discounts)
        {
            Console.WriteLine($"  {discount.GetDescription(),-25} -> ${discount.Apply(price, quantity):F2}");
        }
 
        // A shopping cart where each item can have any kind of discount.
        Console.WriteLine();
        Cart cart = new Cart();
        cart.AddItem(new CartItem("T-Shirt", 15.00m, 2, new PercentOff(25)));
        cart.AddItem(new CartItem("Sneakers", 60.00m, 1, new FlatAmount(10)));
        cart.AddItem(new CartItem("Socks", 4.00m, 4, new BuyOneGetOne()));
        cart.AddItem(new CartItem("Water Bottle", 12.00m, 1));   // no discount
        cart.PrintReceipt();
    }
}