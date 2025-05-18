using System.ComponentModel.DataAnnotations;
namespace DocumentArchiverAPI.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadedAt { get; set; }
        public string UploadedBy { get; set; }
    }
}
