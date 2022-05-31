using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomePlayer_ASP.NET_Core_MVC.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string FileName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; }
        public int Rating { get; set; }


        [NotMapped]
        public IFormFile MovieFile { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
