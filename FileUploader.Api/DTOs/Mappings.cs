using FileUploader.Api.Entities;

namespace FileUploader.Api.DTOs
{
    public static class Mappings
    {
        public static StoredFileDto ToDto(this StoredFile storedFile)
        {
            return new StoredFileDto
            {
                Id = storedFile.Id,
                OriginalFileName = storedFile.OriginalFileName,
                BlobName = storedFile.BlobName,
                ContentType = storedFile.ContentType,
                FileSize = storedFile.FileSize,
                UploadedAtUtc = storedFile.UploadedAtUtc
            };
        }   
    }
}
