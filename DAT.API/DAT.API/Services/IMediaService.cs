using DAT.API.Models;
using DAT.Common.Models.Responses;

namespace DAT.API.Services
{
    public interface IMediaService
    {
        Task<ApiResponse<UploadFileResponseDTO>> FileUpload(List<IFormFile> files);

        Task<ApiResponse<ItemFileManagerResponseDTO>> ListFileManager(ItemFileManagerRequestDTO request);
    }
}
