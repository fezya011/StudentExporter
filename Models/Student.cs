using System.Text.Json.Serialization;

namespace StudentExporter.Models;

public class Student
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("grade")]
    public int Grade { get; set; }
}

public class StudentsData
{
    [JsonPropertyName("group")]
    public string Group { get; set; } = string.Empty;

    [JsonPropertyName("semester")]
    public string Semester { get; set; } = string.Empty;

    [JsonPropertyName("students")]
    public List<Student> Students { get; set; } = new();
}
