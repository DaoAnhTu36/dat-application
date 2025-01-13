using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        var arrClass = new string[]
        {
            //"Inventory", "Goods", "Supplier", "Transaction", "Unit", "Category", "Order", "Stock"
            "GoodsRetail"
        };
        GenModels(arrClass);
        GenController(arrClass);
        GenService(arrClass);
        GenInterface(arrClass);
    }

    public static void GenModels(string[] arrClass)
    {
        var content = File.ReadAllText("template_gen_model.txt");
        foreach (var item in arrClass)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string filePath = $"D:\\Woks\\du-an-1\\dat-application\\DAT.API\\DAT.API\\Models\\{item}WhModels.cs";
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
                string filePath = $"D:\\Woks\\du-an-1\\dat-application\\DAT.API\\DAT.API\\Controllers\\{item}WhController.cs";
                var contenNew = Regex.Replace(content, @"_ClassName_", item);
                contenNew = Regex.Replace(contenNew, @"route", item.ToLower());
                File.WriteAllText(filePath, contenNew);
            }
        }
    }

    public static void GenInterface(string[] arrClass)
    {
        var content = File.ReadAllText("template_gen_interface.txt");
        foreach (var item in arrClass)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string filePath = $"D:\\Woks\\du-an-1\\dat-application\\DAT.API\\DAT.API\\Services\\I{item}WhService.cs";
                var contenNew = Regex.Replace(content, @"_ClassName_", item);
                File.WriteAllText(filePath, contenNew);
            }
        }
    }

    public static void GenService(string[] arrClass)
    {
        var content = File.ReadAllText("template_gen_service.txt");
        foreach (var item in arrClass)
        {
            if (!string.IsNullOrEmpty(item))
            {
                string filePath = $"D:\\Woks\\du-an-1\\dat-application\\DAT.API\\DAT.API\\Services\\Implementations\\{item}WhService.cs";
                var contenNew = Regex.Replace(content, @"_ClassName_", item);
                File.WriteAllText(filePath, contenNew);
            }
        }
    }
}