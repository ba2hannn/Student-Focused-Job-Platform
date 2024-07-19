using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class DestekController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();

        public ActionResult OgrenciDestek()
        {
            if (Session["NextOgrenciID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (nextOgrenci != null)
            {
                ViewBag.OgrenciAdi = nextOgrenci.OgrenciAdi;
            }

            return View(new Destek());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OgrenciDestek(Destek destek)
        {
            if (Session["NextOgrenciID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (nextOgrenci != null)
            {
                ViewBag.OgrenciAdi = nextOgrenci.OgrenciAdi;
            }

            if (nextOgrenci != null)
            {
                destek.Ogrenciler = nextOgrenci;
            }

            if (ModelState.IsValid)
            {
                db.Destek.Add(destek);
                db.SaveChanges();

                return RedirectToAction("AnasayfaOgrenci", "Giris");
            }

            return View(destek);
        }

        public ActionResult IsverenDestek()
        {
            if (Session["NextIsverenID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextIsverenID = Convert.ToInt32(Session["NextIsverenID"]);

            var nextIsveren = db.Isverenler.FirstOrDefault(x => x.IsverenID == nextIsverenID);

            if (nextIsveren != null)
            {
                ViewBag.IsverenAdi = nextIsveren.SirketAdi;
            }

            return View(new Destek());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsverenDestek(Destek destek)
        {
            if (Session["NextIsverenID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextIsverenID = Convert.ToInt32(Session["NextIsverenID"]);

            var nextIsveren = db.Isverenler.FirstOrDefault(x => x.IsverenID == nextIsverenID);

            if (nextIsveren != null)
            {
                ViewBag.IsverenAdi = nextIsveren.SirketAdi;
            }

            if (nextIsveren != null)
            {
                destek.Isverenler = nextIsveren;
            }

            if (ModelState.IsValid)
            {
                db.Destek.Add(destek);
                db.SaveChanges();
                return RedirectToAction("AnasayfaIsveren", "Giris");
            }

            return View(destek);
        }
    }
}