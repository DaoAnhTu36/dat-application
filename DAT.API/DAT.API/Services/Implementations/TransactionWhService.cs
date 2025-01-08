using DAT.API.Models;
using DAT.Common.Logger;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Responses;
using DAT.Common.Utilities;
using DAT.Core;
using DAT.Core.Implementations;
using DAT.Database.Entities.WarehouseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DAT.API.Services.Warehouse.Impl
{
    public class TransactionWhService : Repository<TransactionWhEntity>, ITransactionWhService
    {
        private readonly IOptions<AppConfig> _options;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionWhService(IUnitOfWork unitOfWork
            , DbContext context
            , IOptions<AppConfig> options) : base(context)
        {
            _unitOfWork = unitOfWork;
            _options = options;
        }

        public async Task<ApiResponse<TransactionWhCreateModelRes>> Create(TransactionWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionWhCreateModelRes>();
            try
            {
                using var transactionDB = await _context.Database.BeginTransactionAsync();

                var transId = Guid.NewGuid();
                _context.Add(new TransactionWhEntity
                {
                    Id = transId,
                    TransactionType = req.TransactionType,
                    TransactionDate = req.TransactionDate,
                    TotalPrice = req.TotalPrice,
                    TransactionCode = req.TransactionCode,
                    StockId = req.StockId
                });

                if (req.Details != null)
                {
                    foreach (var transaction in req.Details)
                    {
                        _context.Add(new TransactionDetailWhEntity
                        {
                            UnitPrice = transaction.UnitPrice,
                            DateOfExpired = transaction.DateOfExpired,
                            DateOfManufacture = transaction.DateOfManufacture,
                            GoodsId = transaction.GoodsId,
                            Quantity = transaction.Quantity,
                            TransactionId = transId,
                            TotalPrice = transaction.TotalPrice,
                            SupplierId = transaction.SupplierId,
                            UnitId = transaction.UnitId,
                        });
                    }
                }
                await _unitOfWork.SaveChangesAsync();

                await transactionDB.CommitAsync();
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

        public async Task<ApiResponse<TransactionWhDeleteModelRes>> Delete(TransactionWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionWhDeleteModelRes>();
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

        public async Task<ApiResponse<TransactionWhDetailModelRes>> Detail(TransactionWhDetailModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionWhDetailModelRes>();

            try
            {
                var record = await (from trans in _context.Set<TransactionWhEntity>()
                                    where trans.Id == req.Id
                                    join transDetail in _context.Set<TransactionDetailWhEntity>() on trans.Id equals transDetail.TransactionId
                                    join stocks in _context.Set<StockWhEntity>() on trans.StockId equals stocks.Id
                                    join prod in _context.Set<GoodsWhEntity>() on transDetail.GoodsId equals prod.Id
                                    join supplier in _context.Set<SupplierWhEntity>() on transDetail.SupplierId equals supplier.Id
                                    join unit in _context.Set<UnitWhEntity>() on transDetail.UnitId equals unit.Id
                                    select new
                                    {
                                        TransId = trans.Id,
                                        TransactionDate = trans.TransactionDate,
                                        TransactionType = trans.TransactionType,
                                        TransactionCode = trans.TransactionCode,
                                        TotalPriceTrans = trans.TotalPrice,
                                        CreatedByTrans = trans.CreatedBy,
                                        CreatedDateTrans = trans.CreatedDate,
                                        UpdatedByTrans = trans.UpdatedBy,
                                        UpdatedDateTrans = trans.UpdatedDate,
                                        GoodsName = prod.Name,
                                        GoodsCode = prod.GoodsCode,
                                        SupplierName = supplier.Name,
                                        UnitPrice = transDetail.UnitPrice,
                                        TotalPriceTransDetail = transDetail.TotalPrice,
                                        Quantity = transDetail.Quantity,
                                        DateOfManufacture = transDetail.DateOfManufacture,
                                        DateOfExpired = transDetail.DateOfExpired,
                                        CreatedByTransDetail = transDetail.CreatedBy,
                                        CreatedDateTransDetail = transDetail.CreatedDate,
                                        UpdatedByTransDetail = transDetail.UpdatedBy,
                                        UpdatedDateTransDetail = transDetail.UpdatedDate,
                                        UnitName = unit.Name,
                                        StockId = stocks.Id,
                                        StockName = stocks.Name,
                                    }
                             into tableNew
                                    group tableNew by new
                                    {
                                        tableNew.TransId,
                                        tableNew.TransactionDate,
                                        tableNew.TransactionType,
                                        tableNew.TransactionCode,
                                        tableNew.TotalPriceTrans,
                                        tableNew.CreatedByTrans,
                                        tableNew.CreatedDateTrans,
                                        tableNew.UpdatedByTrans,
                                        tableNew.UpdatedDateTrans,
                                        tableNew.StockId,
                                        tableNew.StockName,
                                    }
                             into tableNewGroup
                                    select new TransactionWhDetailModelRes
                                    {
                                        TotalPrice = tableNewGroup.Key.TotalPriceTrans,
                                        TransactionCode = tableNewGroup.Key.TransactionCode,
                                        TransactionDate = tableNewGroup.Key.TransactionDate,
                                        TransactionType = tableNewGroup.Key.TransactionType,
                                        CreatedBy = tableNewGroup.Key.CreatedByTrans,
                                        CreatedDate = tableNewGroup.Key.CreatedDateTrans,
                                        UpdatedBy = tableNewGroup.Key.UpdatedByTrans,
                                        UpdatedDate = tableNewGroup.Key.UpdatedDateTrans,
                                        Details = tableNewGroup.Select(x => new TransactionDetailModels
                                        {
                                            DateOfExpired = x.DateOfExpired,
                                            DateOfManufacture = x.DateOfManufacture,
                                            GoodsName = x.GoodsName,
                                            Quantity = x.Quantity,
                                            SupplierName = x.SupplierName,
                                            UnitPrice = x.UnitPrice,
                                            TotalPrice = x.TotalPriceTransDetail,
                                            UpdatedDate = x.UpdatedDateTransDetail,
                                            UpdatedBy = x.UpdatedByTransDetail,
                                            CreatedDate = x.CreatedDateTransDetail,
                                            CreatedBy = x.CreatedByTransDetail,
                                            GoodsCode = x.GoodsCode,
                                            UnitName = x.UnitName,
                                        }).ToList()
                                    }).FirstOrDefaultAsync();
                retVal.Data = record;
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

        public async Task<ApiResponse<TransactionWhListModelRes>> List(TransactionWhListModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this, req);
            var retVal = new ApiResponse<TransactionWhListModelRes>();
            try
            {
                var query = (from trans in _context.Set<TransactionWhEntity>()
                            where string.IsNullOrEmpty(req.TransactionType) || trans.TransactionType == req.TransactionType
                            join stock in _context.Set<StockWhEntity>() on trans.StockId equals stock.Id
                            select new TransactionWhModel
                            {
                                TotalPrice = trans.TotalPrice,
                                CreatedBy = trans.CreatedBy,
                                CreatedDate = trans.CreatedDate,
                                Id = trans.Id,
                                Status = trans.Status,
                                TransactionCode = trans.TransactionCode,
                                TransactionDate = trans.TransactionDate,
                                TransactionType = trans.TransactionType,
                                UpdatedBy = trans.UpdatedBy,
                                UpdatedDate = trans.UpdatedDate,
                                StockId = stock.Id,
                                StockName = stock.Name ?? ""
                            }).AsQueryable();
                retVal = new ApiResponse<TransactionWhListModelRes>
                {
                    Data = new TransactionWhListModelRes
                    {
                        List = UtilityDatabase.PaginationExtension(_options, query, req.PageNumber, req.PageSize)
                    }
                };
                if (query == null)
                {
                    retVal.MetaData = new MetaData
                    {
                        Message = "NotFound",
                        StatusCode = "400"
                    };
                    LoggerFunctionUtility.CommonLogEnd(this, retVal);
                    return retVal;
                }
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

        public async Task<ApiResponse<TransactionWhUpdateModelRes>> Update(TransactionWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<TransactionWhUpdateModelRes>();
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
    }
}