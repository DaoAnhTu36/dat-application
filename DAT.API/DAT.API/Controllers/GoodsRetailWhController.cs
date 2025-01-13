using DAT.API.Models;
using DAT.Common.Logger;
using DAT.Common.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using DAT.API.Services.Warehouse;

namespace DAT.API.Controllers.Warehouses
{
    [Route("api/wh/goodsretail")]
    public class GoodsRetailWhController : Controller
    {
        private readonly IGoodsRetailWhService _service;

        public GoodsRetailWhController(IGoodsRetailWhService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<ApiResponse<GoodsRetailWhCreateModelRes>> Create([FromBody] GoodsRetailWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhCreateModelRes>();
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
        public async Task<ApiResponse<GoodsRetailWhUpdateModelRes>> Update([FromBody] GoodsRetailWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhUpdateModelRes>();
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
        public async Task<ApiResponse<GoodsRetailWhDeleteModelRes>> Delete([FromBody] GoodsRetailWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<GoodsRetailWhDeleteModelRes>();
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
        public async Task<ApiResponse<GoodsRetailWhListModelRes>> List([FromBody] GoodsRetailWhListModelReq req)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            LoggerFunctionUtility.CommonLogStart(this, stopwatch);
            var retVal = new ApiResponse<GoodsRetailWhListModelRes>();
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