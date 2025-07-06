using System;

class Program
{
    // Step 1: Understanding Array Representation
    /*
     * Array Representation in Memory:
     * - Arrays are a collection of elements stored in contiguous memory locations.
     * - Each element can be accessed using an index, which is an integer value.
     * - The memory address of an element can be calculated using the base address of the array and the size of each element.
     * 
     * Advantages of Arrays:
     * - Fast access time: O(1) for accessing elements by index.
     * - Memory efficiency: Arrays have a low overhead compared to other data structures.
     * - Simple implementation: Easy to use and understand.
     * 
     * Limitations of Arrays:
     * - Fixed size: Once an array is created, its size cannot be changed.
     * - Inefficient insertions and deletions: O(n) time complexity for these operations, as elements may need to be shifted.
     * - Wasted space: If the array is not fully utilized, it can lead to wasted memory.
     */

    // Step 2: Employee class setup
    class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        public Employee(int employeeId, string name, string position, decimal salary)
        {
            EmployeeId = employeeId;
            Name = name;
            Position = position;
            Salary = salary;
        }

        public override string ToString()
        {
            return $"ID: {EmployeeId}, Name: {Name}, Position: {Position}, Salary: {Salary:C}";
        }
    }

    // Employee Management System
    class EmployeeManagementSystem
    {
        private Employee[] employees;
        private int count;

        public EmployeeManagementSystem(int size)
        {
            employees = new Employee[size];
            count = 0;
        }

        // Step 3: Add an employee
        public void AddEmployee(Employee employee)
        {
            if (count < employees.Length)
            {
                employees[count] = employee;
                count++;
            }
            else
            {
                Console.WriteLine("Employee array is full. Cannot add more employees.");
            }
        }

        // Search for an employee by ID
        public Employee SearchEmployee(int employeeId)
        {
            for (int i = 0; i < count; i++)
            {
                if (employees[i].EmployeeId == employeeId)
                {
                    return employees[i];
                }
            }
            return null; // Not found
        }

        // Traverse and display all employees
        public void TraverseEmployees()
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(employees[i]);
            }
        }

        // Delete an employee by ID
        public void DeleteEmployee(int employeeId)
        {
            for (int i = 0; i < count; i++)
            {
                if (employees[i].EmployeeId == employeeId)
                {
                    // Shift elements to the left
                    for (int j = i; j < count - 1; j++)
                    {
                        employees[j] = employees[j + 1];
                    }
                    employees[count - 1] = null; // Clear the last element
                    count--;
                    Console.WriteLine($"Employee with ID {employeeId} has been deleted.");
                    return;
                }
            }
            Console.WriteLine("Employee not found.");
        }
    }

    static void Main(string[] args)
    {
        // Step 3: Create an instance of EmployeeManagementSystem
        EmployeeManagementSystem ems = new EmployeeManagementSystem(5);

        // Adding employees
        ems.AddEmployee(new Employee(1, "Alice", "Developer", 60000));
        ems.AddEmployee(new Employee(2, "Bob", "Manager", 80000));
        ems.AddEmployee(new Employee(3, "Charlie", "Designer", 50000));

        // Traverse employees
        Console.WriteLine("Employee List:");
        ems.TraverseEmployees();
        Console.WriteLine();

        // Search for an employee
        var searchResult = ems.SearchEmployee(2);
        Console.WriteLine("Search Result:");
        Console.WriteLine(searchResult != null ? searchResult.ToString() : "Employee not found.");
        Console.WriteLine();

        // Delete an employee
        ems.DeleteEmployee(2);
        Console.WriteLine("Employee List after deletion:");
        ems.TraverseEmployees();
        Console.WriteLine();

        // Attempt to add more employees than the array can hold
        ems.AddEmployee(new Employee(4, "David", "Tester", 40000));
        ems.AddEmployee(new Employee(5, "Eve", "HR", 70000));
        ems.AddEmployee(new Employee(6, "Frank", "Admin", 30000)); // This should fail
    }
}
