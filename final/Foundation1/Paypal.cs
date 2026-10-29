public class PayPal : PaymentMethod
{
    private string _email;
    private decimal _accountBalance;
 
    public PayPal(string ownerName, string email, decimal accountBalance)
        : base(ownerName)
    {
        _email = email;
        _accountBalance = accountBalance;
    }
 
    // The hidden details: make sure the account has enough money.
    public override bool ProcessPayment(decimal amount)
    {
        if (!IsValidAmount(amount))
        {
            Console.WriteLine("  Declined: amount must be greater than zero.");
            return false;
        }
        if (amount > _accountBalance)
        {
            Console.WriteLine("  Declined: not enough money in PayPal account.");
            return false;
        }
 
        _accountBalance -= amount;
        Console.WriteLine($"  Approved: ${amount:F2} sent from PayPal. Remaining: ${_accountBalance:F2}");
        return true;
    }
 
    public override string GetPaymentDetails()
    {
        return $"PayPal account for {_ownerName} ({_email})";
    }
}