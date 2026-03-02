using StudentExporter.Interfaces;
using StudentExporter.Readers;

namespace StudentExporter.Factories;

public class JsonReaderFactory : ReaderFactory
{
    public override IDataReader CreateReader() => new JsonDataReader();
}

public class XmlReaderFactory : ReaderFactory
{
    public override IDataReader CreateReader() => new XmlDataReader();
}

public class XlsxReaderFactory : ReaderFactory
{
    public override IDataReader CreateReader() => new XlsxDataReader();
}
