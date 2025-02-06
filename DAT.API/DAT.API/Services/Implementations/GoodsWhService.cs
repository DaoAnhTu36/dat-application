using DAT.API.Models;
using DAT.Common.Logger;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Responses;
using DAT.Common.Users;
using DAT.Common.Utilities;
using DAT.Core;
using DAT.Core.Implementations;
using DAT.Database.Entities.WarehouseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DAT.API.Services.Warehouse.Impl
{
    public class GoodsWhService : Repository<GoodsWhEntity>, IGoodsWhService
    {
        private readonly IOptions<AppConfig> _options;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;

        public GoodsWhService(IUnitOfWork unitOfWork
            , DbContext context
            , IOptions<AppConfig> options
            , IMediaService mediaService) : base(context)
        {
            _unitOfWork = unitOfWork;
            _options = options;
            _mediaService = mediaService;
        }

        public async Task<ApiResponse<GoodsWhCreateModelRes>> Create(GoodsWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsWhCreateModelRes>();
            try
            {
                var recordByBarCode = await _context.Set<GoodsWhEntity>().FirstOrDefaultAsync(x => x.GoodsCode == req.GoodsCode);
                if (recordByBarCode != null)
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        StatusCode = "400"
                    };
                    LoggerFunctionUtility.CommonLogEnd(this, retVal);
                    return retVal;
                }
                var entity = new GoodsWhEntity
                {
                    Name = req.Name,
                    Description = req.Description,
                    GoodsCode = req.GoodsCode,
                    CategoryId = req.CategoryId,
                    Image = req.Image,
                    Price = req.Price,
                };
                _context.Add(entity);
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

        public async Task<ApiResponse<GoodsWhDeleteModelRes>> Delete(GoodsWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsWhDeleteModelRes>();
            try
            {
                var entity = new GoodsWhEntity { Id = req.Id };
                _context.Entry(entity).State = EntityState.Deleted;
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

        public async Task<ApiResponse<GoodsWhDetailModelRes>> Detail(GoodsWhDetailModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsWhDetailModelRes>();

            try
            {
                var query = await (from goods in _context.Set<GoodsWhEntity>()
                                   where goods.Id == req.Id || goods.GoodsCode == req.GoodsCode
                                   join category in _context.Set<CategoryWhEntity>() on goods.CategoryId equals category.Id
                                   select new GoodsWhDetailModelRes
                                   {
                                       Id = goods.Id,
                                       CreatedBy = goods.CreatedBy,
                                       CreatedDate = goods.CreatedDate,
                                       Description = goods.Description,
                                       Name = goods.Name,
                                       UpdatedBy = goods.UpdatedBy,
                                       UpdatedDate = goods.UpdatedDate,
                                       CategoryId = category.Id,
                                       GoodsCode = goods.GoodsCode,
                                       Status = goods.Status,
                                       CategoryName = category.Name,
                                       Price = goods.Price,
                                       Image = goods.Image
                                   }).FirstOrDefaultAsync();
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
                retVal.Data = query;
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

        public async Task<ApiResponse<GoodsWhListModelRes>> List(GoodsWhListModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsWhListModelRes>();
            try
            {
                var query = (from goods in _context.Set<GoodsWhEntity>()
                             join category in _context.Set<CategoryWhEntity>() on goods.CategoryId equals category.Id
                             select new GoodsDetailWhModel
                             {
                                 Id = goods.Id,
                                 CreatedBy = goods.CreatedBy,
                                 CreatedDate = goods.CreatedDate,
                                 Description = goods.Description,
                                 Name = goods.Name,
                                 UpdatedBy = goods.UpdatedBy,
                                 UpdatedDate = goods.UpdatedDate,
                                 GoodsCode = goods.GoodsCode,
                                 CategoryId = goods.CategoryId,
                                 CategoryName = category.Name,
                                 Status = goods.Status,
                                 Image = goods.Image,
                                 Price = goods.Price
                             }).OrderByDescending(x => x.UpdatedDate).AsQueryable();
                retVal = new ApiResponse<GoodsWhListModelRes>
                {
                    Data = new GoodsWhListModelRes
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
                    Message = ex.Message,
                    StatusCode = "500"
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        public async Task<ApiResponse<GoodsWhUpdateModelRes>> Update(GoodsWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsWhUpdateModelRes>();
            try
            {
                var record = await _context.Set<GoodsWhEntity>().FirstOrDefaultAsync(x => x.Id == req.Id);
                if (record == null)
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        Message = "NotFound",
                        StatusCode = "400"
                    };
                    LoggerFunctionUtility.CommonLogEnd(this, retVal);
                    return retVal;
                }
                record.Name = !string.IsNullOrEmpty(req.Name) ? req.Name : record.Name;
                record.Description = !string.IsNullOrEmpty(req.Description) ? req.Name : record.Description;
                record.CategoryId = req.CategoryId != Guid.Empty ? req.CategoryId : record.CategoryId;
                record.Price = req.Price != 0 ? req.Price : record.Price;
                record.Image = !string.IsNullOrEmpty(req.Image) ? req.Image : record.Image;
                record.UpdatedBy = AdminInfo.Id;
                record.UpdatedDate = DateTime.Now;
                _context.Update(record);
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

        public async Task<ApiResponse<GoodsWhSearchListModelRes>> Search(GoodsWhSearchListModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsWhSearchListModelRes>();
            try
            {
                var query = await (from goods in _context.Set<GoodsWhEntity>()
                             join category in _context.Set<CategoryWhEntity>() on goods.CategoryId equals category.Id
                             select new GoodsDetailWhModel
                             {
                                 Id = goods.Id,
                                 CreatedBy = goods.CreatedBy,
                                 CreatedDate = goods.CreatedDate,
                                 Description = goods.Description,
                                 Name = goods.Name,
                                 UpdatedBy = goods.UpdatedBy,
                                 UpdatedDate = goods.UpdatedDate,
                                 GoodsCode = goods.GoodsCode,
                                 CategoryId = goods.CategoryId,
                                 CategoryName = category.Name,
                                 Status = goods.Status,
                                 Image = goods.Image,
                                 Price = goods.Price
                             }).OrderByDescending(x => x.UpdatedDate).ToListAsync();
                query = query.Where(x => string.IsNullOrEmpty(req.TextSearch) || UtilityDatabase.FindMatches(req.TextSearch, x.Name)).ToList();
                retVal = new ApiResponse<GoodsWhSearchListModelRes>
                {
                    Data = new GoodsWhSearchListModelRes
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
                    Message = ex.Message,
                    StatusCode = "500"
                };
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

    }
}