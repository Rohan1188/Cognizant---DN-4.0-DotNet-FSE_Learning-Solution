using System;

// Step 2: Define Component Interface
public interface INotifier
{
    void Send(string message);
}

// Step 3: Implement Concrete Component
public class EmailNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending Email Notification: {message}");
    }
}

// Step 4: Implement Decorator Classes

// Abstract Decorator Class
public abstract class NotifierDecorator : INotifier
{
    protected INotifier _wrappee;

    public NotifierDecorator(INotifier notifier)
    {
        _wrappee = notifier;
    }

    public virtual void Send(string message)
    {
        _wrappee.Send(message);
    }
}

// Concrete Decorator - SMS
public class SMSNotifierDecorator : NotifierDecorator
{
    public SMSNotifierDecorator(INotifier notifier) : base(notifier) { }

    public override void Send(string message)
    {
        base.Send(message); // send notification via wrapped notifier
        SendSMS(message);   // add SMS notification
    }

    private void SendSMS(string message)
    {
        Console.WriteLine($"Sending SMS Notification: {message}");
    }
}

// Concrete Decorator - Slack
public class SlackNotifierDecorator : NotifierDecorator
{
    public SlackNotifierDecorator(INotifier notifier) : base(notifier) { }

    public override void Send(string message)
    {
        base.Send(message); // send notification via wrapped notifier
        SendSlack(message); // add Slack notification
    }

    private void SendSlack(string message)
    {
        Console.WriteLine($"Sending Slack Notification: {message}");
    }
}

// Step 5: Test the Decorator Implementation
class Program
{
    static void Main(string[] args)
    {
        string message = "This is a test notification.";

        // Base notifier - only Email
        INotifier emailNotifier = new EmailNotifier();

        Console.WriteLine("Email only:");
        emailNotifier.Send(message);

        Console.WriteLine("\nEmail + SMS:");
        // Email + SMS
        INotifier emailAndSmsNotifier = new SMSNotifierDecorator(emailNotifier);
        emailAndSmsNotifier.Send(message);

        Console.WriteLine("\nEmail + SMS + Slack:");
        // Email + SMS + Slack
        INotifier emailSmsSlackNotifier = new SlackNotifierDecorator(emailAndSmsNotifier);
        emailSmsSlackNotifier.Send(message);
    }
}
