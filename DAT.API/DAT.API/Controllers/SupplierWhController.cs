using DAT.API.Models;
using DAT.Common.Logger;
using DAT.Common.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using DAT.API.Services.Warehouse;

namespace DAT.API.Controllers.Warehouses
{
    [Route("api/wh/supplier")]
    public class SupplierWhController : Controller
    {
        private readonly ISupplierWhService _service;

        public SupplierWhController(ISupplierWhService service)
        {
            _service = service;
        }

        [HttpPost("supplier-create")]
        public async Task<ApiResponse<SupplierWhCreateModelRes>> Create([FromBody] SupplierWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<SupplierWhCreateModelRes>();
            if (!ModelState.IsValid)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    StatusCode = "400"
                };
                LoggerFunctionUtility.CommonLogEnd(this, retVal);
                return retVal;
            }
            retVal = await _service.Create(req);
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        [HttpPost("supplier-update")]
        public async Task<ApiResponse<SupplierWhUpdateModelRes>> Update([FromBody] SupplierWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<SupplierWhUpdateModelRes>();
            if (!ModelState.IsValid)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    StatusCode = "400"
                };
                LoggerFunctionUtility.CommonLogEnd(this, retVal);
                return retVal;
            }
            retVal = await _service.Update(req);
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        [HttpPost("supplier-delete")]
        public async Task<ApiResponse<SupplierWhDeleteModelRes>> Delete([FromBody] SupplierWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<SupplierWhDeleteModelRes>();
            if (!ModelState.IsValid)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    StatusCode = "400"
                };
                LoggerFunctionUtility.CommonLogEnd(this, retVal);
                return retVal;
            }
            retVal = await _service.Delete(req);
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        [HttpPost("supplier-list")]
        public async Task<ApiResponse<SupplierWhListModelRes>> List([FromBody] SupplierWhListModelReq req)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            LoggerFunctionUtility.CommonLogStart(this, stopwatch);
            var retVal = new ApiResponse<SupplierWhListModelRes>();
            if (!ModelState.IsValid)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    StatusCode = "400"
                };
                LoggerFunctionUtility.CommonLogEnd(this, retVal);
                return retVal;
            }
            retVal = await _service.List(req);
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        [HttpPost("supplier-detail")]
        public async Task<ApiResponse<SupplierWhDetailModelRes>> Detail([FromBody] SupplierWhDetailModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this, req);
            var retVal = new ApiResponse<SupplierWhDetailModelRes>();
            if (!ModelState.IsValid)
            {
                retVal.IsNormal = false;
                retVal.MetaData = new MetaData
                {
                    Message = "Model invalid",
                    StatusCode = "400"
                };
                LoggerFunctionUtility.CommonLogEnd(this, retVal);
                return retVal;
            }
            retVal = await _service.Detail(req);
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }
    }
}