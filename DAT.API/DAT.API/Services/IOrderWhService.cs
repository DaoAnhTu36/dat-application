using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface IOrderWhService
    {
        Task<ApiResponse<OrderWhUpdateModelRes>> Update(OrderWhUpdateModelReq req);

        Task<ApiResponse<OrderWhCreateModelRes>> Create(OrderWhCreateModelReq req);

        Task<ApiResponse<OrderWhDeleteModelRes>> Delete(OrderWhDeleteModelReq req);

        Task<ApiResponse<OrderWhListModelRes>> List(OrderWhListModelReq req);
    }
}