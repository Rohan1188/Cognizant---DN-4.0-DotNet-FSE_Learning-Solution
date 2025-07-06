using System;

// Step 2: Define Target Interface
public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount);
}

// Step 3: Implement Adaptee Classes (Different Payment Gateways with their own APIs)

public class PayPalGateway
{
    public void PayWithPayPal(decimal amount)
    {
        Console.WriteLine($"Processing payment of ${amount} through PayPal.");
    }
}

public class StripeGateway
{
    public void MakeStripePayment(decimal amountInCents)
    {
        Console.WriteLine($"Processing payment of ${amountInCents / 100m} through Stripe.");
    }
}

public class SquareGateway
{
    public void SquarePay(double amount)
    {
        Console.WriteLine($"Processing payment of ${amount} through Square.");
    }
}

// Step 4: Implement Adapter Classes

public class PayPalAdapter : IPaymentProcessor
{
    private readonly PayPalGateway _payPalGateway;

    public PayPalAdapter(PayPalGateway payPalGateway)
    {
        _payPalGateway = payPalGateway;
    }

    public void ProcessPayment(decimal amount)
    {
        _payPalGateway.PayWithPayPal(amount);
    }
}

public class StripeAdapter : IPaymentProcessor
{
    private readonly StripeGateway _stripeGateway;

    public StripeAdapter(StripeGateway stripeGateway)
    {
        _stripeGateway = stripeGateway;
    }

    public void ProcessPayment(decimal amount)
    {
        // Stripe expects amount in cents as int
        int amountInCents = (int)(amount * 100);
        _stripeGateway.MakeStripePayment(amountInCents);
    }
}

public class SquareAdapter : IPaymentProcessor
{
    private readonly SquareGateway _squareGateway;

    public SquareAdapter(SquareGateway squareGateway)
    {
        _squareGateway = squareGateway;
    }

    public void ProcessPayment(decimal amount)
    {
        // Square accepts double
        _squareGateway.SquarePay((double)amount);
    }
}

// Step 5: Test the Adapter Implementation

class Program
{
    static void Main(string[] args)
    {
        decimal paymentAmount = 100.50m;

        IPaymentProcessor paypalProcessor = new PayPalAdapter(new PayPalGateway());
        IPaymentProcessor stripeProcessor = new StripeAdapter(new StripeGateway());
        IPaymentProcessor squareProcessor = new SquareAdapter(new SquareGateway());

        Console.WriteLine("Using PayPal Adapter:");
        paypalProcessor.ProcessPayment(paymentAmount);

        Console.WriteLine("\nUsing Stripe Adapter:");
        stripeProcessor.ProcessPayment(paymentAmount);

        Console.WriteLine("\nUsing Square Adapter:");
        squareProcessor.ProcessPayment(paymentAmount);
    }
}
