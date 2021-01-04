using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildingAndFreezerApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admins , Building", Users = "hossam50@gmail.com, ayman5383@gmail.com, MangerBuilding@gamil.com")]

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admins , Freezer", Users = "hossam50@gmail.com, ayman5383@gmail.com, MangerFreezer@gamil.com")]

        public ActionResult IndexFreezer()
        {
            return View();
        }

    }
}