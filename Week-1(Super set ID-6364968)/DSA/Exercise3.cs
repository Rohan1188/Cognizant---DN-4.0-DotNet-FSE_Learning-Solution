using System;

class Program
{
    // Step 1: Understanding Sorting Algorithms
    /*
     * Sorting Algorithms:
     * 
     * 1. Bubble Sort:
     * - Simple comparison-based algorithm
     * - Repeatedly steps through the list, compares adjacent elements, and swaps them if they are in the wrong order
     * - Time Complexity: O(n^2) in the worst and average cases, O(n) in the best case (when the array is already sorted)
     * - Space Complexity: O(1) (in-place sorting)
     * 
     * 2. Insertion Sort:
     * - Builds a sorted array one element at a time
     * - Efficient for small datasets or nearly sorted data
     * - Time Complexity: O(n^2) in the worst and average cases, O(n) in the best case
     * - Space Complexity: O(1)
     * 
     * 3. Quick Sort:
     * - Divide-and-conquer algorithm
     * - Selects a 'pivot' element and partitions the array into elements less than and greater than the pivot
     * - Time Complexity: O(n log n) on average, O(n^2) in the worst case (rare with good pivot selection)
     * - Space Complexity: O(log n) (due to recursion)
     * 
     * 4. Merge Sort:
     * - Also a divide-and-conquer algorithm
     * - Divides the array into halves, sorts them, and merges them back together
     * - Time Complexity: O(n log n) in all cases
     * - Space Complexity: O(n) (not in-place)
     */

    // Step 2: Order class setup
    class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }

        public Order(int orderId, string customerName, decimal totalPrice)
        {
            OrderId = orderId;
            CustomerName = customerName;
            TotalPrice = totalPrice;
        }

        public override string ToString()
        {
            return $"Order ID: {OrderId}, Customer: {CustomerName}, Total Price: {TotalPrice:C}";
        }
    }

    // Step 3: Implement Bubble Sort
    static void BubbleSort(Order[] orders)
    {
        int n = orders.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (orders[j].TotalPrice > orders[j + 1].TotalPrice)
                {
                    // Swap
                    var temp = orders[j];
                    orders[j] = orders[j + 1];
                    orders[j + 1] = temp;
                }
            }
        }
    }

    // Step 3: Implement Quick Sort
    static void QuickSort(Order[] orders, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(orders, low, high);
            QuickSort(orders, low, pi - 1);
            QuickSort(orders, pi + 1, high);
        }
    }

    static int Partition(Order[] orders, int low, int high)
    {
        decimal pivot = orders[high].TotalPrice;
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (orders[j].TotalPrice <= pivot)
            {
                i++;
                // Swap
                var temp = orders[i];
                orders[i] = orders[j];
                orders[j] = temp;
            }
        }

        // Swap the pivot element with the element at i + 1
        var temp1 = orders[i + 1];
        orders[i + 1] = orders[high];
        orders[high] = temp1;

        return i + 1;
    }

    // Helper method to generate random orders
    static Order[] GenerateOrders(int count)
    {
        var random = new Random();
        var orders = new Order[count];

        for (int i = 0; i < count; i++)
        {
            orders[i] = new Order(
                i + 1,
                $"Customer {i + 1}",
                (decimal)(random.NextDouble() * 1000) // Total price between 0 and 1000
            );
        }

        return orders;
    }

    static void Main(string[] args)
    {
        // Generate random orders
        int orderCount = 1000;
        Order[] orders = GenerateOrders(orderCount);
        Order[] ordersForBubbleSort = (Order[])orders.Clone();
        Order[] ordersForQuickSort = (Order[])orders.Clone();

        // Measure Bubble Sort performance
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        BubbleSort(ordersForBubbleSort);
        stopwatch.Stop();
        Console.WriteLine("Bubble Sort completed in: " + stopwatch.ElapsedMilliseconds + " ms");

        // Measure Quick Sort performance
        stopwatch.Restart();
        QuickSort(ordersForQuickSort, 0, ordersForQuickSort.Length - 1);
        stopwatch.Stop();
        Console.WriteLine("Quick Sort completed in: " + stopwatch.ElapsedMilliseconds + " ms");

        // Step 4: Analysis comparison
        Console.WriteLine("\nAlgorithm Analysis:");
        Console.WriteLine("-------------------");
        Console.WriteLine("Bubble Sort:");
        Console.WriteLine("- Time Complexity: O(n^2) - inefficient for large datasets");
        Console.WriteLine("- Best for: Educational purposes, small datasets");
        Console.WriteLine("- Advantages: Simple to implement, easy to understand");
        Console.WriteLine();

        Console.WriteLine("Quick Sort:");
        Console.WriteLine("- Time Complexity: O(n log n) on average - efficient for large datasets");
        Console.WriteLine("- Best for: Large datasets, general-purpose sorting");
        Console.WriteLine("- Advantages: Much faster than Bubble Sort, especially for large arrays");
        Console.WriteLine("- In-place sorting with low memory overhead");
        Console.WriteLine();

        Console.WriteLine("Recommendation:");
        Console.WriteLine("For sorting customer orders on an e-commerce platform:");
        Console.WriteLine("- Use Quick Sort for its efficiency and performance on larger datasets.");
    }
}
