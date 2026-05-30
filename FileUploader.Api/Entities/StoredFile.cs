namespace FileUploader.Api.Entities
{
    public class StoredFile
    {
        public string Id { get; set; }
        public string OriginalFileName { get; set; }
        public string? ContentType { get; set; }
        public long? FileSize { get; set; }
        public string Url { get; set; }
        public DateTime UploadedAtUtc { get; set; }

        public static string NewId() => $"f_{Guid.CreateVersion7()}";
    }
}
