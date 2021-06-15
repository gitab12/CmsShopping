using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShopping.Areas.Area.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: Area/DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}