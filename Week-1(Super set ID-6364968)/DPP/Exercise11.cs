using System;

namespace DependencyInjectionExample
{
    // Step 1: Define Repository Interface
    public interface ICustomerRepository
    {
        Customer FindCustomerById(int id);
    }

    // Step 2: Implement Concrete Repository
    public class CustomerRepositoryImpl : ICustomerRepository
    {
        public Customer FindCustomerById(int id)
        {
            // Simulating a customer lookup
            if (id == 1)
            {
                return new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            }
            else
            {
                return null; // Customer not found
            }
        }
    }

    // Model Class
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    // Step 3: Define Service Class
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        // Step 4: Implement Dependency Injection via Constructor
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.FindCustomerById(id);
        }
    }

    // Step 5: Test the Dependency Injection Implementation
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the repository
            ICustomerRepository customerRepository = new CustomerRepositoryImpl();

            // Inject the repository into the service
            CustomerService customerService = new CustomerService(customerRepository);

            // Find a customer by ID
            Customer customer = customerService.GetCustomerById(1);

            // Display customer details
            if (customer != null)
            {
                Console.WriteLine($"Customer ID: {customer.Id}");
                Console.WriteLine($"Customer Name: {customer.Name}");
                Console.WriteLine($"Customer Email: {customer.Email}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
