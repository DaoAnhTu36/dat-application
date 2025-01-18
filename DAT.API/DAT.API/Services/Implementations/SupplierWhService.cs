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
    public class SupplierWhService : Repository<SupplierWhEntity>, ISupplierWhService
    {
        private readonly IOptions<AppConfig> _options;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierWhService(IUnitOfWork unitOfWork
            , DbContext context
            , IOptions<AppConfig> options) : base(context)
        {
            _unitOfWork = unitOfWork;
            _options = options;
        }

        public async Task<ApiResponse<SupplierWhCreateModelRes>> Create(SupplierWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<SupplierWhCreateModelRes>();
            try
            {
                var entity = new SupplierWhEntity
                {
                    Address = req.Address,
                    Name = req.Name
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

        public async Task<ApiResponse<SupplierWhDeleteModelRes>> Delete(SupplierWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<SupplierWhDeleteModelRes>();
            try
            {
                var entity = new SupplierWhEntity { Id = req.Id };
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

        public async Task<ApiResponse<SupplierWhDetailModelRes>> Detail(SupplierWhDetailModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<SupplierWhDetailModelRes>();

            try
            {
                var query = await _context.Set<SupplierWhEntity>().Where(x => x.Id == req.Id).Select(x => new SupplierWhDetailModelRes
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    Address = x.Address,
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

        public async Task<ApiResponse<SupplierWhListModelRes>> List(SupplierWhListModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<SupplierWhListModelRes>();
            try
            {
                var query = _context.Set<SupplierWhEntity>()
                    .OrderByDescending(x => x.UpdatedDate)
                    .Select(x => new SupplierModel
                    {
                        UpdatedDate = x.UpdatedDate,
                        UpdatedBy = x.UpdatedBy,
                        Id = x.Id,
                        CreatedDate = x.CreatedDate,
                        Address = x.Address,
                        CreatedBy = x.CreatedBy,
                        Name = x.Name
                    });
                retVal = new ApiResponse<SupplierWhListModelRes>
                {
                    Data = new SupplierWhListModelRes
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

        public async Task<ApiResponse<SupplierWhUpdateModelRes>> Update(SupplierWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<SupplierWhUpdateModelRes>();
            try
            {
                var record = await _context.Set<SupplierWhEntity>().FirstOrDefaultAsync(x => x.Id == req.Id);
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
                record.Address = !string.IsNullOrEmpty(req.Address) ? req.Address : record.Address;
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
    }
}