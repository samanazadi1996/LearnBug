using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    public class leafletController : Controller
    {
        // GET: leaflet
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}