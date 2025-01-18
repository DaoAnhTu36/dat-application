using DAT.API.Models;
using DAT.Common.Logger;
using DAT.Common.Medias;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Responses;
using DAT.Common.Utilities;
using DAT.Database;
using DAT.Database.Entities.WarehouseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace DAT.API.Services.Implementations
{
    public class MediaService : IMediaService
    {
        private readonly IOptions<AppConfig> _options;
        private readonly EntityDBContext _dbContext;

        public MediaService(IOptions<AppConfig> options
            , EntityDBContext dbContext)
        {
            _options = options;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<UploadFileResponseDTO>> FileUpload(List<IFormFile> files)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<UploadFileResponseDTO>
            {
                Data = new UploadFileResponseDTO
                {
                    FileIds = new List<FileUploadDTO>()
                }
            };
            try
            {
                if (_options.Value.FTPServerSetting == null)
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        StatusCode = "400"
                    };
                    return retVal;
                }
                string ftpServerUrl = _options.Value.FTPServerSetting.Server;
                string ftpUsername = _options.Value.FTPServerSetting.User;
                string ftpPassword = _options.Value.FTPServerSetting.Password;
                var size = files.Sum(f => f.Length);
                var filePath = Directory.GetCurrentDirectory() + _options.Value.FolderSetting?.PathFolderMedia?.Trim();
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string pathSave = string.Empty;
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var fileName = UtilityConvert.RenameFileUpload(formFile.FileName);
                        pathSave = filePath + fileName;
                        var id = Guid.NewGuid();
                        using (var stream = new FileStream(pathSave, FileMode.OpenOrCreate))
                        {
                            await formFile.CopyToAsync(stream);
                            _dbContext.Add(new MediaManagerEntity
                            {
                                Id = id,
                                Path = pathSave,
                                Name = fileName,
                            });
                        }

                        await _dbContext.SaveChangesAsync();
                        retVal.Data.FileIds.Add(new FileUploadDTO
                        {
                            FileId = id,
                            FileName = fileName
                        });
                    }

                    if (_options.Value.FTPServerSetting == null)
                    {
                        retVal.IsNormal = false;
                        retVal.MetaData = new MetaData
                        {
                            StatusCode = "400"
                        };
                        LoggerFunctionUtility.CommonLogEnd(this, retVal);
                        return retVal;
                    }
                    foreach (IFormFile file in files)
                    {
                        var process = await FTPServerCommon.UploadFile(ftpServerUrl, pathSave, ftpUsername, ftpPassword);
                        if (!process.Item2)
                        {
                            retVal.IsNormal = false;
                            retVal.MetaData = new MetaData
                            {
                                StatusCode = "400"
                            };
                            LoggerFunctionUtility.CommonLogEnd(this, retVal);
                            return retVal;
                        }
                    }
                }
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

        public async Task<ApiResponse<ItemFileManagerResponseDTO>> ListFileManager(ItemFileManagerRequestDTO request)
        {
            LoggerFunctionUtility.CommonLogStart(this);
            var retVal = new ApiResponse<ItemFileManagerResponseDTO>();
            try
            {
                var query = await _dbContext.MediaManagerEntities.Select(x => new MediaManagerDTO
                {
                    FileName = x.Name,
                    FilePath = x.Path
                }).ToListAsync();
                retVal = new ApiResponse<ItemFileManagerResponseDTO>
                {
                    Data = new ItemFileManagerResponseDTO
                    {
                        List = UtilityDatabase.PaginationExtension(_options, query, request.PageNumber, request.PageSize, out int totalPage, out int currentPage)
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