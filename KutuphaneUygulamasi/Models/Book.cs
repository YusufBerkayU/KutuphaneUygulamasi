using System.ComponentModel.DataAnnotations;

namespace KutuphaneUygulamasi.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

      
        public string? PdfFilePath { get; set; }
    }
}
