namespace Tests.DataTests;

public class TempFile : IDisposable
{
    public string Path { get; }

    public TempFile(string extension)
    {
        Path = System.IO.Path.GetTempFileName() + extension;
    }

    public void Dispose()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
}