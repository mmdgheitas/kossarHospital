namespace kosarHospital.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string doctorName { get; set; }
        public string? detail { get; set; }
        public DateTime date { get; set; }
        public User user { get; set; }
    }
}