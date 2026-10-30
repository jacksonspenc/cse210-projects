public class PercentOff : Discount
{
    private decimal _percent;
 
    public PercentOff(decimal percent) : base("Percent Off")
    {
        _percent = percent;
    }
 
    // Takes a percentage off the whole total.
    public override decimal Apply(decimal price, int quantity)
    {
        decimal total = price * quantity;
        return total * (1 - _percent / 100);
    }
 
    public override string GetDescription()
    {
        return $"{_percent}% off";
    }
}