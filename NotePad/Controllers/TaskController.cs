using Be;
using Dal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotePad.Models;
using NotePad.Utilities;

namespace NotePad.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly Database database;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        public TaskController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, Database _database)
        {
            database = _database;
            Environment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var user = database.users.Where(o => o.Username == User.Identity.Name).SingleOrDefault();
            var tasks = database.task_Todos.Where(o => o.UserId == user.UserId && o.isdelet == false).ToList();
            return View(tasks);
        }
        [HttpPost]
        public JsonResult Add_Task(NoteViewModel model)
        {
            var user = database.users.Where(o => o.Username == User.Identity.Name).SingleOrDefault();
            var upload = new Upload_Image(Environment);
            var task = new Be.Task_Todo()
            {
                Title = model.Title,
                Date = model.Date,
                Detail = model.Detail,
                Image = upload.upload(model.Image),
                UserId = user.UserId
            };
            database.task_Todos.Add(task);
            database.SaveChanges();
            return Json(task.TaskId);
        }
        public JsonResult Delete_Task(int id)
        {
            var task = database.task_Todos.Where(o => o.TaskId == id).SingleOrDefault();
            task.isdelet = true;
            database.task_Todos.Update(task);
            database.SaveChanges();
            return Json("Success");
        }
        [HttpGet]
        public IActionResult Edit_Task(int Id)
        {
            return View(database.task_Todos.Where(o => o.TaskId == Id).SingleOrDefault());
        }
        [HttpPost]
        public IActionResult Edit_Task(int Id, EditNoteViewModel model)
        {
            var upload = new Upload_Image(Environment);
            var task = database.task_Todos.Where(o => o.TaskId == Id).SingleOrDefault();
            task.Title = model.Title;
            task.Date = model.Date;
            task.Detail = model.Detail;
            task.Image = upload.upload(model.Image);
            database.task_Todos.Update(task);
            database.SaveChanges();
            return RedirectToAction("Index", "Task");
        }
    }
}
