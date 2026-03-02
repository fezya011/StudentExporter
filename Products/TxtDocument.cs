using StudentExporter.Interfaces;
using StudentExporter.Models;

namespace StudentExporter.Products;

public class TxtDocument : IExportDocument
{
    public async Task Save(string path, List<Student> data, StudentsData meta)
    {
        var lines = new List<string>
        {
            $"ОТЧЁТ УСПЕВАЕМОСТИ",
            $"Группа: {meta.Group}",
            $"Семестр: {meta.Semester}",
            new string('-', 40),
            $"{"№",-5} {"ФИО студента",-25} {"Оценка",-8}",
            new string('-', 40)
        };

        foreach (var student in data)
        {
            lines.Add($"{student.Id,-5} {student.Name,-25} {student.Grade,-8}");
        }

        lines.Add(new string('-', 40));
        lines.Add($"Всего студентов: {data.Count}");
        lines.Add($"Средний балл: {data.Average(s => s.Grade):F2}");

        await File.WriteAllLinesAsync(path, lines, System.Text.Encoding.UTF8);
    }
}
