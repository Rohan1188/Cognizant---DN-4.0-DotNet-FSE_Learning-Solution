using System;

// Step 2: Define Subject Interface
public interface IImage
{
    void Display();
}

// Step 3: Implement Real Subject Class
public class RealImage : IImage
{
    private string _fileName;

    public RealImage(string fileName)
    {
        _fileName = fileName;
        LoadFromRemoteServer(_fileName);
    }

    private void LoadFromRemoteServer(string fileName)
    {
        Console.WriteLine($"Loading image '{fileName}' from remote server...");
        // Simulate delay for loading
        System.Threading.Thread.Sleep(1000);
    }

    public void Display()
    {
        Console.WriteLine($"Displaying image '{_fileName}'.");
    }
}

// Step 4: Implement Proxy Class
public class ProxyImage : IImage
{
    private RealImage _realImage;
    private string _fileName;

    public ProxyImage(string fileName)
    {
        _fileName = fileName;
    }

    public void Display()
    {
        // Lazy initialization: only load when needed
        if (_realImage == null)
        {
            _realImage = new RealImage(_fileName);
        }
        else
        {
            Console.WriteLine($"Using cached image '{_fileName}'.");
        }
        _realImage.Display();
    }
}

// Step 5: Test Proxy Implementation
class Program
{
    static void Main(string[] args)
    {
        IImage image1 = new ProxyImage("photo1.jpg");
        IImage image2 = new ProxyImage("photo2.jpg");

        // Image loads only on first display call
        image1.Display();
        Console.WriteLine();

        // Second call uses cached image - no loading delay
        image1.Display();
        Console.WriteLine();

        // Load second image
        image2.Display();
        Console.WriteLine();

        // Cached second image display
        image2.Display();
    }
}
