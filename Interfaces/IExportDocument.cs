using StudentExporter.Models;

namespace StudentExporter.Interfaces;

public interface IExportDocument
{
    Task Save(string path, List<Student> data, StudentsData meta);
}
