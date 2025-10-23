using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice_7_api.Models
{
    public class Rent_Book
    {
        [Key]
        public int rent_book_id { get; set; }
        public DateOnly start_date { get; set; }
        public DateOnly end_date { get; set; }


        [Required]
        [ForeignKey("Reader")]
        public int reader_id { get; set; }
        public Reader Reader { get; set; }

        [Required]
        [ForeignKey("Book")]

        public int book_id { get; set; }
        public Book Book { get; set; }

        [Required]
        [ForeignKey("Status")]
        public int status_id { get; set; }

        public Status Status { get; set; }
    }
}
