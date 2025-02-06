using DAT.API.Models;
using DAT.API.Services;
using DAT.Common.Logger;
using DAT.Common.Medias;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DAT.API.Controllers
{
    [Route("api/media")]
    public class MediaController : Controller
    {
        private readonly IMediaService _service;

        public MediaController(IMediaService service)
        {
            _service = service;
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<ApiResponse<UploadFileResponseDTO>> FileUpload([FromForm]List<IFormFile> files)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<UploadFileResponseDTO>();
            try
            {
                retVal = await _service.FileUpload(files);
            }
            catch (Exception ex)
            {
                retVal = new ApiResponse<UploadFileResponseDTO>
                {
                    IsNormal = false,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        StatusCode = "500",
                    }
                };
                LoggerFunctionUtility.CommonLogEnd(this, retVal);
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }

        [HttpPost("list")]
        public async Task<ApiResponse<ItemFileManagerResponseDTO>> ListFileManager(ItemFileManagerRequestDTO request)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<ItemFileManagerResponseDTO>();
            try
            {
                retVal = await _service.ListFileManager(request);
            }
            catch (Exception ex)
            {
                retVal = new ApiResponse<ItemFileManagerResponseDTO>
                {
                    IsNormal = false,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        StatusCode = "500",
                    }
                };
                LoggerFunctionUtility.CommonLogEnd(this, retVal);
            }
            LoggerFunctionUtility.CommonLogEnd(this, retVal);
            return retVal;
        }
    }
}