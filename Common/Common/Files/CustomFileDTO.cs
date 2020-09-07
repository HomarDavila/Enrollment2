using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Files
{
    public class CustomFileDTO
    {
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string FileContentType { get; set; }
        public string FileExtension { get; set; }
        public string FileURI { get; set; }
        public long? FileSize { get; set; }
        public string FileDataBase64 { get; set; }

    }
}
