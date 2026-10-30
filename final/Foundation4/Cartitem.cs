public class CartItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Discount? Discount { get; set; }   // null means no discount

    public CartItem(string name, decimal price, int quantity, Discount? discount = null)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Discount = discount;
    }

    public decimal GetFullPrice()
    {
        return Price * Quantity;
    }

    public decimal GetFinalPrice()
    {
        if (Discount == null)
        {
            return GetFullPrice();
        }
        return Discount.Apply(Price, Quantity);
    }
}