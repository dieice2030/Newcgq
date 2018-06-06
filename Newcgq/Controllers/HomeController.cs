using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newcgq.Extends;
using Newcgq.Models;

namespace Newcgq.Controllers
{
    public class HomeController : Controller
    {
        WIFIcgqContext _context;

        public HomeController(WIFIcgqContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Login(string username,string password,string valicode,string usergroup)
        {
            try
            {
                if (valicode.ToLower() != HttpContext.Session.GetString("LoginValidateCode").ToLower())
                {
                    return Json(-2);//验证码错误
                }
                else
                {
                    if (usergroup == "1")//普通用户
                    {
                    var user = _context.UserInfo.Where(m => m.UserName.Equals(username));
                    var test = user.Count();
                    if (user.Count() == 0)
                        return Json(0);//用户不存在
                    else if (user.First().PassWord == password)
                        {
                            HttpContext.Session.SetInt32("UserId", user.Select(m => m.Id).Single());
                            HttpContext.Session.SetString("UserType", "user");
                            return Json(1);//验证通过
                        }

                    else
                        return Json(-1);//密码错误
                    }
                    else//管理员用户
                    {
                        var user = _context.AdminInfo.Where(m => m.UserName.Equals(username));
                        var test = user.Count();
                        if (user.Count() == 0)
                            return Json(0);//用户不存在
                        else if (user.First().PassWord == password)
                        {
                            HttpContext.Session.SetInt32("UserId", user.Select(m => m.Id).Single());
                            HttpContext.Session.SetString("UserType", "admin");
                            return Json(1);//验证通过
                        }
                        else
                            return Json(-1);//密码错误
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public JsonResult NavController()
        {
            try
            {
                var userType = HttpContext.Session.GetString("UserType");
                if (userType == "admin")
                    return Json("admin");
                if (userType == "user")
                    return Json("user");
                return Json(0);
            }
            catch
            {
                return Json(-1);
            }
        }

        public JsonResult Quit()
        {
            try
            {
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Remove("UserType");
                return Json(1);
            }
            catch
            {
                return Json(-1);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Validate(ValidateServices _ValidateServices)
        {
            string code = "";
            System.IO.MemoryStream ms = _ValidateServices.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
          
        }
    }
}
