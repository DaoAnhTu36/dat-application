using System.Text.RegularExpressions;

internal class Program
{
    public static string pathCommon = "E:\\code\\dat-application\\DAT.API\\DAT.API";
    private static void Main(string[] args)
    {
        var arrClass = new string[]
        {
            //"Inventory", "Goods", "Supplier", "Transaction", "Unit", "Category", "Order", "Stock"
            "TransactionRetail"
        };
        GenModels(arrClass);
        GenController(arrClass);
        GenService(arrClass);
        GenInterface(arrClass);
    }

    public static void GenModels(string[] arrClass)
    {
        var content = File.ReadAllText("template_model.txt");
        foreach (var item in arrClass)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string filePath = $"{pathCommon}\\Models\\{item}WhModels.cs";
                var contenNew = Regex.Replace(content, @"_ClassName_", item);
                File.WriteAllText(filePath, contenNew);
            }
        }
    }

    public static void GenController(string[] arrClass)
    {
        var content = File.ReadAllText("template_controller.txt");
        foreach (var item in arrClass)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string filePath = $"{pathCommon}\\Controllers\\{item}WhController.cs";
                var contenNew = Regex.Replace(content, @"_ClassName_", item);
                contenNew = Regex.Replace(contenNew, @"route", item.ToLower());
                File.WriteAllText(filePath, contenNew);
            }
        }
    }

    public static void GenInterface(string[] arrClass)
    {
        var content = File.ReadAllText("template_interface.txt");
        foreach (var item in arrClass)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string filePath = $"{pathCommon}\\Services\\I{item}WhService.cs";
                var contenNew = Regex.Replace(content, @"_ClassName_", item);
                File.WriteAllText(filePath, contenNew);
            }
        }
    }

    public static void GenService(string[] arrClass)
    {
        var content = File.ReadAllText("template_service.txt");
        foreach (var item in arrClass)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string filePath = $"{pathCommon}\\Services\\Implementations\\{item}WhService.cs";
                var contenNew = Regex.Replace(content, @"_ClassName_", item);
                File.WriteAllText(filePath, contenNew);
            }
        }
    }
}