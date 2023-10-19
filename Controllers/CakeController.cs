using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using cake_project.Models;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.Xml;
using Microsoft.Extensions.Hosting;

namespace cake_project.Controllers
{
    public class CakeController : Controller
    {
        private readonly ILogger<CakeController> _logger;
        private readonly Cart_dbContext _cart_dbContext;
        string _path;

        


        public CakeController( Cart_dbContext cart_dbContext, IWebHostEnvironment hostEnvironment)
        {
            
			_cart_dbContext = cart_dbContext;
            _path = $@"{hostEnvironment.WebRootPath}\img_size";
        }
        FileInfo[] GetFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_path);
            FileInfo[] files = directoryInfo.GetFiles();

            return files;
        }


        public IActionResult Index()
                {
                    return View();
                }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult commodity()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult serve()
        {
            return View();
        }

        [Authorize(Roles ="1,99")]
        public IActionResult product(int Cid = 1)
        {
            ViewBag.CategoryName = _cart_dbContext.PcategorySet.FirstOrDefault(m => m.PCid == Cid).PCName;

            var product = _cart_dbContext.Products
                .Where(m => m.PCid == Cid)
                .OrderByDescending(m => m.PCid)
                .ToList();

            return View(product);
            
        }


        [Authorize(Roles = "99")]
        public IActionResult ProductUpload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductUpload(Products products, IFormFile formFile)
        {
            TempData["Error"] = "資料上傳失敗";
            if (ModelState.IsValid)
            {
                if(formFile != null)
                {
                    if(formFile.Length > 0)
                    {
                        string fileName=$"{Guid.NewGuid().ToString()}.jpg";
                        string savePath = $"{_path}\\{fileName}";
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        products.Ptitle = fileName;
                        products.PTime = DateTime.Now;
                        _cart_dbContext.Products.Add(products);
                        _cart_dbContext.SaveChanges();
                        TempData["Success"] = "照片上傳成功";

                        return RedirectToAction("product", new { Pid = products.PCid });
                    }
                }
            }
            return View(products);
        }

        public IActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Login(string Acc, string Pass)
        {
              Models.Cart_dbContext db = _cart_dbContext;
            
              var  members_get = from m in db.Members
                              where m.Account==Acc && m.Password==Pass
                              select m;
              var  members_result = members_get.FirstOrDefault(m => m.Account == Acc && m.Password == Pass);

            if (members_result != null)
            {
                IList<Claim> claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name,members_result.name),
                    new Claim(ClaimTypes.Role,Convert.ToString(members_result.Roles))
                };

                var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties { IsPersistent = true };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdentity), authProperties);

                return RedirectToAction("product", "Cake");

            }


            TempData["Error"] = "帳號或密碼錯誤"; 


            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }



        public IActionResult Register()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.Members pstback,string Account)
        {
            Models.Cart_dbContext db = _cart_dbContext;

            var register_get = from m in db.Members
                               where m.Account == Account
                               select m;
            var members_result = register_get.FirstOrDefault(m => m.Account == Account );
            if (members_result == null)
            {
                db.Members.Add(pstback);
                db.SaveChanges();
            }
            else
            {
                TempData["Error"] = "帳號已註冊";
                return View();
            }
            

            


            return RedirectToAction("Login");
        }
        
        [Authorize(Roles = "99")]
        public IActionResult Cart()
        {
            
            /*
            List<Models.Products> result= new List<Models.Products>();
            
            using (Models.Cart_dbContext db = _cart_dbContext)
            {
                result = (from i in db.Products select i).ToList();
            }
            */
            



            return View();
            
        }








     
    }
    
}
   