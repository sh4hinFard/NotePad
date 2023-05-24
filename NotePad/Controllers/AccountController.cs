using Be;
using Dal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NotePad.Models;
using NotePad.Repositories;
using NotePad.Utilities;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace NotePad.Controllers
{
    public class AccountController : Controller
    {
        private readonly Database database;
        private readonly IMessageSender messageSender;
        private readonly Get_Ip get_Ip;
        private readonly IHttpContextAccessor _accessor;
        public AccountController(Database Database,IMessageSender sender,Get_Ip _Ip, IHttpContextAccessor accessor)
        {
            database = Database;
            messageSender = sender;
            get_Ip = _Ip;   
            _accessor = accessor;   

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Register(RegisterViewModel model)
        {
            var pass = Hasher.EncodePasswordMd5(model.Password);
            var check = database.users.Any(i=>i.Username == model.Username||i.Email == model.Email);
            if (!check) {
                var user = new User() { Email = model.Email, Username = model.Username, Password = pass,Firstname =model.Firstname,Lastname=model.Lastname,IsActive=false };
                await database.users.AddAsync(user);
              await  database.SaveChangesAsync();
                var confirmation = new EmailViewModel { Id =user.UserId};
                messageSender.SendMessageSmtp(model.Email,model.Username,user.UserId);
                return Json("success");
            }
            else
            {
                return Json("Failed");
            }
          
        }
        public IActionResult Active_User(int UserId)
        {
            var user = database.users.Where(o => o.UserId == UserId).SingleOrDefault();
            if (user != null && user.IsActive == false)
            {
                user.IsActive = true;
                database.users.Update(user);
                database.SaveChanges();
                return View();
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Username,string Password)
        {
            var pass = Hasher.EncodePasswordMd5(Password);
            var user = database.users.Where(i => i.Username == Username ||i.Email ==Username).SingleOrDefault();
            if (user!=null)
            {
                
                if (user.Password == pass)
                {
                    if(user.IsActive)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                            new Claim(ClaimTypes.Name,user.Username)
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties { IsPersistent = true };
                        HttpContext.SignInAsync(principal, properties);
                        ViewBag.Success = true;
                        var log = new Be.Logs();
                        log.Logdate = DateTime.Now; 
                        log.UserId = user.UserId;
                        var middleware = new Get_Ip(_accessor);
                        var context = new DefaultHttpContext();
                        log.Ip = await middleware.Invoke(context);
                        database.logs.Add(log); 
                        database.SaveChanges();
                        return RedirectToAction("Index", "Notebook");
                    }
                    else
                    {
                        ModelState.AddModelError("IsActive", "please Active Your Account");
                        return View("\\Views\\Account\\Index.cshtml");
                    }

                }
                else
                {
                    ModelState.AddModelError(Password, "Wrong Password");
                    return View("\\Views\\Account\\Index.cshtml");
                }

            }
            ModelState.AddModelError(Username, "User Not Found");
            return View("\\Views\\Account\\Index.cshtml");


        
    }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> User_Profile()
        {
            var user = await database.users.Where(i => i.Username == User.Identity.Name).SingleOrDefaultAsync();
            return View(user);  
        }
        public async Task<IActionResult> Setting()
        {
            var user =  database.users.Where(i => i.Username == User.Identity.Name).SingleOrDefault();
            var middleware = new Get_Ip(_accessor);
            var context = new DefaultHttpContext();
            ViewBag.Ip =  await middleware.Invoke(context);
            var logs =  database.logs.Where(o => o.UserId == user.UserId).ToList();
            return View(logs);
        }
    }
}
