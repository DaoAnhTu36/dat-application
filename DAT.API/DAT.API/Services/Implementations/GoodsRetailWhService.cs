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
    public class GoodsRetailWhService : Repository<GoodsRetailWhEntity>, IGoodsRetailWhService
    {
        private readonly IOptions<AppConfig> _options;
        private readonly IUnitOfWork _unitOfWork;

        public GoodsRetailWhService(IUnitOfWork unitOfWork
            , DbContext context
            , IOptions<AppConfig> options) : base(context)
        {
            _unitOfWork = unitOfWork;
            _options = options;
        }

        public async Task<ApiResponse<GoodsRetailWhDetailModelRes>> Detail(GoodsRetailWhDetailModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhDetailModelRes>();
            try
            {
                var record = await _context.Set<GoodsRetailWhEntity>().Where(x => x.GoodsCode == req.GoodsCode).OrderByDescending(x => x.UpdatedDate).FirstOrDefaultAsync();
                if (record == null)
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        StatusCode = "400"
                    };
                    LoggerFunctionUtility.CommonLogEnd(this, retVal);
                    return retVal;
                }
                retVal.Data = new GoodsRetailWhDetailModelRes
                {
                    GoodsName = record.GoodsName,
                    Price = record.Price,
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

        public async Task<ApiResponse<GoodsRetailWhCreateModelRes>> Create(GoodsRetailWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhCreateModelRes>();
            try
            {
                foreach (var record in req.ListReq)
                {
                    _context.Add(new GoodsRetailWhEntity
                    {
                        GoodsId = record.GoodsId,
                        GoodsCode = record.GoodsCode,
                        GoodsName = record.GoodsName,
                        Price = record.Price,
                        TransDetailId = record.TransDetailId,
                    });
                }
                await _unitOfWork.SaveChangesAsync();
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

        public async Task<ApiResponse<GoodsRetailWhDeleteModelRes>> Delete(GoodsRetailWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhDeleteModelRes>();
            try
            {
            }
            catch (Exception ex)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    StatusCode = "500"
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        public async Task<ApiResponse<GoodsRetailWhListModelRes>> List(GoodsRetailWhListModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhListModelRes>();
            try
            {
                var query = await _context.Set<GoodsRetailWhEntity>().OrderByDescending(x => x.UpdatedDate).GroupBy(x => x.GoodsName).Select(x => new GoodsRetailModelRes
                {
                    GoodsName = x.Key,
                    GoodsRetails = x.OrderByDescending(y => y.UpdatedDate).Select(c => new GoodsRetailInstance
                    {
                        GoodsName = c.GoodsName,
                        GoodsCode = c.GoodsCode,
                        GoodsId = c.GoodsId,
                        Price = c.Price,
                        TransDetailId = c.TransDetailId,
                        Id = c.Id,
                    })
                }).ToListAsync();
                retVal = new ApiResponse<GoodsRetailWhListModelRes>
                {
                    Data = new GoodsRetailWhListModelRes
                    {
                        List = UtilityDatabase.PaginationExtension(_options, query, req.PageNumber, req.PageSize, out int totalPage, out int currentPage)
                    },
                    PageInfo = new PageInfo
                    {
                        CurrentPage = currentPage,
                        TotalPage = totalPage
                    }
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

        public async Task<ApiResponse<GoodsRetailWhUpdateModelRes>> Update(GoodsRetailWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhUpdateModelRes>();
            try
            {
                var record = await _context.Set<GoodsRetailWhEntity>().FirstOrDefaultAsync(x => x.Id == req.Id);
                if (record == null)
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        StatusCode = "400",
                    };
                    LoggerFunctionUtility.CommonLogEnd(this, retVal);
                    return retVal;
                }
                record.Price = req.Price;
                record.UpdatedDate = DateTime.Now;
                _context.Update(record);
                await _unitOfWork.SaveChangesAsync();
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

        public async Task<ApiResponse<GoodsRetailWhSearchlModelRes>> Search(GoodsRetailWhSearchlModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhSearchlModelRes>();
            try
            {
                var query = await (from goodsRetail in _context.Set<GoodsRetailWhEntity>()
                                   where goodsRetail.GoodsCode == req.TextSearch || goodsRetail.GoodsName.ToLower().Contains(req.TextSearch.ToLower())
                                   join transDetail in _context.Set<TransactionDetailWhEntity>() on goodsRetail.TransDetailId equals transDetail.Id
                                   join unit in _context.Set<UnitWhEntity>() on transDetail.UnitId equals unit.Id
                                   orderby goodsRetail.UpdatedDate descending
                                   select new GoodsRetailWhSearchlModelRes
                                   {
                                       GoodsCode = goodsRetail.GoodsCode,
                                       GoodsId = goodsRetail.GoodsId.ToString(),
                                       GoodsName = goodsRetail.GoodsName,
                                       Price = goodsRetail.Price,
                                       TransDetailId = transDetail.Id.ToString(),
                                       UnitId = transDetail.UnitId.ToString(),
                                       UnitName = unit.Name ?? ""
                                   }).FirstOrDefaultAsync();
                if (query == null)
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        StatusCode = "400"
                    };
                    LoggerFunctionUtility.CommonLogEnd(this, retVal);
                    return retVal;
                }
                retVal.Data = query;
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

        public async Task<ApiResponse<GoodsRetailWhListModelRes>> ListForMachine()
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhListModelRes>();
            try
            {
                var query = await _context.Set<GoodsRetailWhEntity>().OrderByDescending(x => x.UpdatedDate).GroupBy(x => x.GoodsName).Select(x => new GoodsRetailModelRes
                {
                    GoodsName = x.Key,
                    GoodsRetails = x.OrderByDescending(y => y.UpdatedDate).Select(c => new GoodsRetailInstance
                    {
                        GoodsName = c.GoodsName,
                        GoodsCode = c.GoodsCode,
                        GoodsId = c.GoodsId,
                        Price = c.Price,
                        TransDetailId = c.TransDetailId,
                        Id = c.Id,
                    })
                }).ToListAsync();
                retVal = new ApiResponse<GoodsRetailWhListModelRes>
                {
                    Data = new GoodsRetailWhListModelRes
                    {
                        List = query
                    }
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

        public async Task<ApiResponse<List<GoodsRetailWhStatisticsModelRes>>> Statistics(GoodsRetailWhStatisticsModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<List<GoodsRetailWhStatisticsModelRes>>();
            try
            {
                var query = await _context.Set<GoodsRetailWhEntity>()
                    .Where(x=> (req.FromDate == null || x.CreatedDate >= req.FromDate) && (req.ToDate == null || x.CreatedDate <= req.ToDate))
                    .OrderByDescending(x => x.UpdatedDate)
                    .GroupBy(x => x.GoodsCode)
                    .Select(x => new GoodsRetailWhStatisticsModelRes
                    {
                        GoodsCode = x.Key,
                        GoodsId = x.Select(x => x.GoodsId).FirstOrDefault(),
                        GoodsName = x.Select(x => x.GoodsName).FirstOrDefault() ?? "",
                        Price = 0,
                        Quantity = x.Count(),
                        TotalPrice = x.Sum(x => x.Price)
                    })
                    .OrderByDescending(x => x.TotalPrice)
                    .ToListAsync();
                retVal = new ApiResponse<List<GoodsRetailWhStatisticsModelRes>>
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