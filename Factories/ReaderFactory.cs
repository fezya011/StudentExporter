using StudentExporter.Interfaces;

namespace StudentExporter.Factories;

public abstract class ReaderFactory
{
    public abstract IDataReader CreateReader();
}
