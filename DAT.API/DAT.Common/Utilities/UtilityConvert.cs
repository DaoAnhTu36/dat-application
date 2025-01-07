using Newtonsoft.Json;

namespace DAT.Common.Utilities
{
    public static class UtilityConvert
    {
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T? DeserializeObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static string RenameFileUpload(string fileName)
        {
            var createName = DateTime.Now.ToString("ddMMyyyy") + "_" + Guid.NewGuid().ToString();
            var extension = Path.GetExtension(fileName);
            return createName + extension;
        }

        public static string ConvertDatetimeToString(DateTime date)
        {
            return date.ToString("dd/MM/yyyy hh:mm:ss");
        }
    }
}