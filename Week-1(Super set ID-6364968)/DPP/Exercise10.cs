using System;

// Step 2: Model Class
public class Student
{
    public string Name { get; set; }
    public int Id { get; set; }
    public string Grade { get; set; }
}

// Step 3: View Class
public class StudentView
{
    public void DisplayStudentDetails(Student student)
    {
        Console.WriteLine("Student Details:");
        Console.WriteLine($"ID: {student.Id}");
        Console.WriteLine($"Name: {student.Name}");
        Console.WriteLine($"Grade: {student.Grade}");
    }
}

// Step 4: Controller Class
public class StudentController
{
    private Student _model;
    private StudentView _view;

    public StudentController(Student model, StudentView view)
    {
        _model = model;
        _view = view;
    }

    public void SetStudentName(string name)
    {
        _model.Name = name;
    }

    public string GetStudentName()
    {
        return _model.Name;
    }

    public void SetStudentId(int id)
    {
        _model.Id = id;
    }

    public int GetStudentId()
    {
        return _model.Id;
    }

    public void SetStudentGrade(string grade)
    {
        _model.Grade = grade;
    }

    public string GetStudentGrade()
    {
        return _model.Grade;
    }

    public void UpdateView()
    {
        _view.DisplayStudentDetails(_model);
    }
}

// Step 5: Test MVC Implementation
class Program
{
    static void Main(string[] args)
    {
        // Create Model
        Student student = new Student { Name = "Alice", Id = 101, Grade = "A" };

        // Create View
        StudentView view = new StudentView();

        // Create Controller
        StudentController controller = new StudentController(student, view);

        // Display initial details
        controller.UpdateView();

        Console.WriteLine("\nUpdating student details...\n");

        // Update student details via controller
        controller.SetStudentName("Bob");
        controller.SetStudentGrade("B+");

        // Display updated details
        controller.UpdateView();
    }
}
