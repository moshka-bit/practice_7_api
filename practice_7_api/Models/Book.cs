using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice_7_api.Models
{
    public class Book
    {
        [Key]
        public int book_id { get; set; }
        public string book_name { get; set; }
        public DateOnly public_date { get; set; }
        public string description { get; set; }
        public int count { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int author_id { get; set; }
        public Author Author { get; set; }

        [Required]
        [ForeignKey("Genre")]
        public int genre_id { get; set; }
        public Genre Genre { get; set; }
    }
}
