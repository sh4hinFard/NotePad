using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Be
{
  public class User
    {
        [Key]  
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public ICollection<Notebook> notebooks { get; set; }
        public ICollection<Task_Todo> task_Todos { get; set; }
        public ICollection<Logs> logs { get; set; }
    }
}
