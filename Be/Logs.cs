using System.ComponentModel.DataAnnotations;

namespace Be
{
    public class Logs
    {
        [Key]
        public int LogId { get; set; }
        public DateTime Logdate { get; set; }
        public User user { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
    }
}
