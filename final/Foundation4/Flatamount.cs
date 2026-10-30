public class FlatAmount : Discount
{
    private decimal _amountOff;

    public FlatAmount(decimal amountOff) : base("Flat Amount")
    {
        _amountOff = amountOff;
    }

    // Subtracts a fixed dollar amount, but never goes below $0.
    public override decimal Apply(decimal price, int quantity)
    {
        decimal total = price * quantity - _amountOff;
        if (total < 0)
        {
            total = 0;
        }
        return total;
    }

    public override string GetDescription()
    {
        return $"${_amountOff:F2} off";
    }
}