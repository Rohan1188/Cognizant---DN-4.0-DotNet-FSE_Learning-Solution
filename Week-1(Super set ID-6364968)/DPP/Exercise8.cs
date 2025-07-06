using System;

namespace StrategyPatternExample
{
    // Step 1: Define Strategy Interface
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }

    // Step 2: Implement Concrete Strategies
    public class CreditCardPayment : IPaymentStrategy
    {
        private string _cardNumber;

        public CreditCardPayment(string cardNumber)
        {
            _cardNumber = cardNumber;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid {amount:C} using Credit Card: {_cardNumber}");
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        private string _email;

        public PayPalPayment(string email)
        {
            _email = email;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid {amount:C} using PayPal account: {_email}");
        }
    }

    // Step 3: Implement Context Class
    public class PaymentContext
    {
        private IPaymentStrategy _paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void ExecutePayment(decimal amount)
        {
            if (_paymentStrategy == null)
            {
                Console.WriteLine("Payment strategy not set.");
                return;
            }
            _paymentStrategy.Pay(amount);
        }
    }

    // Step 4: Test the Strategy Implementation
    class Program
    {
        static void Main(string[] args)
        {
            PaymentContext paymentContext = new PaymentContext();

            // Using Credit Card Payment
            paymentContext.SetPaymentStrategy(new CreditCardPayment("1234-5678-9012-3456"));
            paymentContext.ExecutePayment(100.00m);

            // Using PayPal Payment
            paymentContext.SetPaymentStrategy(new PayPalPayment("user@example.com"));
            paymentContext.ExecutePayment(200.00m);

            // Attempting to execute payment without setting a strategy
            paymentContext.SetPaymentStrategy(null);
            paymentContext.ExecutePayment(50.00m);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
