public class FileLogger : ILogger
{
  private string rootPath;
  private static object _lock = new object();
  public FileLogger(string path)
  {
    rootPath = path;
  }
  #pragma warning disable CS8603
  public IDisposable BeginScope<TState>(TState state) => null;

  public bool IsEnabled(LogLevel logLevel) => logLevel == LogLevel.Information;

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
  {
    if (formatter != null)
    {
      lock (_lock)
      {
        string fullFilePath = Path.Combine(rootPath, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
        var n = Environment.NewLine;
        File.AppendAllText(fullFilePath, string.Format("{0:HH:mm}: {1}{2}", DateTime.Now, formatter(state, exception), Environment.NewLine));
      }
    }
  }
}