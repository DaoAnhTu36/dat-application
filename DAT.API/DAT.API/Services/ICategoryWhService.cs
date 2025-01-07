using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface ICategoryWhService
    {
        Task<ApiResponse<CategoryWhUpdateModelRes>> Update(CategoryWhUpdateModelReq req);

        Task<ApiResponse<CategoryWhCreateModelRes>> Create(CategoryWhCreateModelReq req);

        Task<ApiResponse<CategoryWhDeleteModelRes>> Delete(CategoryWhDeleteModelReq req);

        Task<ApiResponse<CategoryWhListModelRes>> List(CategoryWhListModelReq req);
    }
}