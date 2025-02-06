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
                var query = await _context.Set<GoodsRetailWhEntity>()
                    .OrderByDescending(x => x.UpdatedDate)
                    .GroupBy(x => new
                    {
                        x.GoodsName,
                        x.GoodsCode
                    })
                    .Select(x => new GoodsRetailModelRes
                    {
                        GoodsName = x.Key.GoodsName,
                        GoodsCode = x.Key.GoodsCode,
                        GoodsRetails = x.OrderByDescending(y => y.UpdatedDate).Select(c => new GoodsRetailInstance
                        {
                            GoodsName = c.GoodsName,
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

        public async Task<ApiResponse<GoodsRetailWhSearchModelRes>> Search(GoodsRetailWhSearchModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhSearchModelRes>();
            try
            {
                var query = await _context.Set<GoodsRetailWhEntity>()
                    .OrderByDescending(x => x.UpdatedDate)
                    .GroupBy(x => new
                    {
                        x.GoodsName,
                        x.GoodsCode
                    })
                    .Select(x => new GoodsRetailModelRes
                    {
                        GoodsName = x.Key.GoodsName,
                        GoodsCode = x.Key.GoodsCode,
                        GoodsRetails = x.OrderByDescending(y => y.UpdatedDate).Select(c => new GoodsRetailInstance
                        {
                            GoodsName = c.GoodsName,
                            GoodsId = c.GoodsId,
                            Price = c.Price,
                            TransDetailId = c.TransDetailId,
                            Id = c.Id,
                        })
                    }).ToListAsync();
                query = query.Where(x => (string.IsNullOrEmpty(req.goodsCode) || x.GoodsCode.Contains(req.goodsCode)) && (string.IsNullOrEmpty(req.goodsName) || UtilityDatabase.FindMatches(req.goodsName, x.GoodsName))).ToList();
                retVal = new ApiResponse<GoodsRetailWhSearchModelRes>
                {
                    Data = new GoodsRetailWhSearchModelRes
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

        public async Task<ApiResponse<GoodsRetailWhListModelRes>> ListForMachine()
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhListModelRes>();
            try
            {
                var query = await _context.Set<GoodsRetailWhEntity>().OrderByDescending(x => x.UpdatedDate).GroupBy(x => new
                {
                    x.GoodsName,
                    x.GoodsCode
                }).Select(x => new GoodsRetailModelRes
                {
                    GoodsName = x.Key.GoodsName,
                    GoodsCode = x.Key.GoodsCode,
                    GoodsRetails = x.OrderByDescending(y => y.UpdatedDate).Select(c => new GoodsRetailInstance
                    {
                        GoodsName = c.GoodsName,
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

        public async Task<ApiResponse<GoodsRetailWhHistoryChangeOfPriceModelRes>> HistoryChangeOfPrice(GoodsRetailWhHistoryChangeOfPriceModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhHistoryChangeOfPriceModelRes>();
            try
            {
                var query = await _context.Set<GoodsRetailWhEntity>()
                    .Where(x => x.GoodsId == req.GoodsId
                    && req.FromDate == null || req.FromDate >= x.CreatedDate
                    && req.ToDate == null || req.ToDate <= x.CreatedDate
                    ).Select(x => x.Price).ToListAsync();
                retVal.Data = new GoodsRetailWhHistoryChangeOfPriceModelRes
                {
                    Prices = query
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

        public async Task<ApiResponse<GoodsRetailWhSearchForMachineModelRes>> SearchForMachine(GoodsRetailWhSearchForMachineModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhSearchForMachineModelRes>();
            try
            {
                var query = await _context.Set<GoodsRetailWhEntity>()
                    .OrderByDescending(x => x.UpdatedDate)
                    .GroupBy(x => new
                    {
                        x.GoodsName,
                        x.GoodsCode,
                        x.TransDetailId,
                    })
                    .Select(x => new GoodsRetailModelRes
                    {
                        GoodsName = x.Key.GoodsName,
                        GoodsCode = x.Key.GoodsCode,
                        GoodsRetails = x.OrderByDescending(y => y.UpdatedDate).Select(c => new GoodsRetailInstance
                        {
                            GoodsName = c.GoodsName,
                            GoodsId = c.GoodsId,
                            Price = c.Price,
                            TransDetailId = c.TransDetailId,
                            Id = c.Id,
                        })
                    }).ToListAsync();
                query = query.Where(x => string.IsNullOrEmpty(req.textSearch) || x.GoodsCode.Contains(req.textSearch) || UtilityDatabase.FindMatches(req.textSearch, x.GoodsName)).ToList();
                if (query.Count > 0)
                {
                    var fullData = (from searchResult in query
                                    join transDetail in _context.Set<TransactionDetailWhEntity>() on searchResult.GoodsRetails.FirstOrDefault()?.TransDetailId equals transDetail.Id
                                    join unit in _context.Set<UnitWhEntity>() on transDetail.UnitId equals unit.Id
                                    select new GoodsRetailModelRes
                                    {
                                        GoodsCode = searchResult.GoodsCode,
                                        GoodsName = searchResult.GoodsName,
                                        GoodsRetails = searchResult.GoodsRetails.Select(x => new GoodsRetailInstance
                                        {
                                            Id = x.Id,
                                            GoodsId = x.GoodsId,
                                            GoodsName = x.GoodsName,
                                            Price = x.Price,
                                            TransDetailId = x.TransDetailId,
                                            UnitId = unit.Id,
                                            UnitName = unit.Name ?? ""
                                        })
                                    }).ToList();
                    query = fullData;
                }
                retVal = new ApiResponse<GoodsRetailWhSearchForMachineModelRes>
                {
                    Data = new GoodsRetailWhSearchForMachineModelRes
                    {
                        List = query
                    },
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