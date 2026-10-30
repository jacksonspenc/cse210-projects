public class BuyOneGetOne : Discount
{
    public BuyOneGetOne() : base("Buy One Get One")
    {
    }
 
    // Every second item is free: you pay for half, rounded up.
    public override decimal Apply(decimal price, int quantity)
    {
        int paidItems = (quantity + 1) / 2;
        return price * paidItems;
    }
 
    public override string GetDescription()
    {
        return "Buy one, get one free";
    }
}