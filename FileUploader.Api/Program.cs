using Azure.Identity;
using Azure.Storage.Blobs;
using FileUploader.Api.Database;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BlobContainerClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var accountUrl = config["BlobStorage:AccountUrl"];
    var containerName = config["BlobStorage:ContainerName"];

    return new BlobContainerClient(
        new Uri($"{accountUrl}/{containerName}"),
        new DefaultAzureCredential());
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500_000_000; // 500 MB
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        policy.WithExposedHeaders("Content-Disposition");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using IServiceScope scope = app.Services.CreateScope();
    await using AppDbContext dbContext =
        scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
