using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class DeneyimController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();

        public ActionResult Deneyim()
        {
            int ogrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            if (ogrenciID == 0)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            var deneyimler = db.Deneyimler.Where(d => d.Ogrenciler.OgrenciID == ogrenciID).ToList();

            return View(deneyimler);
        }

        public ActionResult DeneyimEkle()
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            return View();
        }


        [HttpPost]
        public ActionResult DeneyimEkle(Deneyimler model)
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (nextOgrenci != null)
            {
                ViewBag.OgrenciID = nextOgrenciID;
            }

            if (ModelState.IsValid)
            {
                var yeniDeneyim = new Deneyimler
                {
                    DeneyimAdi = model.DeneyimAdi,
                    DeneyimDetayi = model.DeneyimDetayi,
                    Deneyimİcerigi = model.Deneyimİcerigi,
                    Ogrenciler = nextOgrenci,
                };

                db.Deneyimler.Add(yeniDeneyim);
                db.SaveChanges();

                return RedirectToAction("Deneyim");
            }

            return View(model);
        }

        public ActionResult DeneyimSil(int DeneyimID)
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }
            var deneyim = db.Deneyimler.Find(DeneyimID);
            db.Deneyimler.Remove(deneyim);
            db.SaveChanges();
            return RedirectToAction("Deneyim");

        }

        public ActionResult DeneyimDuzenle(int? deneyimID)
        {
            Deneyimler deneyim = null;

            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            if (deneyimID != null)
            {
                deneyim = db.Deneyimler.Where(x => x.DeneyimID == deneyimID).FirstOrDefault();
            }

            return View(deneyim);
        }

        [HttpPost]
        public ActionResult DeneyimDuzenle(Deneyimler model)
        {
            Deneyimler deneyim = db.Deneyimler.Where(x => x.DeneyimID == model.DeneyimID).FirstOrDefault();

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (deneyim != null)
            {

                deneyim.DeneyimAdi = model.DeneyimAdi;
                deneyim.DeneyimDetayi = model.DeneyimDetayi;
                deneyim.Deneyimİcerigi = model.Deneyimİcerigi;
                deneyim.Ogrenciler = nextOgrenci;
                deneyim.DeneyimID = model.DeneyimID;

                db.SaveChanges();
            }
            return RedirectToAction("Deneyim");
        }
    }
}