using StudentExporter.Interfaces;
using StudentExporter.Readers;

namespace StudentExporter.Factories;

public class JsonReaderFactory : ReaderFactory
{
    public override IDataReader CreateReader() => new JsonDataReader();
}

