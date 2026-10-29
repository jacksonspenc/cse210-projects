public abstract class PaymentMethod
{
    protected string _ownerName;
 
    public PaymentMethod(string ownerName)
    {
        _ownerName = ownerName;
    }
 
    // Every payment method must know how to process a payment
    public abstract bool ProcessPayment(decimal amount);
 
    // and how to describe itself.
    public abstract string GetPaymentDetails();
 
    // Shared helper that all child classes can use.
    protected bool IsValidAmount(decimal amount)
    {
        return amount > 0;
    }
}