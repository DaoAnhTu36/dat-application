using System.Net;
using System.Net.Http.Headers;
using FluentFTP;
using static Google.Apis.Requests.BatchRequest;

namespace DAT.Common.Medias
{
    public static class FTPServerCommon
    {
        public static async Task<(string, bool)> UploadFile(string ftpServerUrl, string filePath, string ftpUsername, string ftpPassword)
        {
            try
            {
                using var ftpClient = new FtpClient(ftpServerUrl, ftpUsername, ftpPassword);
                 ftpClient.Connect();

                // Define the remote file path on the FTP server
                string remoteFilePath = $"/medias/{Path.GetFileName(filePath)}";

                // Upload the file
                ftpClient.UploadFile(filePath, remoteFilePath);
                ftpClient.Disconnect();
                return new("success", true);
            }
            catch (Exception ex)
            {
                return new(ex.Message, false);
            }
        }
    }
}