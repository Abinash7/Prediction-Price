using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using PricePrediction.Models;

namespace PricePrediction.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        EcommerceDBEntities _db = new EcommerceDBEntities();
        [AllowAnonymous]
        public ActionResult Index()
        {

            return View("Index");
        }

        public ActionResult Banner()
        {
            return PartialView("Banner", _db.tblBanners.ToList());

        }

        public ActionResult About()
        {
            return View();

        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            return View();
        }
        public ActionResult Product()
        {

            List<tblProduct> p = _db.tblProducts.ToList();
            return View(p);
        }

    }
}
