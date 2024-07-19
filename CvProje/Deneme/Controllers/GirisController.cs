using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class GirisController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();

        [HttpGet]
        public ActionResult GirisYap()
        {
            AnatabloVM mod = new AnatabloVM();
            mod.Icerigler = db.Icerigler.ToList();
            return View(mod);
        }

        public ActionResult OgrenciGirisYap()
        {
            return View();
        }


        public ActionResult IsverenGirisYap()
        {
            return View();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CikisYap()
        {
            Session["NextOgrenciID"] = null;
            Session["NextIsverenID"] = null;
            Session.Clear();

            return RedirectToAction("GirisYap", "Giris");
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AnasayfaOgrenci()
        {
            if (Session["Mail"] != null)
            {
                string ogrenciMail = Session["Mail"].ToString();
                var currentOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciMail == ogrenciMail);
                if (currentOgrenci != null)
                {
                    int nextOgrenciID = currentOgrenci.OgrenciID;

                    var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

                    if (nextOgrenci != null)
                    {
                        Session["NextOgrenciID"] = nextOgrenciID;
                        ViewBag.OgrenciAdi = nextOgrenci.OgrenciAdi;
                        return View();
                    }
                }
            }
            return RedirectToAction("GirisYap");
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AnasayfaIsveren()
        {
            if (Session["Mail"] != null)
            {
                string isverenMail = Session["Mail"].ToString();

                var currentIsveren = db.Isverenler.FirstOrDefault(x => x.SirketMail == isverenMail);
                if (currentIsveren != null)
                {
                    int nextIsverenID = currentIsveren.IsverenID;

                    var nextIsveren = db.Isverenler.FirstOrDefault(x => x.IsverenID == nextIsverenID);

                    if (nextIsveren != null)
                    {
                        Session["NextIsverenID"] = nextIsverenID;
                        ViewBag.IsverenAdi = nextIsveren.SirketAdi;
                        return View();
                    }
                }
            }
            return RedirectToAction("GirisYap");
        }




        [HttpPost]
        public ActionResult OgrenciGirisYap(Ogrenciler ogrenciler)
        {
            var ogrenciDB = db.Ogrenciler.Where(x => x.OgrenciMail == ogrenciler.OgrenciMail && x.Sifre == ogrenciler.Sifre).FirstOrDefault();
            if (ogrenciDB != null)
            {
                Session["Mail"] = ogrenciler.OgrenciMail.ToString();
                Session["ID"] = ogrenciler.OgrenciID.ToString();

                return RedirectToAction("AnasayfaOgrenci", "Giris");
            }

            else
            {
                ViewBag.giris = "E-mail veya parola hatalı";

            }

            return View();

        }

        [HttpPost]
        public ActionResult IsverenGirisYap(Isverenler ısverenler)
        {
            var isverenDB = db.Isverenler.Where(x => x.SirketMail == ısverenler.SirketMail && x.Sifre == ısverenler.Sifre).FirstOrDefault();
            if (isverenDB != null)
            {
                Session["Mail"] = ısverenler.SirketMail.ToString();
                Session["ID"] = ısverenler.IsverenID.ToString();
                return RedirectToAction("AnasayfaIsveren", "Giris");
            }
            else
            {
                ViewBag.giris = "E-mail veya parola hatalı";

            }

            return View();

        }




        public ActionResult OgrenciKayit()
        {
            ViewBag.UniversiteList = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");
            ViewBag.BolumList = new SelectList(db.Bolumler.ToList(), "BolumID", "BolumAdi");

            return View();
        }

        [HttpPost]
        public ActionResult OgrenciKayit(Ogrenciler ogrenciler)
        {
            VeritabaniContext db = new VeritabaniContext();


            if (string.IsNullOrEmpty(ogrenciler.OgrenciMail) || string.IsNullOrEmpty(ogrenciler.TelefonNumarasi) || string.IsNullOrEmpty(ogrenciler.OgrenciAdi) || string.IsNullOrEmpty(ogrenciler.OgrenciSoyadi))
            {
                ViewBag.result = "Lütfen tüm alanları doldurunuz.";
                ViewBag.status = "warning";
                return View();
            }

            if (!ogrenciler.OgrenciMail.EndsWith("edu.tr", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.result = "Lütfen mailinizi doğru giriniz. Örnek olarak ÖğrenciNumarası@ogrenci.üniversiteniz.edu.tr";
                ViewBag.status = "danger";
                return View();
            }

            if (db.Ogrenciler.Any(o => o.OgrenciMail == ogrenciler.OgrenciMail))
            {
                ViewBag.result = "Bu e-posta adresi zaten kayıtlı.";
                ViewBag.status = "danger";
                return View();
            }

            db.Ogrenciler.Add(ogrenciler);
            int sonuc = db.SaveChanges();
            if (sonuc > 0)
            {
                ViewBag.result = "Kayıt oluşturuldu.";
                ViewBag.status = "success";
            }
            else
            {
                ViewBag.result = "Kayıt Başarısız";
                ViewBag.status = "danger";
            }

            return View();
        }


        public ActionResult IsverenKayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IsverenKayit(Isverenler ısverenler)
        {
            VeritabaniContext db = new VeritabaniContext();

            if (db.Isverenler.Any(o => o.SirketMail == ısverenler.SirketMail))
            {
                ViewBag.result = "Bu e-posta adresi zaten kayıtlı.";
                ViewBag.status = "danger";
                return View();
            }

            db.Isverenler.Add(ısverenler);
            int sonuc = db.SaveChanges();
            if (sonuc > 0)
            {
                ViewBag.result = "Kayıt oluşturuldu.";
                ViewBag.status = "success";
            }

            else
            {
                ViewBag.result = "Kayıt Başarısız";
                ViewBag.status = "danger";
            }

            return View();
        }



    }
}
