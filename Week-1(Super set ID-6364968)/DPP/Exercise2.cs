using System;

// Step 2: Define Document Classes
public interface IDocument
{
    void Open();
}

// Step 3: Create Concrete Document Classes
public class WordDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening a Word document.");
    }
}

public class PdfDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening a PDF document.");
    }
}

public class ExcelDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening an Excel document.");
    }
}

// Step 4: Implement the Factory Method
public abstract class DocumentFactory
{
    public abstract IDocument CreateDocument();
}

public class WordDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new WordDocument();
    }
}

public class PdfDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new PdfDocument();
    }
}

public class ExcelDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new ExcelDocument();
    }
}

// Step 5: Test the Factory Method Implementation
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Factory Method Pattern Demo");
        Console.WriteLine("---------------------------");

        // Create a Word document
        DocumentFactory wordFactory = new WordDocumentFactory();
        IDocument wordDoc = wordFactory.CreateDocument();
        wordDoc.Open();

        // Create a PDF document
        DocumentFactory pdfFactory = new PdfDocumentFactory();
        IDocument pdfDoc = pdfFactory.CreateDocument();
        pdfDoc.Open();

        // Create an Excel document
        DocumentFactory excelFactory = new ExcelDocumentFactory();
        IDocument excelDoc = excelFactory.CreateDocument();
        excelDoc.Open();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
