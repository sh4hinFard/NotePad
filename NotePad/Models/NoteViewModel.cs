using Microsoft.AspNetCore.Http;

namespace NotePad.Models
{
    public class NoteViewModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public IFormFile Image { get; set; }
        public string Detail { get; set; }
    }
}
