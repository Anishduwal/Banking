using Banking.Models;
using Banking.Repo.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Campaign.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo _accountRepo;

        private readonly IWebHostEnvironment WebHostEnvironment;
        public AccountController(IAccountRepo accountRepo, IWebHostEnvironment webHostEnvironment)
        {
            _accountRepo = accountRepo;
            WebHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string Username, string Password)
        {
            try
            {
                if (Username != null && Password != null)
                {
                    using (var sha256 = SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                        Password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    }
                    var data = _accountRepo.Login(Username, Password);
                    if (data.Code == 0)
                    {
                        HttpContext.Session.SetString("username", Username);
                        HttpContext.Session.SetString("Id", Convert.ToString(data.Id));
                        HttpContext.Session.SetString("Isauthenticated", "true");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Isauthenticated", "false");
                        ViewBag.Error = data.Message;
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        public IActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount([FromForm] UserDetailsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ConfirmPassword == model.Password)
                    {
                        using (var sha256 = SHA256.Create())
                        {
                            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
                            model.Password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Password doesnot match";
                        return View();
                    }
                    var data = _accountRepo.CreateAdmin(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");

        }
        

    }
}
