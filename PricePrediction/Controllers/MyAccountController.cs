using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PricePrediction.Models;

namespace PricePrediction.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        EcommerceDBEntities _db = new EcommerceDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel l, string ReturnUrl = "")
        {
            using (EcommerceDBEntities _db = new EcommerceDBEntities())
            {
                var users = _db.tblUsers.Where(u => u.Username == l.Username && u.Password == l.Password).FirstOrDefault();
                if (users != null)
                {
                    FormsAuthentication.SetAuthCookie(l.Username, l.Rememberme);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "InvalidUser!!!");
                }

            }
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Signup()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Signup(tblUser tb)
        {
            _db.tblUsers.Add(tb);
            _db.SaveChanges();
            ViewBag.Message = "Successfully Created Account";
            return View();
        }
        [Authorize(Roles="Admin")]
        public ActionResult Select()
        {
            List<tblProduct> lst = _db.tblProducts.ToList();            
            return View(lst);
        }
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblProduct tb)
        {
            HttpPostedFileBase photo = Request.Files["photo"];
            var fileName = Path.GetFileName(photo.FileName);
            tb.photo = fileName;
            _db.tblProducts.Add(tb);
            if (_db.SaveChanges() > 0)
            {
                photo.SaveAs(Server.MapPath("~/ProductPhoto/" + fileName));
            }
            return RedirectToAction("AdminIndex", "Home");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            tblProduct tp = _db.tblProducts.Where(u => u.pID == id).FirstOrDefault();
            return View(tp);
        }
        [HttpPost]
        public ActionResult Edit(tblProduct p)
        {
            HttpPostedFileBase photo = Request.Files["photo"];
            var fileName = Path.GetFileName(photo.FileName);
            tblProduct tp = _db.tblProducts.Find(p.pID);
            tp.pName = p.pName;
            tp.pBrand = p.pBrand;
            tp.photo = fileName;
            fileName = p.photo;
            tp.description = p.description;         
            if (_db.SaveChanges() > 0)
            {
                photo.SaveAs(Server.MapPath("~/ProductPhoto/" + fileName));
            }
            return RedirectToAction("AdminIndex", "Home");

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            tblProduct tp = _db.tblProducts.Where(u => u.pID == id).FirstOrDefault();
            return View(tp);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            tblProduct tb = _db.tblProducts.Where(u => u.pID == id).FirstOrDefault();
            return View(tb);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete_post(int id)
        {
            tblProduct tb = _db.tblProducts.Where(u => u.pID == id).FirstOrDefault();
            _db.tblProducts.Remove(tb);
            _db.SaveChanges();
            return RedirectToAction("AdminIndex","Home");
        }
        //public ActionResult User_List()
        //{
        //    List<tblUser> lst= _db.tblUsers.ToList();
        //    return View(lst);
        //}
        //public ActionResult User_Details(int id)
        //{
        //    tblUser tu= _db.tblUsers.Where(u => u.uID == id).FirstOrDefault();
        //    return View(tu);
        //}
        //[HttpGet]
        //public ActionResult User_Delete(int id)
        //{
        //    tblUser tu = _db.tblUsers.Where(u => u.uID == id).FirstOrDefault();
        //    return View(tu);
        //}
        //[HttpPost, ActionName("Delete")]
        //public ActionResult User_Delete_Post(int id)
        //{
        //    tblUser tu = _db.tblUsers.Where(u => u.uID == id).FirstOrDefault();
        //    _db.tblUsers.Remove(tu);
        //    _db.SaveChanges();
        //    return RedirectToAction("User_List");
           
        //}
    }
}