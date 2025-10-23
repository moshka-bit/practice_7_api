namespace practice_7_api.Requests
{
    public class CreateNewReader
    {
        public string reader_last_name { get; set; }
        public string reader_first_name { get; set; }
        public DateOnly reader_date_birth { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
