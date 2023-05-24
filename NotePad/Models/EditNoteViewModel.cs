namespace NotePad.Models
{
    public class EditNoteViewModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public IFormFile Image { get; set; }
        public string Detail { get; set; }
    }
}
