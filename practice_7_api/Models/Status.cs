using System.ComponentModel.DataAnnotations;

namespace practice_7_api.Models
{
    public class Status
    {
        [Key]
        public int status_id { get; set; }
        public string status_name { get; set; }
    }
}
