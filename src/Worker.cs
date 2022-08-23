namespace sessionLogger;

using System.Runtime.Versioning;
using Microsoft.Win32;
public class Worker : BackgroundService
{
  private readonly ILogger<Worker> _logger;

  public Worker(ILogger<Worker> logger)
  {
    _logger = logger;
  }


  [SupportedOSPlatform("windows")]
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    SystemEvents.SessionSwitch += SessionSwitchEventHandler;
    while (!stoppingToken.IsCancellationRequested)
    {
      await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
    }
    SystemEvents.SessionSwitch -= SessionSwitchEventHandler;
  }

  [SupportedOSPlatform("windows")]
  private void SessionSwitchEventHandler(object sender, SessionSwitchEventArgs e)
  {
    switch (e.Reason)
    {
      case SessionSwitchReason.SessionLock:
        WriteEntry("lock");
        break;
      case SessionSwitchReason.SessionLogoff:
        WriteEntry("logoff");
        break;
      case SessionSwitchReason.SessionUnlock:
        WriteEntry("unlock");
        break;

      case SessionSwitchReason.SessionLogon:
        WriteEntry("logon");
        break;
    }
  }

  private void WriteEntry(string evt)
  {
    _logger.LogInformation(new EventId(), "{0} {1}", evt, Environment.UserName);
  }
}
