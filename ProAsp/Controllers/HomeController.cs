using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProAsp.Core.Services;

namespace ProAsp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILoggerService _loggerService;

        public HomeController(IUserService userService, ILoggerService loggerService)
        {
            _userService = userService;
            _loggerService = loggerService;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            ViewBag.Message = "About me.";
            _loggerService.LogInfo("aaa");
            return View();
        }

        public ViewResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ViewResult Users()
        {
            return View(_userService.GetAllUsers());
        }
    }
}