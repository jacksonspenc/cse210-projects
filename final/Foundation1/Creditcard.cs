public class CreditCard : PaymentMethod
{
    private string _cardNumber;
    private DateTime _expirationDate;
    private decimal _creditLimit;
    private decimal _balanceOwed;
 
    public CreditCard(string ownerName, string cardNumber, DateTime expirationDate, decimal creditLimit)
        : base(ownerName)
    {
        _cardNumber = cardNumber;
        _expirationDate = expirationDate;
        _creditLimit = creditLimit;
        _balanceOwed = 0;
    }
 
    // The hidden details: check the amount, the expiration date, and the credit limit.
    public override bool ProcessPayment(decimal amount)
    {
        if (!IsValidAmount(amount))
        {
            Console.WriteLine("  Declined: amount must be greater than zero.");
            return false;
        }
        if (IsExpired())
        {
            Console.WriteLine("  Declined: card is expired.");
            return false;
        }
        if (_balanceOwed + amount > _creditLimit)
        {
            Console.WriteLine("  Declined: credit limit exceeded.");
            return false;
        }
 
        _balanceOwed += amount;
        Console.WriteLine($"  Approved: ${amount:F2} charged to card. Balance owed: ${_balanceOwed:F2}");
        return true;
    }
 
    public override string GetPaymentDetails()
    {
        return $"Credit card for {_ownerName} ({MaskCardNumber()})";
    }
 
    private bool IsExpired()
    {
        return DateTime.Now > _expirationDate;
    }
 
    private string MaskCardNumber()
    {
        return "ending in " + _cardNumber.Substring(_cardNumber.Length - 4);
    }
}
 