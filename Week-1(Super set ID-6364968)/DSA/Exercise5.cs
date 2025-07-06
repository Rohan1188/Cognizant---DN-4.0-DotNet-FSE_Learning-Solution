using System;

class Program
{
    // Step 1: Understanding Linked Lists
    /*
     * Types of Linked Lists:
     * 
     * 1. Singly Linked List:
     * - Consists of nodes where each node contains data and a reference (or pointer) to the next node.
     * - Allows traversal in one direction (from head to tail).
     * - Efficient for insertions and deletions as they do not require shifting elements.
     * 
     * 2. Doubly Linked List:
     * - Each node contains data, a reference to the next node, and a reference to the previous node.
     * - Allows traversal in both directions (forward and backward).
     * - More memory overhead due to the extra pointer, but allows for more flexible operations.
     */

    // Step 2: Task class setup
    class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }

        public Task(int taskId, string taskName, string status)
        {
            TaskId = taskId;
            TaskName = taskName;
            Status = status;
        }

        public override string ToString()
        {
            return $"Task ID: {TaskId}, Name: {TaskName}, Status: {Status}";
        }
    }

    // Node class for the singly linked list
    class Node
    {
        public Task Data { get; set; }
        public Node Next { get; set; }

        public Node(Task data)
        {
            Data = data;
            Next = null;
        }
    }

    // Singly Linked List for managing tasks
    class TaskLinkedList
    {
        private Node head;

        // Step 3: Add a task
        public void AddTask(Task task)
        {
            Node newNode = new Node(task);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        // Search for a task by ID
        public Task SearchTask(int taskId)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.TaskId == taskId)
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return null; // Not found
        }

        // Traverse and display all tasks
        public void TraverseTasks()
        {
            Node current = head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

        // Delete a task by ID
        public void DeleteTask(int taskId)
        {
            if (head == null) return;

            // If the head needs to be removed
            if (head.Data.TaskId == taskId)
            {
                head = head.Next;
                Console.WriteLine($"Task with ID {taskId} has been deleted.");
                return;
            }

            Node current = head;
            while (current.Next != null)
            {
                if (current.Next.Data.TaskId == taskId)
                {
                    current.Next = current.Next.Next; // Bypass the node to delete it
                    Console.WriteLine($"Task with ID {taskId} has been deleted.");
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine("Task not found.");
        }
    }

    static void Main(string[] args)
    {
        // Step 3: Create an instance of TaskLinkedList
        TaskLinkedList taskList = new TaskLinkedList();

        // Adding tasks
        taskList.AddTask(new Task(1, "Design UI", "In Progress"));
        taskList.AddTask(new Task(2, "Implement Backend", "Pending"));
        taskList.AddTask(new Task(3, "Write Documentation", "Completed"));

        // Traverse tasks
        Console.WriteLine("Task List:");
        taskList.TraverseTasks();
        Console.WriteLine();

        // Search for a task
        var searchResult = taskList.SearchTask(2);
        Console.WriteLine("Search Result:");
        Console.WriteLine(searchResult != null ? searchResult.ToString() : "Task not found.");
        Console.WriteLine();

        // Delete a task
        taskList.DeleteTask(2);
        Console.WriteLine("Task List after deletion:");
        taskList.TraverseTasks();
        Console.WriteLine();

        // Attempt to delete a non-existing task
        taskList.DeleteTask(4);
    }
}
