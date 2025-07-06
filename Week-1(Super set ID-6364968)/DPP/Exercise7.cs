using System;
using System.Collections.Generic;

// Step 2: Define Subject Interface
public interface IStock
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

// Step 4: Define Observer Interface
public interface IObserver
{
    void Update(decimal price);
}

// Step 3: Implement Concrete Subject
public class StockMarket : IStock
{
    private List<IObserver> observers = new List<IObserver>();
    private decimal _stockPrice;

    public decimal StockPrice
    {
        get => _stockPrice;
        set
        {
            _stockPrice = value;
            NotifyObservers();
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(_stockPrice);
        }
    }
}

// Step 5: Implement Concrete Observers

public class MobileApp : IObserver
{
    private string _appName;

    public MobileApp(string appName)
    {
        _appName = appName;
    }

    public void Update(decimal price)
    {
        Console.WriteLine($"MobileApp {_appName} received stock price update: ${price}");
    }
}

public class WebApp : IObserver
{
    private string _websiteName;

    public WebApp(string websiteName)
    {
        _websiteName = websiteName;
    }

    public void Update(decimal price)
    {
        Console.WriteLine($"WebApp {_websiteName} received stock price update: ${price}");
    }
}

// Step 6: Test the Observer Implementation

class Program
{
    static void Main(string[] args)
    {
        StockMarket stockMarket = new StockMarket();

        IObserver mobileApp1 = new MobileApp("MobileAppOne");
        IObserver mobileApp2 = new MobileApp("MobileAppTwo");
        IObserver webApp = new WebApp("FinanceWeb");

        stockMarket.RegisterObserver(mobileApp1);
        stockMarket.RegisterObserver(mobileApp2);
        stockMarket.RegisterObserver(webApp);

        Console.WriteLine("Updating stock price to 150.75");
        stockMarket.StockPrice = 150.75m;

        Console.WriteLine("\nRemoving MobileAppTwo observer");
        stockMarket.RemoveObserver(mobileApp2);

        Console.WriteLine("\nUpdating stock price to 155.00");
        stockMarket.StockPrice = 155.00m;
    }
}
