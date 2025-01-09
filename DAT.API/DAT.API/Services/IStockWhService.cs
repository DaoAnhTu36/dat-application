using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface IStockWhService
    {
        Task<ApiResponse<StockWhUpdateModelRes>> Update(StockWhUpdateModelReq req);

        Task<ApiResponse<StockWhCreateModelRes>> Create(StockWhCreateModelReq req);

        Task<ApiResponse<StockWhDeleteModelRes>> Delete(StockWhDeleteModelReq req);

        Task<ApiResponse<StockWhListModelRes>> List(StockWhListModelReq req);
        Task<ApiResponse<StockWhDetailModelRes>> Detail(StockWhDetailModelReq req);
        Task<ApiResponse<StockWhSearchListModelRes>> Search(StockWhSearchListModelReq req);
    }
}