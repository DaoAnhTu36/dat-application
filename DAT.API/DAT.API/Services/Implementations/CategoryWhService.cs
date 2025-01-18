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
    public class CategoryWhService : Repository<CategoryWhEntity>, ICategoryWhService
    {
        private readonly IOptions<AppConfig> _options;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryWhService(IUnitOfWork unitOfWork
            , DbContext context
            , IOptions<AppConfig> options) : base(context)
        {
            _unitOfWork = unitOfWork;
            _options = options;
        }

        public async Task<ApiResponse<CategoryWhCreateModelRes>> Create(CategoryWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<CategoryWhCreateModelRes>();
            try
            {
                var record = await _context.Set<CategoryWhEntity>().FirstOrDefaultAsync(x => x.Name == req.Name);
                if (record != null)
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        StatusCode = "400"
                    };
                    LoggerFunctionUtility.CommonLogEnd(this, retVal);
                    return retVal;
                }
                _context.Add(new CategoryWhEntity
                {
                    Name = req.Name,
                });
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

        public async Task<ApiResponse<CategoryWhDeleteModelRes>> Delete(CategoryWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<CategoryWhDeleteModelRes>();
            try
            {
                var entity = new CategoryWhEntity { Id = req.Id };
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

        public async Task<ApiResponse<CategoryWhListModelRes>> List(CategoryWhListModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<CategoryWhListModelRes>();
            try
            {
                var record = _context.Set<CategoryWhEntity>().Select(x => new CategoryWhModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    Status = x.Status,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                });
                retVal = new ApiResponse<CategoryWhListModelRes>
                {
                    Data = new CategoryWhListModelRes
                    {
                        List = UtilityDatabase.PaginationExtension(_options, record, req.PageNumber, req.PageSize, out int totalPage, out int currentPage)
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

        public async Task<ApiResponse<CategoryWhUpdateModelRes>> Update(CategoryWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<CategoryWhUpdateModelRes>();
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