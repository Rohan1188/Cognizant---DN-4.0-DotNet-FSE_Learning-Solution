using System;

// Step 2: Define a Product Class
public class Computer
{
    // Attributes of the Computer class
    public string CPU { get; private set; }
    public string RAM { get; private set; }
    public string Storage { get; private set; }
    public string GPU { get; private set; }
    public string OS { get; private set; }

    // Private constructor that takes the Builder as a parameter
    private Computer(Builder builder)
    {
        CPU = builder.CPU;
        RAM = builder.RAM;
        Storage = builder.Storage;
        GPU = builder.GPU;
        OS = builder.OS;
    }

    // Step 3: Implement the Builder Class
    public class Builder
    {
        // Optional parameters with default values
        public string CPU { get; private set; } = "Default CPU";
        public string RAM { get; private set; } = "8GB";
        public string Storage { get; private set; } = "256GB SSD";
        public string GPU { get; private set; } = "Integrated Graphics";
        public string OS { get; private set; } = "Windows 10";

        // Methods to set each attribute
        public Builder SetCPU(string cpu)
        {
            CPU = cpu;
            return this;
        }

        public Builder SetRAM(string ram)
        {
            RAM = ram;
            return this;
        }

        public Builder SetStorage(string storage)
        {
            Storage = storage;
            return this;
        }

        public Builder SetGPU(string gpu)
        {
            GPU = gpu;
            return this;
        }

        public Builder SetOS(string os)
        {
            OS = os;
            return this;
        }

        // Build method that returns an instance of Computer
        public Computer Build()
        {
            return new Computer(this);
        }
    }

    // Override ToString for easy display of Computer details
    public override string ToString()
    {
        return $"Computer Specifications:\n" +
               $"CPU: {CPU}\n" +
               $"RAM: {RAM}\n" +
               $"Storage: {Storage}\n" +
               $"GPU: {GPU}\n" +
               $"OS: {OS}";
    }
}

// Step 5: Test the Builder Implementation
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Builder Pattern Demo");
        Console.WriteLine("---------------------");

        // Create a Computer using the Builder pattern
        Computer gamingPC = new Computer.Builder()
            .SetCPU("Intel i9")
            .SetRAM("32GB")
            .SetStorage("1TB SSD")
            .SetGPU("NVIDIA RTX 3080")
            .SetOS("Windows 11")
            .Build();

        Console.WriteLine(gamingPC);

        // Create another Computer with different configuration
        Computer officePC = new Computer.Builder()
            .SetCPU("AMD Ryzen 5")
            .SetRAM("16GB")
            .SetStorage("512GB SSD")
            .SetOS("Windows 10")
            .Build();

        Console.WriteLine("\n" + officePC);

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
