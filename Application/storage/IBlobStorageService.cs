using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.storage
{
    public interface IBlobStorageService
    {
        Task<BlobUploadResult> UploadFileAsync(FileUploadRequest file, string containerName);
        Task DeleteFileAsync(string blobName, string containerName);
        string GetReadSasUrl(string blobName, string containerName, int expiryMinutes = 60);
    }
}
