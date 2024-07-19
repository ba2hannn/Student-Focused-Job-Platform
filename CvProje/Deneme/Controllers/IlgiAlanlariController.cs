using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class IlgiAlanlariController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();

        public ActionResult IlgiAlanlari()
        {
            int ogrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            if (ogrenciID == 0)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            var ilgiAlanlari = db.IlgiAlanlari.Where(ia => ia.Ogrenciler.OgrenciID == ogrenciID).ToList();

            return View(ilgiAlanlari);
        }

        public ActionResult IlgiAlanlariEkle()
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            return View();
        }


        [HttpPost]
        public ActionResult IlgiAlanlariEkle(IlgiAlanlari model)
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
                var yeniIlgiAlani = new IlgiAlanlari
                {
                    IlgiAdi = model.IlgiAdi,
                    IlgiAciklamasi = model.IlgiAciklamasi,
                    Ogrenciler = nextOgrenci,
                };

                db.IlgiAlanlari.Add(yeniIlgiAlani);
                db.SaveChanges();

                return RedirectToAction("IlgiAlanlari");
            }

            return View(model);
        }

        public ActionResult IlgiAlanlariSil(int IlgiAlaniID)
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }
            var ılgialanlari = db.IlgiAlanlari.Find(IlgiAlaniID);
            db.IlgiAlanlari.Remove(ılgialanlari);
            db.SaveChanges();
            return RedirectToAction("IlgiAlanlari");

        }

        public ActionResult IlgiAlanlariDuzenle(int? IlgiAlaniID)
        {
            IlgiAlanlari ilgialani = null;

            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            if (IlgiAlaniID != null)
            {
                ilgialani = db.IlgiAlanlari.Where(x => x.IlgiID == IlgiAlaniID).FirstOrDefault();
            }

            return View(ilgialani);
        }

        [HttpPost]
        public ActionResult IlgiAlanlariDuzenle(IlgiAlanlari model)
        {
            IlgiAlanlari ilgialani = db.IlgiAlanlari.Where(x => x.IlgiID == model.IlgiID).FirstOrDefault();

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);  

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (ilgialani != null)
            {

                ilgialani.IlgiAdi = model.IlgiAdi;
                ilgialani.IlgiAciklamasi = model.IlgiAciklamasi;
                ilgialani.Ogrenciler = nextOgrenci;
                ilgialani.IlgiID = model.IlgiID;

                db.SaveChanges();
            }
            return RedirectToAction("IlgiAlanlari");
        }
    }
}