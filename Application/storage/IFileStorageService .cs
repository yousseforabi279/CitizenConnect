using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.storage
{
    public interface IFileStorageService
    {
        Task<BlobUploadResult> UploadFileAsync(
       FileUploadRequest file,
       string folderName);

        Task DeleteFileAsync(
            string fileKey,
            string folderName);

        string GetFileUrl(
            string fileKey,
            string folderName);
    }
}
