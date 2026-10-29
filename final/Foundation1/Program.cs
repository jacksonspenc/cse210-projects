using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Payment Processor (Abstraction Demo) ===\n");
 
        Checkout checkout = new Checkout();
        checkout.AddItem("Headphones", 59.99m);
        checkout.AddItem("Phone Case", 15.00m);
        Console.WriteLine($"Cart total: ${checkout.GetTotal():F2}\n");
 
        // Three very different payment methods, all stored as the same abstract type.
        PaymentMethod creditCard = new CreditCard("Jackson", "4111222233334417", new DateTime(2028, 5, 31), 500m);
        PaymentMethod payPal = new PayPal("Jackson", "jackson@gmail.com", 40m);
        PaymentMethod giftCard = new GiftCard("Jackson", "GIFT-9876", 100m);
 
        // The checkout code is identical for every method.
        checkout.Pay(payPal);      // declined: not enough in account
        checkout.Pay(giftCard);    // approved
        checkout.Pay(creditCard);  // approved
    }
}