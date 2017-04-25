using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCCL.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        /// <summary>
        /// Shows a menu of items for the admin
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}