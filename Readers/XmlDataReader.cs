using System.Xml.Linq;
using StudentExporter.Interfaces;
using StudentExporter.Models;

namespace StudentExporter.Readers;

public class XmlDataReader : IDataReader
{
    public Task<StudentsData> Read(string path)
    {
        var doc = XDocument.Load(path);
        var root = doc.Root ?? throw new InvalidDataException("Пустой XML-файл.");

        var result = new StudentsData
        {
            Group    = root.Element("Group")?.Value    ?? string.Empty,
            Semester = root.Element("Semester")?.Value ?? string.Empty,
            Students = root.Element("Students")?
                .Elements("Student")
                .Select(e => new Student
                {
                    Id    = int.Parse(e.Element("Id")?.Value    ?? "0"),
                    Name  = e.Element("Name")?.Value             ?? string.Empty,
                    Grade = int.Parse(e.Element("Grade")?.Value ?? "0")
                })
                .ToList() ?? new List<Student>()
        };

        return Task.FromResult(result);
    }
}
