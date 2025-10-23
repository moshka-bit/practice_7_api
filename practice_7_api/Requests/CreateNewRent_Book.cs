namespace practice_7_api.Requests
{
    public class CreateNewRent_Book
    {
        public int reader_id { get; set; }
        public int book_id { get; set; }
        public DateOnly start_date { get; set; }
        public DateOnly end_date { get; set; }
    }
}
