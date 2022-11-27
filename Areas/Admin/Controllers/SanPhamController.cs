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
    public class SanPhamController : Controller
    {
        WebBanHoaEntities data = new WebBanHoaEntities();
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 10;
            return View(data.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaPL = new SelectList(data.PHANLOAIs.ToList().OrderBy(n => n.TenPL), "MaPL", "TenPL");
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MaPL = new SelectList(data.PHANLOAIs.ToList().OrderBy(n => n.TenPL), "MaPL", "TenPL");
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");

            if (fFileUpload == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh bìa";
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuongBan = int.Parse(f["iSoLuongBan"]);
                ViewBag.DonGia = decimal.Parse(f["mDonGia"]);
                ViewBag.MaPL = new SelectList(data.PHANLOAIs.ToList().OrderBy(n => n.TenPL), "MaPL", "TenPL", int.Parse(f["MaPL"]));
                ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", int.Parse(f["MaNCC"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/sp"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.TenSP = f["sTenSP"];
                    sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "/n");
                    sp.HinhMinhHoa = sFileName;
                    sp.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sp.SoLuongBan = int.Parse(f["iSoLuongBan"]);
                    sp.DonGia = decimal.Parse(f["mDonGia"]);
                    sp.MaPL = int.Parse(f["MaPL"]);
                    sp.MaNCC = int.Parse(f["MaNCC"]);
                    data.SANPHAMs.Add(sp);
                    data.SaveChanges();
                }
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = data.CTDONDATHANGs.Where(n => n.MaSP == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Sản Phẩm này đã có trong bảng chi tiết đặt hàng";
                return View(sp);
            }
            var vietsp = (data.SANPHAMs.Where(vs => vs.MaSP == id)).SingleOrDefault();
            if (vietsp != null)
            {
                data.SANPHAMs.Remove(vietsp);
                data.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaPL = new SelectList(data.PHANLOAIs.ToList().OrderBy(n => n.TenPL), "MaPL", "TenPL", sp.MaSP);
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            return View(sp);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var iMaSP = int.Parse(f["iMaSP"]);
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == iMaSP); 
            ViewBag.MaPL = new SelectList(data.PHANLOAIs.ToList().OrderBy(n => n.TenPL), "MaPL", "TenPL", sp.MaPL);
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);

            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/sp"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.HinhMinhHoa = sFileName;
                }
                sp.TenSP = f["sTenSP"];
                sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                sp.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                sp.SoLuongBan = int.Parse(f["iSoLuongBan"]);
                sp.DonGia = decimal.Parse(f["mDonGia"]);
                sp.MaPL = int.Parse(f["MaPL"]);
                sp.MaNCC = int.Parse(f["MaNCC"]);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }

    }

}
