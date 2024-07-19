using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Deneme.Controllers
{
    public class PortfolyoController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();

        public ActionResult OgrenciBilgileri()
        {
            int? nextOgrenciID = Session["NextOgrenciID"] as int?;

            if (nextOgrenciID == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (nextOgrenci == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            return View(new List<Ogrenciler> { nextOgrenci });
        }

        public ActionResult OgrenciDuzenle(int? ogrenciID)
        {
            Ogrenciler ogrenci = null;

            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            if (ogrenciID != null)
            {
                ogrenci = db.Ogrenciler.Where(x => x.OgrenciID == ogrenciID).FirstOrDefault();
            }

            return View(ogrenci);
        }

        [HttpPost]
        public ActionResult OgrenciDuzenle(Ogrenciler model)
        {

            if (Session["NextOgrenciID"] == null)
            {

                return RedirectToAction("GirisYap", "Giris");
            }

            Ogrenciler ogrenci = db.Ogrenciler.Where(x => x.OgrenciID == model.OgrenciID).FirstOrDefault();

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (ogrenci != null)
            {

                ogrenci.OgrenciAdi = model.OgrenciAdi;
                ogrenci.OgrenciSoyadi = model.OgrenciSoyadi;
                ogrenci.OgrenciMail = ogrenci.OgrenciMail;
                ogrenci.TelefonNumarasi = model.TelefonNumarasi;
                ogrenci.Sifre = model.Sifre;

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
            return RedirectToAction("OgrenciBilgileri");
        }

        public class PortfolyoViewModel
        {
            public List<Ogrenciler> Ogrenciler { get; set; }
            public List<IlgiAlanlari> IlgiAlanlari { get; set; }
            public List<Sertifika> Sertifikalar { get; set; }
            public List<Yetenekler> Yetenekler { get; set; }
            public List<Deneyimler> Deneyimler { get; set; }
            public List<Egitimler> Egitimler { get; set; }
        }

        public ActionResult Portfolyo()
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

            var ilgiAlanlari = db.IlgiAlanlari.Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID).ToList();

            var sertifikalar = db.Sertifikalar.Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID).ToList();

            var yetenekler = db.Yetenekler.Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID).ToList();

            var deneyimler = db.Deneyimler.Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID).ToList();

            var egitimler = db.Egitimler.Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID).ToList();

            var viewModel = new PortfolyoViewModel
            {
                Ogrenciler = new List<Ogrenciler> { nextOgrenci },
                IlgiAlanlari = ilgiAlanlari,
                Sertifikalar = sertifikalar,
                Yetenekler = yetenekler,
                Deneyimler = deneyimler,
                Egitimler = egitimler
            };

            return View(viewModel);
        }

        public ActionResult IsverenPortfolyo(int? ogrenciId) // Make the parameter nullable
        {
            if (ogrenciId == null) // Check if ogrenciId is null
            {
                return RedirectToAction("IsverenBasvurular", "Ilanlar");
            }

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == ogrenciId);

            if (nextOgrenci == null)
            {
                return RedirectToAction("IsverenBasvurular", "Ilanlar");

            }

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

            var ilgiAlanlari = db.IlgiAlanlari.Where(x => x.Ogrenciler.OgrenciID == ogrenciId).ToList();
            var sertifikalar = db.Sertifikalar.Where(x => x.Ogrenciler.OgrenciID == ogrenciId).ToList();
            var yetenekler = db.Yetenekler.Where(x => x.Ogrenciler.OgrenciID == ogrenciId).ToList();
            var deneyimler = db.Deneyimler.Where(x => x.Ogrenciler.OgrenciID == ogrenciId).ToList();
            var egitimler = db.Egitimler.Where(x => x.Ogrenciler.OgrenciID == ogrenciId).ToList();

            var viewModel = new PortfolyoViewModel
            {
                Ogrenciler = new List<Ogrenciler> { nextOgrenci },
                IlgiAlanlari = ilgiAlanlari,
                Sertifikalar = sertifikalar,
                Yetenekler = yetenekler,
                Deneyimler = deneyimler,
                Egitimler = egitimler
            };

            return View(viewModel);
        }


    }
}
