using System.ComponentModel.DataAnnotations;

namespace practice_7_api.Models
{
    public class Genre
    {
        [Key]
        public int genre_id { get; set; }

        public string genre_name { get; set; }
    }
}
