using Serilog;

namespace DAT.Common.Logger
{
    public static class LoggerService
    {
        public static void LogInfo(string message)
        {
            Log.Information(message);
        }

        public static void LogInfo(string? className, string? methodName, string? message, bool isStart = false)
        {
            message = FormatLogger(className, methodName, message, isStart);
            Log.Information(message);
        }

        public static void LogError(string message, Exception ex)
        {
            if (ex != null)
            {
                Log.Error(ex, message);
            }
            else
            {
                Log.Error(message);
            }
        }

        public static void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public static void LogDebug(string message)
        {
            Log.Debug(message);
        }

        public static void LogWarning(string? className, string? methodName, string? message, bool isStart = false)
        {
            message = FormatLogger(className, methodName, message, isStart);
            Log.Warning(message);
        }

        public static void LogDebug(string? className, string? methodName, string? message, bool isStart = false)
        {
            message = FormatLogger(className, methodName, message, isStart);
            Log.Debug(message);
        }

        private static string FormatLogger(string? className, string? methodName, string? message, bool isStart = false)
        {
            var type = isStart ? "start" : "end";
            return string.Format(StatusLogger.FormatLoggerStr, className, methodName, type, message);
        }
    }

    public static class StatusLogger
    {
        public static string FormatLoggerStr = "CLassName={0}(|)MethodName={1}(|)Status={2}(|)Message={3}";
    }
}