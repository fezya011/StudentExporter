using StudentExporter.Models;

namespace StudentExporter.Interfaces;

public interface IDataReader
{
    Task<StudentsData> Read(string path);
}
