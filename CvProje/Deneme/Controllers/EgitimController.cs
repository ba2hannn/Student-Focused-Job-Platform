using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class EgitimController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();
        public ActionResult EgitimEkle()
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }
            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);
            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (nextOgrenci != null)
            {
                ViewBag.OgrenciAdi = nextOgrenci.OgrenciAdi; //kullanıcı adı geldi
            }


            ViewBag.BolumList = new SelectList(db.Bolumler.ToList(), "BolumID", "BolumAdi");
            ViewBag.UniversitelerList = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");
            return View();
        }

        public ActionResult Egitim()
        {
            int ogrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            if (ogrenciID == 0)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            var egitimler = db.Egitimler.Where(e => e.Ogrenciler.OgrenciID == ogrenciID).ToList();

            return View(egitimler);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EgitimEkle(Egitimler model, int UniversiteID, int BolumID)
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
                var yeniEgitim = new Egitimler
                {
                    Sinif = model.Sinif,
                    GNO = model.GNO,
                    Ogrenciler = nextOgrenci,
                    Universiteler = db.Universiteler.FirstOrDefault(d => d.UniversiteID == UniversiteID),
                    Bolum = db.Bolumler.FirstOrDefault(s => s.BolumID == BolumID)
                };

                db.Egitimler.Add(yeniEgitim); 
                db.SaveChanges();

                return RedirectToAction("Egitim");
            }

            ViewBag.BolumList = new SelectList(db.Bolumler.ToList(), "BolumID", "BolumAdi");
            ViewBag.UniversitelerList = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");
            return View(model);
        }

        public ActionResult EgitimSil(int egitimID)
        {
            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }
            var egitim = db.Egitimler.Find(egitimID);
            db.Egitimler.Remove(egitim);
            db.SaveChanges();
            return RedirectToAction("Egitim");

        }

        public ActionResult EgitimDuzenle(int? egitimID)
        {
            Egitimler egitim = null;

            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            if (egitimID != null)
            {
                egitim = db.Egitimler.Include("Bolum").Include("Universiteler").Where(x => x.EgitimID == egitimID).FirstOrDefault();
            }

            ViewBag.BolumList = new SelectList(db.Bolumler.ToList(), "BolumID", "BolumAdi");
            ViewBag.UniversitelerList = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");

            return View(egitim);
        }

        [HttpPost]
        public ActionResult EgitimDuzenle(Egitimler model)
        {
            Egitimler egitim = db.Egitimler.Include("Bolum").Include("Universiteler").Where(x => x.EgitimID == model.EgitimID).FirstOrDefault();

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (egitim != null)
            {
                var updatedBolum = db.Bolumler.FirstOrDefault(s => s.BolumID == model.Bolum.BolumID);
                var updatedUniversite = db.Universiteler.FirstOrDefault(u => u.UniversiteID == model.Universiteler.UniversiteID);

                if (updatedBolum != null && updatedUniversite != null)
                {
                    egitim.Sinif = model.Sinif;
                    egitim.GNO = model.GNO;

                    egitim.Bolum = updatedBolum;
                    egitim.Universiteler = updatedUniversite;
                    egitim.Ogrenciler = nextOgrenci;
                    egitim.EgitimID = model.EgitimID;

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
                else
                {
                    ViewBag.result = "Bolum veya Universite bulunamadı.";
                    ViewBag.status = "danger";
                }
            }

            ViewBag.BolumList = new SelectList(db.Bolumler.ToList(), "BolumID", "BolumAdi");
            ViewBag.UniversitelerList = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");

            return View(egitim);
        }
    }
}