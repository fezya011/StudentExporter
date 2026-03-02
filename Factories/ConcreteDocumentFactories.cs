using StudentExporter.Interfaces;
using StudentExporter.Products;

namespace StudentExporter.Factories;

public class PdfFactory : DocumentFactory
{
    public override IExportDocument CreateDocument() => new PdfDocument();
}

public class TxtFactory : DocumentFactory
{
    public override IExportDocument CreateDocument() => new TxtDocument();
}

public class XlsxFactory : DocumentFactory
{
    public override IExportDocument CreateDocument() => new XlsxDocument();
}
