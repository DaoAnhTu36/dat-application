using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface IGoodsRetailWhService
    {
        Task<ApiResponse<GoodsRetailWhUpdateModelRes>> Update(GoodsRetailWhUpdateModelReq req);

        Task<ApiResponse<GoodsRetailWhCreateModelRes>> Create(GoodsRetailWhCreateModelReq req);

        Task<ApiResponse<GoodsRetailWhDeleteModelRes>> Delete(GoodsRetailWhDeleteModelReq req);

        Task<ApiResponse<GoodsRetailWhListModelRes>> List(GoodsRetailWhListModelReq req);
        Task<ApiResponse<GoodsRetailWhSearchlModelRes>> Search(GoodsRetailWhSearchlModelReq req);
    }
}