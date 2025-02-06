using DAT.API.Models;
using DAT.Common.Logger;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Responses;
using DAT.Core;
using DAT.Core.Implementations;
using DAT.Database.Entities.WarehouseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DAT.API.Services.Warehouse.Impl
{
    public class TransactionRetailWhService : Repository<TransactionRetailWhEntity>, ITransactionRetailWhService
    {
        private readonly IOptions<AppConfig> _options;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionRetailWhService(IUnitOfWork unitOfWork
            , DbContext context
            , IOptions<AppConfig> options) : base(context)
        {
            _unitOfWork = unitOfWork;
            _options = options;
        }

        public async Task<ApiResponse<TransactionRetailWhCreateModelRes>> Create(TransactionRetailWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionRetailWhCreateModelRes>();
            try
            {
                foreach (var item in req.Items)
                {
                    var entity = new TransactionRetailWhEntity
                    {
                        GoodsCode = item.GoodsCode,
                        GoodsId = new Guid(item.GoodsId),
                        GoodsName = item.GoodsName,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        TransDetailId = string.IsNullOrEmpty(item.TransDetailId) ? Guid.Empty : new Guid(item.TransDetailId),
                        UnitId = string.IsNullOrEmpty(item.UnitId) ? Guid.Empty : new Guid(item.UnitId),
                        TotalPrice = item.Price * item.Quantity,
                    };
                    _context.Add(entity);
                }
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    Message = ex.Message,
                    StatusCode = "500"
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        public async Task<ApiResponse<TransactionRetailWhDeleteModelRes>> Delete(TransactionRetailWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionRetailWhDeleteModelRes>();
            try
            {
            }
            catch (Exception ex)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    Message = ex.Message,
                    StatusCode = "500"
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        public async Task<ApiResponse<TransactionRetailWhDetailModelRes>> Detail(TransactionRetailWhDetailModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionRetailWhDetailModelRes>();

            try
            {
            }
            catch (Exception ex)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    Message = ex.Message,
                    StatusCode = "500"
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        public async Task<ApiResponse<TransactionRetailWhListModelRes>> List(TransactionRetailWhListModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionRetailWhListModelRes>();
            try
            {
            }
            catch (Exception ex)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    Message = ex.Message,
                    StatusCode = "500"
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        public async Task<ApiResponse<TransactionRetailWhUpdateModelRes>> Update(TransactionRetailWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionRetailWhUpdateModelRes>();
            try
            {
            }
            catch (Exception ex)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    Message = ex.Message,
                    StatusCode = "500"
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        public async Task<ApiResponse<List<TransactionRetailWhStatisticsModelRes>>> Statistics(TransactionRetailWhStatisticsModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<List<TransactionRetailWhStatisticsModelRes>>();
            try
            {
                var query = await _context.Set<TransactionRetailWhEntity>()
                    .Where(x => (req.FromDate == null || x.CreatedDate >= req.FromDate) && (req.ToDate == null || x.CreatedDate <= req.ToDate))
                    .OrderByDescending(x => x.UpdatedDate)
                    .GroupBy(x => x.GoodsCode)
                    .Select(x => new TransactionRetailWhStatisticsModelRes
                    {
                        GoodsCode = x.Key,
                        GoodsId = x.Select(x => x.GoodsId).FirstOrDefault(),
                        GoodsName = x.Select(x => x.GoodsName).FirstOrDefault() ?? "",
                        Price = 0,
                        Quantity = x.Sum(x => x.Quantity),
                        TotalPrice = x.Sum(x => x.Price)
                    })
                    .OrderByDescending(x => x.TotalPrice)
                    .ToListAsync();
                retVal = new ApiResponse<List<TransactionRetailWhStatisticsModelRes>>
                {
                    Data = query
                };
            }
            catch (Exception ex)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    StatusCode = "500",
                    Message = ex.Message
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        public async Task<ApiResponse<List<TransactionRetailWhStatisticsModelRes>>> StatisticsTop5(TransactionRetailWhStatisticsModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<List<TransactionRetailWhStatisticsModelRes>>();
            try
            {
                var query = await _context.Set<TransactionRetailWhEntity>()
                    .Where(x => (req.FromDate == null || x.CreatedDate >= req.FromDate) && (req.ToDate == null || x.CreatedDate <= req.ToDate))
                    .GroupBy(x => x.GoodsCode)
                    .Select(x => new TransactionRetailWhStatisticsModelRes
                    {
                        GoodsCode = x.Key,
                        GoodsId = x.Select(x => x.GoodsId).FirstOrDefault(),
                        GoodsName = x.Select(x => x.GoodsName).FirstOrDefault() ?? "",
                        Price = 0,
                        Quantity = x.Sum(x => x.Quantity),
                        TotalPrice = x.Sum(x => x.Price)
                    })
                    .OrderByDescending(x => x.Quantity)
                    .Take(5)
                    .ToListAsync();
                retVal = new ApiResponse<List<TransactionRetailWhStatisticsModelRes>>
                {
                    Data = query
                };
            }
            catch (Exception ex)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    StatusCode = "500",
                    Message = ex.Message
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }
    }
}