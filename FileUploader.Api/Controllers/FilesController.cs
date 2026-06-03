using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileUploader.Api.Database;
using FileUploader.Api.DTOs;
using FileUploader.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.Api.Controllers
{
    [ApiController]
    [Route("files")]
    public class FilesController(
        BlobContainerClient blobContainerClient,
        AppDbContext appDbContext) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile is null || uploadedFile.Length == 0)
            {
                return BadRequest("File missing.");
            }

            var alreadyExists = await appDbContext.StoredFiles
                .AnyAsync(sf => sf.OriginalFileName == uploadedFile.FileName);

            if (alreadyExists)
            {
                return NoContent();
            }

            var fileToStore = new StoredFile
            {
                Id = StoredFile.NewId(),
                BlobName = StoredFile.NewBlobName(),
                OriginalFileName = uploadedFile.FileName,
                ContentType = uploadedFile.ContentType,
                FileSize = uploadedFile.Length,
                UploadedAtUtc = DateTime.UtcNow
            };

            var blobClient = blobContainerClient.GetBlobClient(fileToStore.BlobName);

            await using var stream = uploadedFile.OpenReadStream();

            await blobClient.UploadAsync(
                stream,
                new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = fileToStore.ContentType
                    }
                });

            appDbContext.StoredFiles.Add(fileToStore);
            await appDbContext.SaveChangesAsync();

            var fileDto = fileToStore.ToDto();

            return CreatedAtAction(nameof(GetStoredFile), new { id = fileDto.Id }, fileDto);
        }

        [HttpGet("{id}/download")]
        public async Task<ActionResult<StoredFileDto>> GetStoredFile(string id)
        {
            var file = await appDbContext.StoredFiles.FindAsync(id);
            if (file is null)
            {
                return NotFound();
            }

            var blobClient = blobContainerClient.GetBlobClient(file.BlobName);

            var stream = await blobClient.OpenReadAsync();

            return File(
                stream, 
                file.ContentType ?? "application/octet-stream",
                file.OriginalFileName);    
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStoredFiles()
        {
            var storedFiles = await appDbContext.StoredFiles
                .OrderByDescending(sf => sf.UploadedAtUtc)
                .ToListAsync();
            var filesDto = storedFiles.Select(sf => sf.ToDto()).ToList();
            return Ok(filesDto);
        }
    }
}
