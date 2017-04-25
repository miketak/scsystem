using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCCL.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult AboutUs()
        {
            return View("AboutUs");
        }

        public ViewResult ContactUs()
        {
            return View("ContactUs");
        }
    }
}