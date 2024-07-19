using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class SertifikaController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();

        public ActionResult Sertifika()
        {
            int ogrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            if (ogrenciID == 0)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            var sertifikalar = db.Sertifikalar.Where(s => s.Ogrenciler.OgrenciID == ogrenciID).ToList();

            return View(sertifikalar);
        }

        public ActionResult SertifikaEkle()
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            return View();
        }


        [HttpPost]
        public ActionResult SertifikaEkle(Sertifika model)
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
                var yeniSertifika = new Sertifika
                {
                    SertifikaAdi = model.SertifikaAdi,
                    SertifikaAciklamasi = model.SertifikaAciklamasi,
                    Ogrenciler = nextOgrenci,
                };

                db.Sertifikalar.Add(yeniSertifika);
                db.SaveChanges();

                return RedirectToAction("Sertifika");
            }

            return View(model);
        }

        public ActionResult SertifikaSil(int SertifikaID)
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }
            var sertifika = db.Sertifikalar.Find(SertifikaID);
            db.Sertifikalar.Remove(sertifika);
            db.SaveChanges();
            return RedirectToAction("Sertifika");

        }

        public ActionResult SertifikaDuzenle(int? sertifikaID)
        {
            Sertifika sertifika = null;

            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            if (sertifikaID != null)
            {
                sertifika = db.Sertifikalar.Where(x => x.SertifikaID == sertifikaID).FirstOrDefault();
            }

            return View(sertifika);
        }

        [HttpPost]
        public ActionResult SertifikaDuzenle(Sertifika model)
        {
            Sertifika sertifika = db.Sertifikalar.Where(x => x.SertifikaID == model.SertifikaID).FirstOrDefault();

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (sertifika != null)
            {

                sertifika.SertifikaAdi = model.SertifikaAdi;
                sertifika.SertifikaAciklamasi = model.SertifikaAciklamasi;
                sertifika.Ogrenciler = nextOgrenci;
                sertifika.SertifikaID = model.SertifikaID;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.result = "Kayıt Güncellendi.";
                    ViewBag.status = "success";
                }
                else
                {
                    ViewBag.result = "Kayıt Güncellenemedi";
                    ViewBag.status = "danger";
                }

            }
            return RedirectToAction("Sertifika");
        }
    }
}