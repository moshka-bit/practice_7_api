namespace practice_7_api.Requests
{
    public class UpdateBook
    {
        public string book_name { get; set; }
        public DateOnly public_date { get; set; }
        public string description { get; set; }
        public int count { get; set; }
        public int author_id { get; set; }
        public int genre_id { get; set; }
    }
}
