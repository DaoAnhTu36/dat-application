using System.Diagnostics;
using DAT.Common.Utilities;

namespace DAT.Common.Logger
{
    public static class LoggerFunctionUtility
    {
        public static void CommonLogStart(object a, object? message = null)
        {
            var className = a.GetType().Name;
            var stackTrace = new StackTrace();
            var methodName = stackTrace?.GetFrame(1)?.GetMethod()?.DeclaringType?.Name;
            LoggerService.LogInfo(className, methodName, message != null ? UtilityConvert.SerializeObject(message) : "", true);
        }

        public static void CommonLogEnd(object a, object? message = null)
        {
            var className = a.GetType().Name;
            var stackTrace = new StackTrace();
            var methodName = stackTrace?.GetFrame(1)?.GetMethod()?.DeclaringType?.Name;
            LoggerService.LogInfo(className, methodName, message != null ? UtilityConvert.SerializeObject(message) : "", false);
        }
    }
}