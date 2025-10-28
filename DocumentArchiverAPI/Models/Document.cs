using System.ComponentModel.DataAnnotations;
namespace DocumentArchiverAPI.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }   // title in db
        public string FileName { get; set; }   // real file name
        public string FilePath { get; set; }   // file path in db
        public string Extention { get; set; }  //.jpg , .pdf ...

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserCreate { get; set; }   // username who uploaded the file 
        public DateTime dateCreate { get; set; }

        public string UserUpdate { get; set; }
        public DateTime dateUpdate { get; set; }
    }
}
