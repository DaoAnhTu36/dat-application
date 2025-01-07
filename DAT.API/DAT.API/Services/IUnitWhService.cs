using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface IUnitWhService
    {
        Task<ApiResponse<UnitWhUpdateModelRes>> Update(UnitWhUpdateModelReq req);

        Task<ApiResponse<UnitWhCreateModelRes>> Create(UnitWhCreateModelReq req);

        Task<ApiResponse<UnitWhDeleteModelRes>> Delete(UnitWhDeleteModelReq req);

        Task<ApiResponse<UnitWhListModelRes>> List(UnitWhListModelReq req);
        Task<ApiResponse<UnitWhDetailModelRes>> Detail(UnitWhDetailModelReq req);
    }
}