namespace FileUploader.Api.DTOs
{
    public sealed record StoredFileDto
    {
        public string Id { get; init; } = null!;
        public string OriginalFileName { get; init; } = null!;
        public string Url { get; init; } = null!;
        public string? ContentType { get; init; } = null!;
        public long? FileSize { get; init; }
        public DateTime UploadedAtUtc { get; init; }
    }
}
