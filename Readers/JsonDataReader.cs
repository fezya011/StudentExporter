using System.Text.Json;
using StudentExporter.Interfaces;
using StudentExporter.Models;

namespace StudentExporter.Readers;

public class JsonDataReader : IDataReader
{
    public async Task<StudentsData> Read(string path)
    {
        await using var stream = File.OpenRead(path);
        var result = await JsonSerializer.DeserializeAsync<StudentsData>(stream);
        return result ?? throw new InvalidDataException("Не удалось десериализовать JSON-файл.");
    }
}
