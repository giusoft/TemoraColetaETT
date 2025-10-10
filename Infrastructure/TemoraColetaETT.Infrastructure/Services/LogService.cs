namespace TemoraColetaETT.Infrastructure.Services;
using Serilog;
using Serilog.Core;
using Serilog.Configuration;

public static class LogService
{
    private static Logger? _logger;

    public static void Initialize()
    {
        if (_logger != null) return;

        string logDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Logs"
        );
        Directory.CreateDirectory(logDir);

        string logPath = Path.Combine(logDir, "log-.txt");

        _logger = new LoggerConfiguration()
            .MinimumLevel.Information() // Nível mínimo de log
            .WriteTo.File(logPath, rollingInterval: RollingInterval.Day) // Escreve para um arquivo, um novo por dia
            .CreateLogger();

        _logger.Information("Logger inicializado.");
    }

    public static void LogInfo(string message) => _logger?.Information(message);
    public static void LogWarning(string message) => _logger?.Warning(message);
    public static void LogError(string message, Exception? ex = null) => _logger?.Error(ex, message);
}