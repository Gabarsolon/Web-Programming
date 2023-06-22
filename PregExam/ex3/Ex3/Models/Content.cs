namespace Ex3.Models
{
    public class Content
    {
        public int ID { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public string description {get; set; }

        public string url { get; set; }
        public int ?userID { get; set; }
    }
}
