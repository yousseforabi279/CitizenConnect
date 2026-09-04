using Application.storage;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Storage
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _connectionString;
        private readonly StorageSharedKeyCredential _credential;

        public BlobStorageService(IConfiguration config)
        {
            _connectionString = config["AzureStorage:ConnectionString"];

            var accountName = ExtractValue(_connectionString, "AccountName");
            var accountKey = ExtractValue(_connectionString, "AccountKey");
            _credential = new StorageSharedKeyCredential(accountName, accountKey);
        }

        private BlobContainerClient GetContainerClient(string containerName)
        {
            var containerClient = new BlobContainerClient(_connectionString, containerName);
            containerClient.CreateIfNotExists(PublicAccessType.None);
            return containerClient;
        }

        public async Task<BlobUploadResult> UploadFileAsync(FileUploadRequest file, string containerName)
        {
            var containerClient = GetContainerClient(containerName);
            var blobName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(file.Content, new BlobHttpHeaders
            {
                ContentType = file.ContentType
            });

            return new BlobUploadResult
            {
                BlobName = blobName,
                ContentType = file.ContentType,
                SizeBytes = file.Length
            };
        }

        public async Task DeleteFileAsync(string blobName, string containerName)
        {
            var containerClient = GetContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public string GetReadSasUrl(string blobName, string containerName, int expiryMinutes = 60)
        {
            var containerClient = GetContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerClient.Name,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(expiryMinutes)
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var sasToken = sasBuilder.ToSasQueryParameters(_credential).ToString();
            return $"{blobClient.Uri}?{sasToken}";
        }

        private static string ExtractValue(string connStr, string key)
            => connStr.Split(';').First(p => p.StartsWith($"{key}=")).Substring(key.Length + 1);

    }
}