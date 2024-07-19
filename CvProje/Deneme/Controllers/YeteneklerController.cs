using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class YeteneklerController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();

        public ActionResult Yetenek()
        {
            int ogrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            if (ogrenciID == 0)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            var yetenekler = db.Yetenekler.Where(y => y.Ogrenciler.OgrenciID == ogrenciID).ToList();

            return View(yetenekler);
        }

        public ActionResult YetenekEkle()
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            return View();
        }


        [HttpPost]
        public ActionResult YetenekEkle(Yetenekler model)
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
                var yeniYetenek = new Yetenekler
                {
                    YetenekAdi = model.YetenekAdi,
                    Yetenekİcerigi = model.Yetenekİcerigi,
                    Ogrenciler = nextOgrenci,
                    YetenekID = model.YetenekID
                };

                db.Yetenekler.Add(yeniYetenek);
                db.SaveChanges();

                return RedirectToAction("Yetenek");
            }

            return View(model);
        }

        public ActionResult YetenekSil(int YetenekID)
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }
            var deneyim = db.Yetenekler.Find(YetenekID);
            db.Yetenekler.Remove(deneyim);
            db.SaveChanges();
            return RedirectToAction("Yetenek");

        }

        public ActionResult YetenekDuzenle(int? yetenekID)
        {
            Yetenekler yetenek = null;

            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            if (yetenekID != null)
            {
                yetenek = db.Yetenekler.Where(x => x.YetenekID == yetenekID).FirstOrDefault();
            }

            return View(yetenek);
        }

        [HttpPost]
        public ActionResult YetenekDuzenle(Yetenekler model)
        {
            Yetenekler yetenek = db.Yetenekler.Where(x => x.YetenekID == model.YetenekID).FirstOrDefault();

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (yetenek != null)
            {

                yetenek.YetenekAdi = model.YetenekAdi;
                yetenek.Yetenekİcerigi = model.Yetenekİcerigi;
                yetenek.Ogrenciler = nextOgrenci;
                yetenek.YetenekID = model.YetenekID;

                db.SaveChanges();
            }
            return RedirectToAction("Yetenek");
        }
    }
}