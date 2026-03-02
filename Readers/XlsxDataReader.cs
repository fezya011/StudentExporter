using ClosedXML.Excel;
using StudentExporter.Interfaces;
using StudentExporter.Models;

namespace StudentExporter.Readers;
public class XlsxDataReader : IDataReader
{
    public Task<StudentsData> Read(string path)
    {
        using var workbook = new XLWorkbook(path);
        var ws = workbook.Worksheet(1);

        var result = new StudentsData
        {
            Group    = ws.Cell(1, 2).GetString(),
            Semester = ws.Cell(2, 2).GetString(),
            Students = new List<Student>()
        };

        int row = 4;
        while (!ws.Cell(row, 1).IsEmpty())
        {
            result.Students.Add(new Student
            {
                Id    = ws.Cell(row, 1).GetValue<int>(),
                Name  = ws.Cell(row, 2).GetString(),
                Grade = ws.Cell(row, 3).GetValue<int>()
            });
            row++;
        }

        return Task.FromResult(result);
    }
}
