using StudentExporter.Factories;
using StudentExporter.Interfaces;


Console.WriteLine("=== Экспортатор успеваемости ===");
Console.WriteLine();
Console.WriteLine("Выберите формат входного файла:");
Console.WriteLine("  1 — JSON (students.json)");
Console.WriteLine("  2 — XML  (students.xml)");
Console.WriteLine("  3 — XLSX (students.xlsx)");
Console.Write("Ваш выбор: ");

var inputChoice = Console.ReadLine()?.Trim();

ReaderFactory readerFactory = inputChoice switch
{
    "2" => new XmlReaderFactory(),
    "3" => new XlsxReaderFactory(),
    _   => new JsonReaderFactory()   
};

string inputExtension = inputChoice switch
{
    "2" => "xml",
    "3" => "xlsx",
    _   => "json"
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
Console.WriteLine("  1 — PDF");
Console.WriteLine("  2 — TXT");
Console.WriteLine("  3 — XLSX");
Console.Write("Ваш выбор: ");

var exportChoice = Console.ReadLine()?.Trim();


DocumentFactory factory;
string extension;

switch (exportChoice)
{
    case "1":
        factory   = new PdfFactory();
        extension = "pdf";
        break;
    case "3":
        factory   = new XlsxFactory();
        extension = "xlsx";
        break;
    default: 
        factory   = new TxtFactory();
        extension = "txt";
        break;
}


string outputDir = Path.Combine(AppContext.BaseDirectory, "output");
Directory.CreateDirectory(outputDir);

string fileName = $"report_{studentsData.Group}_{DateTime.Now:yyyyMMdd_HHmmss}.{extension}";
string outputPath = Path.Combine(outputDir, fileName);

IExportDocument document = factory.CreateDocument();
await document.Save(outputPath, studentsData.Students, studentsData);

Console.WriteLine();
Console.WriteLine($"✓ Файл успешно сохранён: {outputPath}");
