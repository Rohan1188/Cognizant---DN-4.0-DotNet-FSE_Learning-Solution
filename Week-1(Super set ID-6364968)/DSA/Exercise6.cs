using System;
using System.Linq;

class Program
{
    // Step 1: Understanding Search Algorithms
    /*
     * Search Algorithms:
     * 
     * 1. Linear Search:
     * - A simple search algorithm that checks each element in the list sequentially until the desired element is found or the list ends.
     * - Time Complexity: O(n) in the worst and average cases, O(1) in the best case (when the element is found at the first position).
     * - Space Complexity: O(1) as it uses a constant amount of space.
     * 
     * 2. Binary Search:
     * - A more efficient search algorithm that works on sorted arrays. It divides the search interval in half repeatedly.
     * - Time Complexity: O(log n) in the worst case, O(1) in the best case (when the middle element is the target).
     * - Space Complexity: O(1) for the iterative version, O(log n) for the recursive version due to call stack.
     * 
     * When to Use:
     * - Use Linear Search for small or unsorted datasets where simplicity is preferred.
     * - Use Binary Search for large, sorted datasets where efficiency is crucial.
     */

    // Step 2: Book class setup
    class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(int bookId, string title, string author)
        {
            BookId = bookId;
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return $"ID: {BookId}, Title: {Title}, Author: {Author}";
        }
    }

    // Step 3: Implement Linear Search
    static Book LinearSearch(Book[] books, string title)
    {
        foreach (var book in books)
        {
            if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return book;
            }
        }
        return null; // Not found
    }

    // Step 3: Implement Binary Search
    static Book BinarySearch(Book[] sortedBooks, string title)
    {
        int left = 0;
        int right = sortedBooks.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            int comparison = string.Compare(sortedBooks[mid].Title, title, StringComparison.OrdinalIgnoreCase);
            if (comparison == 0)
            {
                return sortedBooks[mid]; // Found
            }
            else if (comparison < 0)
            {
                left = mid + 1; // Search in the right half
            }
            else
            {
                right = mid - 1; // Search in the left half
            }
        }

        return null; // Not found
    }

    static void Main(string[] args)
    {
        // Sample books
        Book[] books = new Book[]
        {
            new Book(1, "The Great Gatsby", "F. Scott Fitzgerald"),
            new Book(2, "To Kill a Mockingbird", "Harper Lee"),
            new Book(3, "1984", "George Orwell"),
            new Book(4, "Pride and Prejudice", "Jane Austen"),
            new Book(5, "The Catcher in the Rye", "J.D. Salinger")
        };

        // Linear Search
        Console.WriteLine("Linear Search:");
        string searchTitle = "1984";
        var linearResult = LinearSearch(books, searchTitle);
        Console.WriteLine(linearResult != null ? linearResult.ToString() : "Book not found.");
        Console.WriteLine();

        // Binary Search (requires sorted array)
        Console.WriteLine("Binary Search:");
        var sortedBooks = books.OrderBy(b => b.Title).ToArray(); // Sort books by title
        searchTitle = "Pride and Prejudice";
        var binaryResult = BinarySearch(sortedBooks, searchTitle);
        Console.WriteLine(binaryResult != null ? binaryResult.ToString() : "Book not found.");
        Console.WriteLine();

        // Time Complexity Analysis
        Console.WriteLine("Time Complexity Analysis:");
        Console.WriteLine("Linear Search: O(n)");
        Console.WriteLine("Binary Search: O(log n)");
        Console.WriteLine();

        // Discussion on when to use each algorithm
        Console.WriteLine("When to Use Each Algorithm:");
        Console.WriteLine("- Use Linear Search for small or unsorted datasets.");
        Console.WriteLine("- Use Binary Search for large, sorted datasets for better performance.");
    }
}
