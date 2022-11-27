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
    public class DonHangController : Controller
    {
        WebBanHoaEntities data = new WebBanHoaEntities();
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(data.DONDATHANGs.ToList().OrderBy(n => n.MaHD).ToPagedList(iPageNum, iPageSize));
        }
      


        public ActionResult Details(int id)
        {
            var hd = data.DONDATHANGs.SingleOrDefault(n => n.MaHD == id);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hd);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var hd = data.DONDATHANGs.SingleOrDefault(n => n.MaHD == id);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hd);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var hd = data.DONDATHANGs.SingleOrDefault(n => n.MaHD == id);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var viethd = (data.DONDATHANGs.Where(vs => vs.MaHD == id)).SingleOrDefault();
            if (viethd != null)
            {
                data.DONDATHANGs.Remove(viethd);
                data.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }

}
