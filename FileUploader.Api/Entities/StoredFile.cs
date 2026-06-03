namespace FileUploader.Api.Entities
{
    public class StoredFile
    {
        public string Id { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string BlobName { get; set; } = string.Empty;
        public string? ContentType { get; set; }
        public long? FileSize { get; set; }
        public DateTime UploadedAtUtc { get; set; }

        public static string NewId() => $"f_{Guid.CreateVersion7()}";
        public static string NewBlobName() => $"blob_{Guid.CreateVersion7()}";
    }
}
