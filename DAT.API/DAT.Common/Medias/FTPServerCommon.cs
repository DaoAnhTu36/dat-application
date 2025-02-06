using DAT.Common.Models.Responses;
using FluentFTP;

namespace DAT.Common.Medias
{
    public static class FTPServerCommon
    {
        public static async Task<ApiResponse> UploadFile(string ftpServerUrl, string filePath, string ftpUsername, string ftpPassword)
        {
            var retVal = new ApiResponse();
            using var ftpClient = new FtpClient(ftpServerUrl, ftpUsername, ftpPassword);
            try
            {
                if (!ftpClient.IsConnected)
                {
                    ftpClient.Connect();
                }
                var remoteFilePath = $"{Path.GetFileName(filePath)}";
                var result = ftpClient.UploadFile(filePath, remoteFilePath);
                if (result == FtpStatus.Success)
                {
                    retVal.MetaData = new MetaData
                    {
                        Message = "File uploaded successfully.",
                        StatusCode = "200",
                    };
                }
                else
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        Message = "File upload failed.",
                        StatusCode = "500",
                    };
                }
            }
            catch (Exception ex)
            {
                retVal = new ApiResponse
                {
                    IsNormal = false,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        StatusCode = "500",
                    }
                };
            }
            finally
            {
                ftpClient.Disconnect();
            }
            return retVal;
        }

        public static async Task<ApiResponse<List<FtpListItem>>> GetFileListWithFluentFtp(string host, string username, string password, string remoteDirectory)
        {
            var retVal = new ApiResponse<List<FtpListItem>>();
            using (var ftpClient = new FtpClient(host, username, password))
            {
                try
                {
                    ftpClient.Connect();
                    var files = ftpClient.GetListing(remoteDirectory);
                    retVal.Data = [.. files];
                }
                catch (Exception ex)
                {
                    retVal.IsNormal = false;
                    retVal.MetaData = new MetaData
                    {
                        Message = ex.Message,
                        StatusCode = "500",
                    };
                }
                finally
                {
                    ftpClient.Disconnect();
                }
            }
            return retVal;
        }
    }
}