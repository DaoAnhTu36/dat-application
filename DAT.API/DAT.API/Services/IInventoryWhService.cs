using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface IInventoryWhService
    {
        Task<ApiResponse<InventoryWhUpdateModelRes>> Update(InventoryWhUpdateModelReq req);

        Task<ApiResponse<InventoryWhCreateModelRes>> Create(InventoryWhCreateModelReq req);

        Task<ApiResponse<InventoryWhDeleteModelRes>> Delete(InventoryWhDeleteModelReq req);

        Task<ApiResponse<InventoryWhListModelRes>> List(InventoryWhListModelReq req);
    }
}