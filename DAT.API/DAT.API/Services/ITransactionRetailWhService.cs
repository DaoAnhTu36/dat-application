using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services.Warehouse
{
    public interface ITransactionRetailWhService
    {
        Task<ApiResponse<TransactionRetailWhUpdateModelRes>> Update(TransactionRetailWhUpdateModelReq req);

        Task<ApiResponse<TransactionRetailWhCreateModelRes>> Create(TransactionRetailWhCreateModelReq req);

        Task<ApiResponse<TransactionRetailWhDeleteModelRes>> Delete(TransactionRetailWhDeleteModelReq req);

        Task<ApiResponse<TransactionRetailWhListModelRes>> List(TransactionRetailWhListModelReq req);

        Task<ApiResponse<TransactionRetailWhDetailModelRes>> Detail(TransactionRetailWhDetailModelReq req);
    }
}