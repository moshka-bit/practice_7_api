using System.ComponentModel.DataAnnotations;

namespace practice_7_api.Models
{
    public class Author
    {
        [Key]
        public int author_id { get; set; }
        public string author_last_name { get; set; }
        public string author_first_name { get; set; }
        public DateOnly author_date_birth { get; set; }
        public DateOnly author_date_death { get; set; }
    }
}
