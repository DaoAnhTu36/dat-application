namespace DAT.Common.Models.Responses
{
    public class ApiResponse
    {
        public bool IsNormal { get; set; } = true;

        public MetaData MetaData { get; set; } = new MetaData
        {
            Message = "",
            StatusCode = "200"
        };
    }

    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool IsNormal { get; set; } = true;

        public MetaData MetaData { get; set; } = new MetaData
        {
            Message = "",
            StatusCode = "200"
        };
    }

    public class MetaData
    {
        public string StatusCode { get; set; } = "200";
        public string Message { get; set; } = "";
        //public Exception? ExceptionExtra { get; set; }
    }
}