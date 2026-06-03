using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploader.Api.Migrations
{
    /// <inheritdoc />
    public partial class Add_BlobName_Remove_Url : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "StoredFiles",
                newName: "BlobName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlobName",
                table: "StoredFiles",
                newName: "Url");
        }
    }
}
