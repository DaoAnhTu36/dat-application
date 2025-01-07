using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface IGoodsWhService
    {
        Task<ApiResponse<GoodsWhUpdateModelRes>> Update(GoodsWhUpdateModelReq req);

        Task<ApiResponse<GoodsWhCreateModelRes>> Create(GoodsWhCreateModelReq req);

        Task<ApiResponse<GoodsWhDeleteModelRes>> Delete(GoodsWhDeleteModelReq req);

        Task<ApiResponse<GoodsWhListModelRes>> List(GoodsWhListModelReq req);
        Task<ApiResponse<GoodsWhDetailModelRes>> Detail(GoodsWhDetailModelReq req);
    }
}