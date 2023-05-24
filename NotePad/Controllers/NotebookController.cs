using Be;
using Dal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotePad.Models;
using NotePad.Utilities;

namespace NotePad.Controllers
{
    [Authorize]
    public class NotebookController : Controller
    {
        private readonly Database database;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        public NotebookController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, Database _database)
        {
            database = _database;   
            Environment = hostingEnvironment;   
        }
        public IActionResult Index()
        {
            var user = database.users.Where(o => o.Username == User.Identity.Name).SingleOrDefault();
            var Notes = database.notebooks.Where(o => o.UserId == user.UserId &&o.isdelet ==false).ToList();
            return View(Notes);
        }
        [HttpPost]
        public JsonResult Add_Note(NoteViewModel model)
        {
            var user = database.users.Where(o => o.Username == User.Identity.Name).SingleOrDefault();
            var upload =new Upload_Image(Environment);
            var note = new Notebook() {
                Title = model.Title,
                Date = model.Date,
                Detail = model.Detail,
                Image = upload.upload(model.Image),
                UserId = user.UserId
            };
            database.notebooks.Add(note);   
            database.SaveChanges();
            return Json(note.NoteId);
        }
        public JsonResult Delete_Note(int id) {
            var Note = database.notebooks.Where(o => o.NoteId == id).SingleOrDefault();
            Note.isdelet = true;
            database.notebooks.Update(Note);
            database.SaveChanges();
            return Json("Success");
        }
        [HttpGet]
        public IActionResult Edit_Note(int Id)
        {
            return View(database.notebooks.Where(o=>o.NoteId == Id).SingleOrDefault());
        }
        [HttpPost]
        public IActionResult Edit_Note(int Id,EditNoteViewModel model)
        {
            var upload = new Upload_Image(Environment);
            var note = database.notebooks.Where(o => o.NoteId == Id).SingleOrDefault();
            note.Title = model.Title;
            note.Date = model.Date;
            note.Detail = model.Detail;
            note.Image = upload.upload(model.Image);
            database.notebooks.Update(note);
            database.SaveChanges(); 
            return RedirectToAction("Index","Notebook");
        }
    }
}
