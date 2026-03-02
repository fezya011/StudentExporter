using ClosedXML.Excel;
using StudentExporter.Interfaces;
using StudentExporter.Models;

namespace StudentExporter.Products;

public class XlsxDocument : IExportDocument
{
    public Task Save(string path, List<Student> data, StudentsData meta)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Отчёт");
        
        worksheet.Cell(1, 1).Value = $"Отчёт успеваемости — группа {meta.Group} ({meta.Semester})";
        var titleRange = worksheet.Range(1, 1, 1, 3);
        titleRange.Merge();
        titleRange.Style.Font.Bold = true;
        titleRange.Style.Font.FontSize = 14;
        titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        
        worksheet.Cell(3, 1).Value = "№";
        worksheet.Cell(3, 2).Value = "ФИО студента";
        worksheet.Cell(3, 3).Value = "Оценка";

        var headerRange = worksheet.Range(3, 1, 3, 3);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
        headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        
        int row = 4;
        foreach (var student in data)
        {
            worksheet.Cell(row, 1).Value = student.Id;
            worksheet.Cell(row, 2).Value = student.Name;
            worksheet.Cell(row, 3).Value = student.Grade;
            worksheet.Range(row, 1, row, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            row++;
        }
        
        worksheet.Cell(row + 1, 1).Value = "Всего студентов:";
        worksheet.Cell(row + 1, 2).Value = data.Count;
        worksheet.Cell(row + 2, 1).Value = "Средний балл:";
        worksheet.Cell(row + 2, 2).Value = data.Average(s => s.Grade);
        worksheet.Cell(row + 2, 2).Style.NumberFormat.Format = "0.00";

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(path);

        return Task.CompletedTask;
    }
}
