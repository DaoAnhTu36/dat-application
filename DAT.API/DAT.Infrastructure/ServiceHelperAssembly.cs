using System.Reflection;

namespace DAT.Infrastructure
{
    public class ServiceHelperAssembly
    {
        public static Assembly Assembly => typeof(ServiceHelperAssembly).Assembly;
    }
}