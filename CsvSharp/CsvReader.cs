using System.Text;

namespace CsvSharp;
public enum CsvReaderType
{
    FileSystem,
    Url
}

public class CsvReader : ICsvReader
{
    private static readonly HttpClient HttpClient = new HttpClient();

    public Task<string> Read(string path, CsvReaderType type) => type switch
    {
        CsvReaderType.FileSystem => ReadFromFileSystem(path),
        CsvReaderType.Url => ReadFromUrl(path),
        _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unsupported CsvReaderType: {type}")
    };

    private async Task<string> ReadFromFileSystem(string path)
    {
        if (Path.GetExtension(path) != ".csv")
        {
            throw new InvalidOperationException("Only CSV files are supported.");
        }

        var fileBytes = await File.ReadAllBytesAsync(path);
        return Encoding.UTF8.GetString(fileBytes);
    }

    private async Task<string> ReadFromUrl(string url)
    {
        var response = await HttpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to fetch CSV from URL: {url}");
        }

        if (response.Content.Headers.ContentType?.MediaType != "text/csv")
        {
            throw new InvalidOperationException("Only CSV files are supported.");
        }

        return await response.Content.ReadAsStringAsync();
    }
}


