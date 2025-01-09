using DAT.API.Models;
using DAT.Common.Logger;
using DAT.Common.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using DAT.API.Services.Warehouse;

namespace DAT.API.Controllers.Warehouses
{
    [Route("api/wh/stock")]
    public class StockWhController : Controller
    {
        private readonly IStockWhService _service;

        public StockWhController(IStockWhService service)
        {
            _service = service;
        }

        [HttpPost("stock-create")]
        public async Task<ApiResponse<StockWhCreateModelRes>> Create([FromBody] StockWhCreateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<StockWhCreateModelRes>();
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

        [HttpPost("stock-update")]
        public async Task<ApiResponse<StockWhUpdateModelRes>> Update([FromBody] StockWhUpdateModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<StockWhUpdateModelRes>();
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

        [HttpPost("stock-delete")]
        public async Task<ApiResponse<StockWhDeleteModelRes>> Delete([FromBody] StockWhDeleteModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<StockWhDeleteModelRes>();
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

        [HttpPost("stock-list")]
        public async Task<ApiResponse<StockWhListModelRes>> List([FromBody] StockWhListModelReq req)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            LoggerFunctionUtility.CommonLogStart(this, stopwatch);
            var retVal = new ApiResponse<StockWhListModelRes>();
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

        [HttpPost("stock-search")]
        public async Task<ApiResponse<StockWhSearchListModelRes>> Search([FromBody] StockWhSearchListModelReq req)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            LoggerFunctionUtility.CommonLogStart(this, stopwatch);
            var retVal = new ApiResponse<StockWhSearchListModelRes>();
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
            retVal = await _service.Search(req);
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        [HttpPost("stock-detail")]
        public async Task<ApiResponse<StockWhDetailModelRes>> Detail([FromBody] StockWhDetailModelReq req)
        {
            LoggerFunctionUtility.CommonLogStart(this, req);
            var retVal = new ApiResponse<StockWhDetailModelRes>();
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