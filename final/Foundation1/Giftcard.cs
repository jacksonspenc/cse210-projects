public class GiftCard : PaymentMethod
{
    private string _code;
    private decimal _balance;
 
    public GiftCard(string ownerName, string code, decimal balance)
        : base(ownerName)
    {
        _code = code;
        _balance = balance;
    }
 
    // The hidden details: the gift card must cover the whole amount.
    public override bool ProcessPayment(decimal amount)
    {
        if (!IsValidAmount(amount))
        {
            Console.WriteLine("  Declined: amount must be greater than zero.");
            return false;
        }
        if (amount > _balance)
        {
            Console.WriteLine($"  Declined: gift card only has ${_balance:F2}.");
            return false;
        }
 
        _balance -= amount;
        Console.WriteLine($"  Approved: ${amount:F2} taken from gift card. Remaining: ${_balance:F2}");
        return true;
    }
 
    public override string GetPaymentDetails()
    {
        return $"Gift card {_code} for {_ownerName}";
    }
 
    public decimal GetRemainingBalance()
    {
        return _balance;
    }
}
 