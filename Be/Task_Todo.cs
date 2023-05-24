using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Be
{
    public class Task_Todo
    {
        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public string? Detail { get; set; }
        public bool isdelet { get; set; }
        public User user { get; set; }
        public int UserId { get; set; }
    }
}
