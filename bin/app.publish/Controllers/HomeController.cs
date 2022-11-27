using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHoa.Models;
using PagedList.Mvc;
using System.Data.Entity;
using PagedList;

namespace WebBanHoa.Controllers
{
    public class HomeController : Controller
    {
        WebBanHoaEntities db = new WebBanHoaEntities();
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }    
        public ActionResult Menu()
        {

            return View();
        }
        public ActionResult SlideShow()
        {

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(FormCollection f)
        //{
        //    var sTenDN = f["UserName"];
        //    var sMK = f["PassWord"];
        //    ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDNAdmin == sTenDN && n.MatKhauAdmin == sMK);
        //    if (ad != null)
        //    {
        //        Session["Admin"] = ad;
        //        return RedirectToAction("Index", "Home");

        //    }
        //    else
        //    {
        //        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
        //    }
        //    return View();
        //}

        public ActionResult Index(string searchString, int? page)
        {

            var sp = from l in db.SANPHAMs
                        select l;
            int iPageNum = (page ?? 1);
            int iPageSize = 9;
            //return View(data.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
            if (searchString!=null)
            {
                sp = sp.Where(s => s.TenSP.Contains(searchString));
                if (sp.Count() == 0)
                {
                    
                    return RedirectToAction("NotFound", "Home");
                }
                else
                    return View(sp);
                
            }
            else
                return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
        }
    }
}