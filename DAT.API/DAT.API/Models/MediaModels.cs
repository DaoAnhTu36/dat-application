using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    public class MediaModels
    {
    }
    public class UploadFileResponseDTO
    {
        public List<FileUploadDTO>? FileIds { get; set; }
    }

    public class FileUploadDTO
    {
        public Guid? FileId { get; set; }
        public string? FileName { get; set; }
        public string? Path { get; set; }
    }

    public class ItemFileManagerRequestDTO : BasePageEntity
    {
    }

    public class ItemFileManagerResponseDTO
    {
        public IEnumerable<MediaManagerDTO> List { get; set; }
    }

    public class MediaManagerDTO
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
    }
}
