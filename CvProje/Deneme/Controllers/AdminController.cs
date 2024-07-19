using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Deneme.Models;
using Deneme.Models.Managers;

namespace Deneme.Controllers
{
    public class AdminController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();


        public ActionResult DepartmanSil(int DepartmanID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var departman = db.Departmanlar.Find(DepartmanID);
                    if (departman != null)
                    {
                       var ilgiliIlanlar = db.IsIlanalari.Where(il => il.Departmanlar.DepartmanID == DepartmanID).ToList();
                        foreach (var ilan in ilgiliIlanlar)
                        {
                            var ilgiliBasvurular = db.Basvurular.Where(b => b.IsIlanlari.IsIlaniID == ilan.IsIlaniID).ToList();
                            foreach (var basvuru in ilgiliBasvurular)
                            {
                                db.Basvurular.Remove(basvuru);
                            }

                           var ilgiliFavoriIlanlar = db.FavoriIlanlar.Where(f => f.IsIlanlari.IsIlaniID == ilan.IsIlaniID).ToList();
                            foreach (var favori in ilgiliFavoriIlanlar)
                            {
                                db.FavoriIlanlar.Remove(favori);
                            }

                            db.IsIlanalari.Remove(ilan);
                        }

                        db.Departmanlar.Remove(departman);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("DepartmanIslemleri");
        }


        public ActionResult IsIlanlariSil(int IlanID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ilan = db.IsIlanalari.Find(IlanID);
                    if (ilan != null)
                    {
                        var ilgiliIletisimler = db.Iletisim.Where(i => i.IsIlanlari.IsIlaniID == IlanID).ToList();
                        foreach (var iletisim in ilgiliIletisimler)
                        {
                            db.Iletisim.Remove(iletisim);
                        }

                        var ilgiliBasvurular = db.Basvurular.Where(b => b.IsIlanlari.IsIlaniID == IlanID).ToList();
                        foreach (var basvuru in ilgiliBasvurular)
                        {
                            db.Basvurular.Remove(basvuru);
                        }

                        var ilgiliFavoriIlanlar = db.FavoriIlanlar.Where(f => f.IsIlanlari.IsIlaniID == IlanID).ToList();
                        foreach (var favori in ilgiliFavoriIlanlar)
                        {
                            db.FavoriIlanlar.Remove(favori);
                        }

                        db.IsIlanalari.Remove(ilan);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("IsIlanlariIslemleri");
        }

        public ActionResult BolumSil(int BolumID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var bolum = db.Bolumler.Find(BolumID);
                    if (bolum != null)
                    {
                        var ilgiliEgitimler = db.Egitimler.Where(e => e.Bolum.BolumID == BolumID).ToList();
                        foreach (var egitim in ilgiliEgitimler)
                        {
                            db.Egitimler.Remove(egitim);
                        }

                        var ilgiliDersIcerikleri = db.Icerigler.Where(di => di.Bolum.BolumID == BolumID).ToList();
                        foreach (var dersIcerigi in ilgiliDersIcerikleri)
                        {
                            db.Icerigler.Remove(dersIcerigi);
                        }

                        db.Bolumler.Remove(bolum);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("BolumIslemleri");
        }

        public ActionResult DeneyimSil(int DeneyimID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var deneyim = db.Deneyimler.Find(DeneyimID);
                    if (deneyim != null)
                    {
                        db.Deneyimler.Remove(deneyim);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("DeneyimIslemleri");
        }

        public ActionResult DersIcerigiSil(int IcerigID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var dersIcerigi = db.Icerigler.Find(IcerigID);
                    if (dersIcerigi != null)
                    {
                        db.Icerigler.Remove(dersIcerigi);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("DersIcerigiIslemleri");
        }

        public ActionResult DestekSil(int DestekID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var destek = db.Destek.Find(DestekID);
                    if (destek != null)
                    {
                        db.Destek.Remove(destek);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("DestekIslemleri");
        }

        public ActionResult EgitimSil(int EgitimID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var egitim = db.Egitimler.Find(EgitimID);
                    if (egitim != null)
                    {
                        db.Egitimler.Remove(egitim);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("EgitimIslemleri");
        }

        public ActionResult FavoriIlanSil(int FavoriID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var favoriIlan = db.FavoriIlanlar.Find(FavoriID);
                    if (favoriIlan != null)
                    {
                        db.FavoriIlanlar.Remove(favoriIlan);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("FavoriIlanIslemleri");
        }

        public ActionResult IletisimSil(int IletisimID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var iletisim = db.Iletisim.Find(IletisimID);
                    if (iletisim != null)
                    {
                        db.Iletisim.Remove(iletisim);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("IletisimlerIslemleri");
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CikisYap()
        {
            Session["NextOgrenciID"] = null;
            Session["NextIsverenID"] = null;
            Session["AdminMail"] = null;
            Session.Clear();

            return RedirectToAction("AdminGiris");
        }

        public ActionResult IlgiAlanSil(int IlgiID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ilgi = db.IlgiAlanlari.Find(IlgiID);
                    if (ilgi != null)
                    {
                        db.IlgiAlanlari.Remove(ilgi);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("IlgiAlanIslemleri");
        }

        public ActionResult IsverenSil(int IsverenID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var isveren = db.Isverenler.Find(IsverenID);
                    if (isveren != null)
                    {
                        var ilgiliDestekler = db.Destek.Where(d => d.Isverenler.IsverenID == IsverenID).ToList();
                        foreach (var destek in ilgiliDestekler)
                        {
                            db.Destek.Remove(destek);
                        }
                        var ilgiliIlanlar = db.IsIlanalari.Where(il => il.Isverenler.IsverenID == IsverenID).ToList();
                        foreach (var ilan in ilgiliIlanlar)
                        {
                            var ilgiliBasvurular = db.Basvurular.Where(b => b.IsIlanlari.IsIlaniID == ilan.IsIlaniID).ToList();
                            foreach (var basvuru in ilgiliBasvurular)
                            {
                                db.Basvurular.Remove(basvuru);
                            }

                            var ilgiliFavoriIlanlar = db.FavoriIlanlar.Where(f => f.IsIlanlari.IsIlaniID == ilan.IsIlaniID).ToList();
                            foreach (var favori in ilgiliFavoriIlanlar)
                            {
                                db.FavoriIlanlar.Remove(favori);
                            }

                            var ilgiliIletisimler = db.Iletisim.Where(i => i.IsIlanlari.IsIlaniID == ilan.IsIlaniID).ToList();
                            foreach (var iletisim in ilgiliIletisimler)
                            {
                                db.Iletisim.Remove(iletisim);
                            }

                            db.IsIlanalari.Remove(ilan);
                        }

                        // Finally, remove the Isveren
                        db.Isverenler.Remove(isveren);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("IsverenlerIslemleri");
        }




        public ActionResult OgrenciSil(int OgrenciID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ogrenci = db.Ogrenciler.Find(OgrenciID);
                    if (ogrenci != null)
                    {
                        var ilgiliSertifikalar = db.Sertifikalar.Where(s => s.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var sertifika in ilgiliSertifikalar)
                        {
                            db.Sertifikalar.Remove(sertifika);
                        }

                        var ilgiliYetenekler = db.Yetenekler.Where(y => y.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var yetenek in ilgiliYetenekler)
                        {
                            db.Yetenekler.Remove(yetenek);
                        }

                        var ilgiliEgitimler = db.Egitimler.Where(e => e.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var egitim in ilgiliEgitimler)
                        {
                            db.Egitimler.Remove(egitim);
                        }

                        var ilgiliDeneyimler = db.Deneyimler.Where(d => d.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var deneyim in ilgiliDeneyimler)
                        {
                            db.Deneyimler.Remove(deneyim);
                        }

                        var ilgiliIlgiAlanlari = db.IlgiAlanlari.Where(ia => ia.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var ilgi in ilgiliIlgiAlanlari)
                        {
                            db.IlgiAlanlari.Remove(ilgi);
                        }

                        var ilgiliBasvurular = db.Basvurular.Where(b => b.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var basvuru in ilgiliBasvurular)
                        {
                            db.Basvurular.Remove(basvuru);
                        }

                        var ilgiliFavoriIlanlar = db.FavoriIlanlar.Where(f => f.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var favori in ilgiliFavoriIlanlar)
                        {
                            db.FavoriIlanlar.Remove(favori);
                        }

                        var ilgiliIletisimler = db.Iletisim.Where(f => f.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var iletisim in ilgiliIletisimler)
                        {
                            db.Iletisim.Remove(iletisim);
                        }

                        var ilgiliDestek = db.Destek.Where(f => f.Ogrenciler.OgrenciID == OgrenciID).ToList();
                        foreach (var destekmesajlari in ilgiliDestek)
                        {
                            db.Destek.Remove(destekmesajlari);
                        }

                        db.Ogrenciler.Remove(ogrenci);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("OgrencilerIslemleri");
        }

        public ActionResult SektorDuzenle(int sektorID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            var sektor = db.Sektorler.Find(sektorID);

            return View(sektor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SektorDuzenle(Sektorler model)
        {

            var sektor = db.Sektorler.Find(model.SektorID);

            if (sektor != null)
            {
                sektor.SektorAdi = model.SektorAdi;

                db.SaveChanges();
            }

            return RedirectToAction("SektorlerIslemleri");
        }

        public ActionResult SektorSil(int SektorID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sektor = db.Sektorler.Find(SektorID);
                    if (sektor != null)
                    {
                        var ilgiliDersIcerikleri = db.Icerigler.Where(di => di.Sektorler.SektorID == SektorID).ToList();
                        foreach (var dersIcerigi in ilgiliDersIcerikleri)
                        {
                            db.Icerigler.Remove(dersIcerigi);
                        }

                        var ilgiliIlanlar = db.IsIlanalari.Where(il => il.Sektorler.SektorID == SektorID).ToList();
                        foreach (var ilan in ilgiliIlanlar)
                        {
                            var ilgiliBasvurular = db.Basvurular.Where(b => b.IsIlanlari.IsIlaniID == ilan.IsIlaniID).ToList();
                            foreach (var basvuru in ilgiliBasvurular)
                            {
                                db.Basvurular.Remove(basvuru);
                            }

                            var ilgiliFavoriIlanlar = db.FavoriIlanlar.Where(f => f.IsIlanlari.IsIlaniID == ilan.IsIlaniID).ToList();
                            foreach (var favori in ilgiliFavoriIlanlar)
                            {
                                db.FavoriIlanlar.Remove(favori);
                            }

                            db.IsIlanalari.Remove(ilan);
                        }

                        db.Sektorler.Remove(sektor);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("SektorlerIslemleri");
        }

        public ActionResult UniversiteSil(int UniversiteID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var universite = db.Universiteler.Find(UniversiteID);
                    if (universite != null)
                    {
                        var ilgiliBolumler = db.Bolumler.Where(b => b.Universite.UniversiteID == UniversiteID).ToList();
                        foreach (var bolum in ilgiliBolumler)
                        {
                            var ilgiliEgitimler = db.Egitimler.Where(e => e.Bolum.BolumID == bolum.BolumID).ToList();
                            foreach (var egitim in ilgiliEgitimler)
                            {
                                db.Egitimler.Remove(egitim);
                            }

                            var ilgiliDersIcerikleri = db.Icerigler.Where(di => di.Bolum.BolumID == bolum.BolumID).ToList();
                            foreach (var dersIcerigi in ilgiliDersIcerikleri)
                            {
                                db.Icerigler.Remove(dersIcerigi);
                            }

                            db.Bolumler.Remove(bolum);
                        }

                        db.Universiteler.Remove(universite);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("UniversitelerIslemleri");
        }

        public ActionResult SertifikaSil(int SertifikaID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sertifika = db.Sertifikalar.Find(SertifikaID);
                    if (sertifika != null)
                    {
                        db.Sertifikalar.Remove(sertifika);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("SertifikaIslemleri");
        }


        public ActionResult YetenekSil(int YetenekID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var yetenek = db.Yetenekler.Find(YetenekID);
                    if (yetenek != null)
                    {
                        db.Yetenekler.Remove(yetenek);
                        db.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            return RedirectToAction("YetenekIslemleri");
        }

        public ActionResult DestekIslemleri(AdminPaneli adminPaneli)
        {

            var adminDB = db.AdminPaneli.Where(x => x.AdminMail == adminPaneli.AdminMail && x.AdminSifre == adminPaneli.AdminSifre).FirstOrDefault();
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }


            List<Destek> Destek = db.Destek.ToList();

            return View(Destek);
        }

        public ActionResult AdminGiris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminGiris(AdminPaneli adminPaneli)
        {
            Session["AdminMail"] = adminPaneli.AdminMail.ToString();

            var adminDB = db.AdminPaneli.Where(x => x.AdminMail == adminPaneli.AdminMail && x.AdminSifre == adminPaneli.AdminSifre).FirstOrDefault();
            if (adminDB != null)
            {

                return RedirectToAction("AnasayfaAdmin", "Admin");
            }

            else
            {
                ViewBag.giris = "E-mail veya parola hatalı";

            }

            return View();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AnasayfaAdmin()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            return View();
        }
        public ActionResult BolumIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Bolum> bolumler = db.Bolumler.Include("Universite").ToList();

            ViewBag.UniversiteList = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");

            return View(bolumler);
        }




        public ActionResult BolumDuzenle(int? bolumID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            Bolum bolum = null;

            if (bolumID != null)
            {
                bolum = db.Bolumler.Where(x => x.BolumID == bolumID).FirstOrDefault();
            }

            return View(bolum);
        }

        [HttpPost]
        public ActionResult BolumDuzenle(Bolum model)
        {
            Bolum bolum = db.Bolumler.Where(x => x.BolumID == model.BolumID).FirstOrDefault();

            if (bolum != null)
            {

                bolum.BolumAdi = model.BolumAdi;
                bolum.BolumID = model.BolumID;

                db.SaveChanges();
            }
            return RedirectToAction("BolumIslemleri");
        }


        public ActionResult BolumEkle()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            ViewBag.Universiteler = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BolumEkle(Bolum model, int UniversiteID)
        {
            if (ModelState.IsValid)
            {
                try
                {

                   var universite = db.Universiteler.FirstOrDefault(u => u.UniversiteID == UniversiteID);
                    if (universite == null)
                    {
                        ModelState.AddModelError("", "Seçilen üniversite bulunamadı.");
                        ViewBag.Universiteler = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");
                        return View(model);
                    }


                    model.Universite = universite;

                    db.Bolumler.Add(model);
                    db.SaveChanges();

                    ViewBag.SuccessMessage = "Bölüm başarıyla eklendi.";
                    return RedirectToAction("BolumIslemleri");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }


            ViewBag.Universiteler = new SelectList(db.Universiteler.ToList(), "UniversiteID", "UniversiteAdi");
            return View(model);
        }


        public ActionResult DepartmanIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Departmanlar> departmanlar = db.Departmanlar.ToList();
            return View(departmanlar);
        }




        public ActionResult DepartmanEkle()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DepartmanEkle(string departmanAdi)
        {
            var yeniDepartman = new Departmanlar { DepartmanAdi = departmanAdi };

            db.Departmanlar.Add(yeniDepartman);
            db.SaveChanges();

            return RedirectToAction("DepartmanIslemleri");
        }


        public ActionResult DepartmanDuzenle(int departmanID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            var departman = db.Departmanlar.Find(departmanID);
            return View(departman);
        }

        [HttpPost]
        public ActionResult DepartmanDuzenle(Departmanlar model)
        {
            var departman = db.Departmanlar.Find(model.DepartmanID);

            if (departman != null)
            {
                departman.DepartmanAdi = model.DepartmanAdi;
                db.SaveChanges();
            }

            return RedirectToAction("DepartmanIslemleri");
        }



        public ActionResult DersIcerigiIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<DersIcerigi> dersIcerikleri = db.Icerigler.ToList();
            return View(dersIcerikleri);
        }

        public ActionResult DersIcerigSil(int IcerigID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }



            var bolum = db.Icerigler.Find(IcerigID);
            if (bolum != null)
            {
                db.Icerigler.Remove(bolum);
                db.SaveChanges();
            }

            return RedirectToAction("DersIcerigiIslemleri");
        }


        public ActionResult DersIcerigiEkle()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            ViewBag.SektorlerList = new SelectList(db.Sektorler.ToList(), "SektorID", "SektorAdi");
            ViewBag.BolumlerList = new SelectList(db.Bolumler.ToList(), "BolumID", "BolumAdi");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DersIcerigiEkle(DersIcerigi model, int SektorID, int BolumID)
        {
            model.Sektorler = db.Sektorler.FirstOrDefault(s => s.SektorID == SektorID);
            model.Bolum = db.Bolumler.FirstOrDefault(b => b.BolumID == BolumID);
            db.Icerigler.Add(model);
            db.SaveChanges();

            return RedirectToAction("DersIcerigiIslemleri");
        }




        public ActionResult DersIcerigiDuzenle(int icerikID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            var dersIcerigi = db.Icerigler.Find(icerikID);

            ViewBag.SektorList = new SelectList(db.Sektorler.ToList(), "SektorAdi", "SektorAdi");

            ViewBag.BolumList = new SelectList(db.Bolumler.ToList(), "BolumAdi", "BolumAdi");

            return View(dersIcerigi);
        }

        [HttpPost]
        public ActionResult DersIcerigiDuzenle(DersIcerigi model)
        {
            var dersIcerigi = db.Icerigler.Find(model.IcerigID);

            if (dersIcerigi != null)
            {
                dersIcerigi.IcerigAdi = model.IcerigAdi;
                dersIcerigi.Sinif = model.Sinif;
                dersIcerigi.Sektorler.SektorAdi = model.Sektorler.SektorAdi;
                dersIcerigi.Bolum.BolumAdi = model.Bolum.BolumAdi;

                db.SaveChanges();
            }

            return RedirectToAction("DersIcerigiIslemleri");
        }


        public ActionResult EgitimIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Egitimler> egitimler = db.Egitimler.ToList();
            return View(egitimler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EgitimEkle(string sinif, float gno, string ogrenciAdi, string universiteAdi, string bolumAdi)
        {
            var ogrenci = db.Ogrenciler.FirstOrDefault(o => o.OgrenciAdi == ogrenciAdi) ?? new Ogrenciler { OgrenciAdi = ogrenciAdi };
            var universite = db.Universiteler.FirstOrDefault(u => u.UniversiteAdi == universiteAdi) ?? new Universiteler { UniversiteAdi = universiteAdi };
            var bolum = db.Bolumler.FirstOrDefault(b => b.BolumAdi == bolumAdi) ?? new Bolum { BolumAdi = bolumAdi };

            var yeniEgitim = new Egitimler
            {
                Sinif = int.Parse(sinif),
                GNO = gno,
                Ogrenciler = ogrenci,
                Universiteler = universite,
                Bolum = bolum
            };

            db.Egitimler.Add(yeniEgitim);
            db.SaveChanges();

            return RedirectToAction("EgitimIslemleri");
        }



        public ActionResult EgitimDuzenle(int egitimID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            var egitim = db.Egitimler.Find(egitimID);

            ViewBag.UniversiteList = new SelectList(db.Universiteler.ToList(), "UniversiteAdi", "UniversiteAdi");
            ViewBag.BolumList = new SelectList(db.Bolumler.ToList(), "BolumAdi", "BolumAdi");

            return View(egitim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EgitimDuzenle(Egitimler model)
        {
            var egitim = db.Egitimler.Find(model.EgitimID);

            if (egitim != null)
            {
                egitim.Sinif = model.Sinif;
                egitim.GNO = model.GNO;
                egitim.Ogrenciler.OgrenciAdi = model.Ogrenciler.OgrenciAdi;
                egitim.Universiteler.UniversiteAdi = model.Universiteler.UniversiteAdi;
                egitim.Bolum.BolumAdi = model.Bolum.BolumAdi;

                db.SaveChanges();
            }

            return RedirectToAction("EgitimIslemleri");
        }


        public ActionResult FavoriIlanlarIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<FavoriIlanlar> favoriIlanlar = db.FavoriIlanlar.Include("IsIlanlari").ToList();
            return View(favoriIlanlar);
        }


        public ActionResult IsIlanlariIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<IsIlanlari> isIlanlari = db.IsIlanalari.ToList();
            return View(isIlanlari);
        }



        public ActionResult IsverenlerIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Isverenler> isverenler = db.Isverenler.ToList();
            return View(isverenler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsverenEkle(string sirketAdi, string sektor, string sirketMail, string telefon, string sirketAdres, string sifre)
        {

            Isverenler yeniIsveren = new Isverenler();
            yeniIsveren.SirketAdi = sirketAdi;
            yeniIsveren.SirketMail = sirketMail;
            yeniIsveren.telefon = telefon;
            yeniIsveren.SirketAdres = sirketAdres;
            yeniIsveren.Sifre = sifre;

            db.Isverenler.Add(yeniIsveren);
            db.SaveChanges();

            return RedirectToAction("IsverenlerIslemleri");
        }



        public ActionResult OgrencilerIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Ogrenciler> ogrenciler = db.Ogrenciler.ToList();
            return View(ogrenciler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OgrenciEkle(string ogrenciAdi, string ogrenciSoyadi, string ogrenciMail, string telefonNumarasi, string sifre)
        {
            Ogrenciler yeniOgrenci = new Ogrenciler();
            yeniOgrenci.OgrenciAdi = ogrenciAdi;
            yeniOgrenci.OgrenciSoyadi = ogrenciSoyadi;
            yeniOgrenci.OgrenciMail = ogrenciMail;
            yeniOgrenci.TelefonNumarasi = telefonNumarasi;
            yeniOgrenci.Sifre = sifre;

            db.Ogrenciler.Add(yeniOgrenci);
            db.SaveChanges();

            return RedirectToAction("OgrencilerIslemleri");
        }

        public ActionResult YetenekIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Yetenekler> yetenekler = db.Yetenekler.ToList();
            return View(yetenekler);
        }

        public ActionResult SektorlerIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Sektorler> sektorler = db.Sektorler.ToList();
            return View(sektorler);
        }

        public ActionResult IlgiAlanlari()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<IlgiAlanlari> alanlar = db.IlgiAlanlari.ToList();
            return View(alanlar);
        }

        public ActionResult DeneyimIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Deneyimler> deneyimler = db.Deneyimler.ToList();
            return View(deneyimler);
        }

        public ActionResult SertifikaIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Sertifika> sertifikalar = db.Sertifikalar.ToList();
            return View(sertifikalar);
        }

        public ActionResult UniversitelerIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Universiteler> universiteler = db.Universiteler.ToList();
            return View(universiteler);
        }

        public ActionResult UniversiteEkle()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UniversiteEkle(string universiteAdi)
        {

            var yeniUniversite = new Universiteler { UniversiteAdi = universiteAdi };

            db.Universiteler.Add(yeniUniversite);
            db.SaveChanges();

            return RedirectToAction("UniversitelerIslemleri");
        }


        public ActionResult UniversiteDuzenle(int universiteID)
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            var universite = db.Universiteler.Find(universiteID);

            return View(universite);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UniversiteDuzenle(Universiteler model)
        {

            if (ModelState.IsValid)
            {
                var universite = db.Universiteler.Find(model.UniversiteID);

                if (universite != null)
                {
                    universite.UniversiteAdi = model.UniversiteAdi;

                    db.SaveChanges();
                }

                return RedirectToAction("UniversitelerIslemleri");
            }

            return View(model);
        }


        public ActionResult IletisimlerIslemleri()
        {
            if (Session["AdminMail"] == null)
            {
                return RedirectToAction("AdminGiris", "Admin");
            }
            List<Iletisim> iletişimler = db.Iletisim.ToList();


            return View(iletişimler);
        }


    }
}
