using System.Reflection;

namespace DAT.Core
{
    public class ServiceCoreAssembly
    {
        public static Assembly Assembly => typeof(ServiceCoreAssembly).Assembly;
    }
}