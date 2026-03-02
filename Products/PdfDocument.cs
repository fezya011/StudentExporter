using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using StudentExporter.Interfaces;
using StudentExporter.Models;

namespace StudentExporter.Products;

public class PdfDocument : IExportDocument
{
    public Task Save(string path, List<Student> data, StudentsData meta)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Text($"Отчёт успеваемости — группа {meta.Group} ({meta.Semester})")
                    .Bold().FontSize(16).AlignCenter();

                page.Content().PaddingTop(20).Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem(1).Text("№").Bold();
                        row.RelativeItem(5).Text("ФИО студента").Bold();
                        row.RelativeItem(2).Text("Оценка").Bold();
                    });

                    column.Item().LineHorizontal(1);

                    foreach (var student in data)
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem(1).Text(student.Id.ToString());
                            row.RelativeItem(5).Text(student.Name);
                            row.RelativeItem(2).Text(student.Grade.ToString());
                        });
                    }

                    column.Item().PaddingTop(20).Text($"Всего студентов: {data.Count}");
                    var avg = data.Average(s => s.Grade);
                    column.Item().Text($"Средний балл: {avg:F2}");
                });

                page.Footer().AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Страница ");
                        x.CurrentPageNumber();
                        x.Span(" из ");
                        x.TotalPages();
                    });
            });
        })
        .GeneratePdf(path);

        return Task.CompletedTask;
    }
}
