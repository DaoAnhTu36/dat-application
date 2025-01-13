using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface IGoodsPriceWhService
    {
        Task<ApiResponse<GoodsPriceWhUpdateModelRes>> Update(GoodsPriceWhUpdateModelReq req);

        Task<ApiResponse<GoodsPriceWhCreateModelRes>> Create(GoodsPriceWhCreateModelReq req);

        Task<ApiResponse<GoodsPriceWhDeleteModelRes>> Delete(GoodsPriceWhDeleteModelReq req);

        Task<ApiResponse<GoodsPriceWhListModelRes>> List(GoodsPriceWhListModelReq req);
    }
}