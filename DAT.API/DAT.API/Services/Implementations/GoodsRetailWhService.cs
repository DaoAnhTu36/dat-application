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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                var record = await _context.Set<GoodsRetailWhEntity>().Where(x => x.GoodsCode == req.TextSearch || x.GoodsName.ToLower().Contains(req.TextSearch.ToLower())).OrderByDescending(x => x.UpdatedDate).FirstOrDefaultAsync();
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
                retVal.Data = new GoodsRetailWhSearchlModelRes
                {
                    GoodsName = record.GoodsName,
                    Price = record.Price,
                    UnitName = "",
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