namespace CsvSharp;
public interface ICsvReader
{
    Task<string> Read(string path, CsvReaderType type);
}
