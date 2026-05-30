namespace FileUploader.Api.DTOs
{
    public static class Mappings
    {
        public static StoredFileDto ToDto(this Entities.StoredFile storedFile)
        {
            return new StoredFileDto
            {
                Id = storedFile.Id,
                OriginalFileName = storedFile.OriginalFileName,
                Url = storedFile.Url,
                ContentType = storedFile.ContentType,
                FileSize = storedFile.FileSize,
                UploadedAtUtc = storedFile.UploadedAtUtc
            };
        }   
    }
}
