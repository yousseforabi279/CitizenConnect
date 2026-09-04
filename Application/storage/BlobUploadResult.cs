using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.storage
{
    public class BlobUploadResult
    {
        public string BlobName { get; set; }
        public string ContentType { get; set; }
        public long SizeBytes { get; set; }
    }
}
