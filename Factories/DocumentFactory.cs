using StudentExporter.Interfaces;

namespace StudentExporter.Factories;

public abstract class DocumentFactory
{
    public abstract IExportDocument CreateDocument();
}
