using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeamWebApplicationAPI.Models
{
    public class FileDto
    {
        public byte[]? Data { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
}
