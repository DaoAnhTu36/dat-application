using DAT.API.Models;
using DAT.Common.Logger;
using DAT.Common.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using DAT.API.Services.Warehouse;

namespace DAT.API.Controllers.Warehouses
{
    [Route("api/wh/goodsprice")]
    public class GoodsPriceWhController : Controller
    {
        private readonly IGoodsPriceWhService _service;

        public GoodsPriceWhController(IGoodsPriceWhService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<ApiResponse<GoodsPriceWhCreateModelRes>> Create([FromBody] GoodsPriceWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsPriceWhCreateModelRes>();
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

        [HttpPost("update")]
        public async Task<ApiResponse<GoodsPriceWhUpdateModelRes>> Update([FromBody] GoodsPriceWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsPriceWhUpdateModelRes>();
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

        [HttpPost("delete")]
        public async Task<ApiResponse<GoodsPriceWhDeleteModelRes>> Delete([FromBody] GoodsPriceWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsPriceWhDeleteModelRes>();
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

        [HttpPost("list")]
        public async Task<ApiResponse<GoodsPriceWhListModelRes>> List([FromBody] GoodsPriceWhListModelReq req)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            LoggerFunctionUtility.CommonLogStart(this, stopwatch);
            var retVal = new ApiResponse<GoodsPriceWhListModelRes>();
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
    }
}