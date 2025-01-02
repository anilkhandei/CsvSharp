namespace CsvSharp;

public class CsvHelper
{
    private readonly ICsvReader _csvReader;

    public CsvHelper(ICsvReader csvReader)
    {
        _csvReader = csvReader;
    }

    public Task<string> ReadCsv(string path, CsvReaderType type) => _csvReader.Read(path, type);
}