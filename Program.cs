using StudentExporter.Factories;
using StudentExporter.Interfaces;


Console.WriteLine("=== Экспортатор успеваемости ===");
Console.WriteLine();
Console.WriteLine("Выберите формат входного файла:");
Console.WriteLine("  1 — JSON (students.json)");
Console.Write("Ваш выбор: ");

var inputChoice = Console.ReadLine()?.Trim();

ReaderFactory readerFactory = inputChoice switch
{
    "1" => new JsonReaderFactory()   
};

string inputExtension = inputChoice switch
{
    "1" => "json"
};

string inputPath = Path.Combine(AppContext.BaseDirectory, $"students.{inputExtension}");

if (!File.Exists(inputPath))
{
    inputPath = Path.Combine(AppContext.BaseDirectory, "students.json");
    if (!File.Exists(inputPath))
    {
        Console.WriteLine($"Файл не найден: {inputPath}");
        return;
    }
    readerFactory = new JsonReaderFactory();
    Console.WriteLine($"Файл выбранного формата не найден, используется students.json.");
}

IDataReader reader = readerFactory.CreateReader();
var studentsData = await reader.Read(inputPath);

Console.WriteLine($"\nЗагружено: группа {studentsData.Group}, {studentsData.Semester}");
Console.WriteLine($"Студентов: {studentsData.Students.Count}");


Console.WriteLine();
Console.WriteLine("Выберите формат экспорта:");
Console.WriteLine("  1 - PDF");
Console.WriteLine("  2 - XLSX");
Console.Write("Ваш выбор: ");

var exportChoice = Console.ReadLine()?.Trim();

DocumentFactory factory = null;
string extension;

switch (exportChoice)
{
    case "1":
        factory = new PdfFactory();
        extension = "pdf";
        break;
    case "2":
        factory = new XlsxFactory();
        extension = "xlsx";
        break;
    default: 
        Console.WriteLine("тока 2 цифры на выбор... возьми телефон детка");
        break;
}


string outputDir = Path.Combine(AppContext.BaseDirectory, "output");
Directory.CreateDirectory(outputDir);

string fileName = $"report_{studentsData.Group}_{DateTime.Now:yyyyMMdd_HHmmss}";
string outputPath = Path.Combine(outputDir, fileName);

IExportDocument document = factory.CreateDocument();
await document.Save(outputPath, studentsData.Students, studentsData);

Console.WriteLine();
Console.WriteLine($"✓ Файл успешно сохранён: {outputPath}");
