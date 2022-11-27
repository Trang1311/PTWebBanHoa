using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHoa.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanHoa.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        WebBanHoaEntities data = new WebBanHoaEntities();
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(data.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }



        public ActionResult Details(int id)
        {
            var kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           
            var vietkh = (data.KHACHHANGs.Where(vs => vs.MaKH == id)).SingleOrDefault();
            if (vietkh != null)
            {
                data.KHACHHANGs.Remove(vietkh);
                data.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }

}
