using StudentExporter.Interfaces;
using StudentExporter.Products;

namespace StudentExporter.Factories;

public class PdfFactory : DocumentFactory
{
    public override IExportDocument CreateDocument() => new PdfDocument();
}

public class XlsxFactory : DocumentFactory
{
    public override IExportDocument CreateDocument() => new XlsxDocument();
}
