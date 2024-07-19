using Deneme.Models;
using Deneme.Models.Managers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Deneme.Controllers
{
    public class IlanlarController : Controller
    {
        private VeritabaniContext db = new VeritabaniContext();
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IsFirsatlari()
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

            var egitimBilgileri = db.Egitimler.FirstOrDefault(e => e.Ogrenciler.OgrenciID == nextOgrenciID);

            if (egitimBilgileri == null)
            {
                return RedirectToAction("Egitim", "Egitim");
            }

            var dersIcerikleri = db.Icerigler
                                    .Where(d => d.Bolum.BolumID == egitimBilgileri.Bolum.BolumID && d.Sinif <= egitimBilgileri.Sinif)
                                    .ToList();

            var sektorIDler = dersIcerikleri.Select(d => d.Sektorler.SektorID).ToList();

            var isIlanlari = db.IsIlanalari
                                .Where(i => sektorIDler.Contains(i.Sektorler.SektorID))
                                .ToList();

            var viewModel = new AnatabloVM
            {
                IsIlanlari = isIlanlari,
            };

            return View(viewModel);
        }



        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpPost]
        public ActionResult IsFirsatlari(int ilanID)
        {
            if (Session["NextOgrenciID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            bool isFavoriteExists = db.FavoriIlanlar.Any(x => x.Ogrenciler.OgrenciID == nextOgrenciID && x.IsIlanlari.IsIlaniID == ilanID);

            if (isFavoriteExists == false)
            {
                FavoriIlanlar favori = new FavoriIlanlar
                {
                    Ogrenciler = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID),
                    IsIlanlari = db.IsIlanalari.FirstOrDefault(x => x.IsIlaniID == ilanID)
                };
                db.FavoriIlanlar.Add(favori);
                db.SaveChanges();

                return Json("success");
            }
            else
            {
                return Json("exists");
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult OgrenciMesajlar()
        {
            if (Session["NextOgrenciID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);

            if (nextOgrenci == null)
            {
                return HttpNotFound();
            }

            ViewBag.OgrenciAdi = nextOgrenci.OgrenciAdi;

            var mesajlarim = db.Iletisim
                                .Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID)
                                .GroupBy(x => new { x.IsIlanlari.Isverenler.SirketAdi, x.IsIlanlari.IsIlaniID })
                                .Select(g => g.FirstOrDefault())
                                .ToList();

            return View(mesajlarim);
        }



        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult OgrenciMesajlasma(int? ilanId)
        {
            if (Session["NextOgrenciID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);
            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);
            var ogrencibilgisi = nextOgrenci;


            TempData["IsIlaniID"] = ilanId;

            if (nextOgrenci == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            ViewBag.OgrenciAdi = ogrencibilgisi.OgrenciAdi;
            ViewBag.OgrenciBilgisi = ogrencibilgisi.OgrenciAdi + " " + ogrencibilgisi.OgrenciSoyadi;


            var iletisimBilgisi = db.Iletisim.Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID && x.IsIlanlari.IsIlaniID == ilanId).ToList();

            var isIlani = db.IsIlanalari.FirstOrDefault(i => i.IsIlaniID == ilanId);
            if (isIlani != null)
            {
                var isveren = db.Isverenler.FirstOrDefault(i => i.IsverenID == isIlani.Isverenler.IsverenID);
                if (isveren != null)
                {
                    ViewBag.IsverenAdi = isveren.SirketAdi;
                }
            }

            return View(iletisimBilgisi);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OgrenciMesajlasma(string OgrenciMesaj)
        {
            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);
            var nextOgrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);
            int ilanId = Convert.ToInt32(TempData["IsIlaniID"]);

            if (nextOgrenci != null)
            {
                var ilan = db.IsIlanalari.FirstOrDefault(d => d.IsIlaniID == ilanId);

                if (ilan != null)
                {
                    var yeniIletisim = new Iletisim
                    {
                        OgrenciMesaj = OgrenciMesaj,
                        Ogrenciler = nextOgrenci,
                        IsIlanlari = ilan
                    };

                    db.Iletisim.Add(yeniIletisim);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("OgrenciMesajlasma", new { ilanId = ilanId, ogrenciId = nextOgrenciID });
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IsverenMesajlar()
        {
            if (Session["NextIsverenID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextIsverenID = Convert.ToInt32(Session["NextIsverenID"]);

            var nextIsveren = db.Isverenler.FirstOrDefault(x => x.IsverenID == nextIsverenID);

            if (nextIsveren == null)
            {
                return HttpNotFound();
            }

            ViewBag.IsverenAdi = nextIsveren.SirketAdi;


            var isIlanIDler = db.IsIlanalari
                                .Where(ilan => ilan.Isverenler.IsverenID == nextIsverenID)
                                .Select(ilan => ilan.IsIlaniID)
                                .ToList();

            var mesajlarim = db.Iletisim
                                .Where(x => isIlanIDler.Contains(x.IsIlanlari.IsIlaniID))
                                .GroupBy(x => new { x.Ogrenciler.OgrenciID, x.IsIlanlari.IsIlaniID })
                                .Select(g => g.FirstOrDefault())
                                .ToList();

            return View(mesajlarim);
        }




        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IsverenMesajlasma(int? ilanId, int? ogrenciId)
        {
            if (Session["NextIsverenID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextIsverenID = Convert.ToInt32(Session["NextIsverenID"]);
            var nextIsveren = db.Isverenler.FirstOrDefault(x => x.IsverenID == nextIsverenID);

            if (nextIsveren == null)
            {
                return HttpNotFound();
            }

            ViewBag.IsverenAdi = nextIsveren.SirketAdi;

            var iletisimBilgisi = db.Iletisim
                                    .Where(x => x.IsIlanlari.IsIlaniID == ilanId && x.Ogrenciler.OgrenciID == ogrenciId)
                                    .OrderBy(x => x.IletisimID)
                                    .ToList();

            var ogrenci = db.Ogrenciler.FirstOrDefault(o => o.OgrenciID == ogrenciId);
            if (ogrenci != null)
            {
                ViewBag.OgrenciAdiSoyadi = ogrenci.OgrenciAdi + " " + ogrenci.OgrenciSoyadi;
            }

            return View(iletisimBilgisi);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsverenMesajlasma(int ilanId, int ogrenciId, string IsverenMesaj)
        {
            if (Session["NextIsverenID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextIsverenID = Convert.ToInt32(Session["NextIsverenID"]);
            var nextIsveren = db.Isverenler.FirstOrDefault(x => x.IsverenID == nextIsverenID);

            if (nextIsveren == null)
            {
                return HttpNotFound();
            }

            var ilan = db.IsIlanalari.FirstOrDefault(d => d.IsIlaniID == ilanId);
            var ogrenci = db.Ogrenciler.FirstOrDefault(d => d.OgrenciID == ogrenciId);

            if (ilan != null && ogrenci != null)
            {
                var yeniIletisim = new Iletisim
                {
                    IsverenMesaj = IsverenMesaj,
                    Ogrenciler = ogrenci,
                    IsIlanlari = ilan
                };

                db.Iletisim.Add(yeniIletisim);
                db.SaveChanges();
            }

            return RedirectToAction("IsverenMesajlasma", new { ilanId = ilanId, ogrenciId = ogrenciId });
        }



        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult FavoriIlanlarim()
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

            var favoriIlanlar = db.FavoriIlanlar
                                    .Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID)
                                    .Select(x => x.IsIlanlari)
                                    .ToList();

            return View(favoriIlanlar);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpPost]
        public ActionResult FavoriIlanlarim(int ilanID)
        {
            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            var favoriIlan = db.FavoriIlanlar.FirstOrDefault(x => x.Ogrenciler.OgrenciID == nextOgrenciID && x.IsIlanlari.IsIlaniID == ilanID);

            if (favoriIlan != null)
            {

                db.FavoriIlanlar.Remove(favoriIlan);
                db.SaveChanges();
            }
            return RedirectToAction("FavoriIlanlarim");
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IlanEkle()
        {
            if (Session["NextIsverenID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextIsverenID = Convert.ToInt32(Session["NextIsverenID"]);

            var nextIsveren = db.Isverenler.FirstOrDefault(x => x.IsverenID == nextIsverenID);

            if (nextIsveren != null)
            {
                ViewBag.IsverenID = nextIsverenID;
                ViewBag.IsverenAdi = nextIsveren.SirketAdi;
            }

            ViewBag.DepartmanlarList = new SelectList(db.Departmanlar.ToList(), "DepartmanID", "DepartmanAdi");
            ViewBag.SektorlerList = new SelectList(db.Sektorler.ToList(), "SektorID", "SektorAdi");

            return View();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpPost]
        public ActionResult IlanEkle(IsIlanlari ilan, int DepartmanID, int SektorID)
        {
            int nextIsverenID = Convert.ToInt32(Session["NextIsverenID"]);

            var nextIsveren = db.Isverenler.FirstOrDefault(x => x.IsverenID == nextIsverenID);

            if (nextIsveren != null)
            {
                ViewBag.IsverenID = nextIsverenID;
                ViewBag.IsverenAdi = nextIsveren.SirketAdi;
            }

            if (ModelState.IsValid)
            {
                ilan.Isverenler = nextIsveren;
                ilan.Departmanlar = db.Departmanlar.FirstOrDefault(d => d.DepartmanID == DepartmanID);
                ilan.Sektorler = db.Sektorler.FirstOrDefault(s => s.SektorID == SektorID);

                db.IsIlanalari.Add(ilan);
                db.SaveChanges();

                return Json(new { success = true, message = "İlan başarıyla eklendi." });
            }
            ViewBag.DepartmanlarList = new SelectList(db.Departmanlar.ToList(), "DepartmanID", "DepartmanAdi");
            ViewBag.SektorlerList = new SelectList(db.Sektorler.ToList(), "SektorID", "SektorAdi");

            return Json(new { success = false, message = "Form geçersiz. Lütfen tüm alanları doldurun." });
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IsIlanlari()
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

            AnatabloVM mod = new AnatabloVM();
            mod.IsIlanlari = db.IsIlanalari.ToList();

            return View(mod);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpPost]
        public ActionResult IsIlanlari(int ilanID)
        {
            if (Session["NextOgrenciID"] == null)
            {
                return RedirectToAction("GirisYap", "Giris");
            }

            int nextOgrenciID = Convert.ToInt32(Session["NextOgrenciID"]);

            bool isFavoriteExists = db.FavoriIlanlar.Any(x => x.Ogrenciler.OgrenciID == nextOgrenciID && x.IsIlanlari.IsIlaniID == ilanID);

            if (isFavoriteExists == false)
            {
                FavoriIlanlar favori = new FavoriIlanlar
                {
                    Ogrenciler = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID),
                    IsIlanlari = db.IsIlanalari.FirstOrDefault(x => x.IsIlaniID == ilanID)
                };
                db.FavoriIlanlar.Add(favori);
                db.SaveChanges();

                return Json("success");
            }
            else
            {
                return Json("exists");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IsIlanlarim()
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


            var isilanlari = db.IsIlanalari
                        .Where(x => x.Isverenler.IsverenID == nextIsverenID)
                        .ToList();


            var favoriIlanSayilari = db.FavoriIlanlar
                .Where(f => f.IsIlanlari.Isverenler.IsverenID == nextIsverenID)
                .GroupBy(f => f.IsIlanlari.IsIlaniID)
                .Select(g => new
                {
                    IsIlanID = g.Key,
                    FavoriSayisi = g.Count()
                }).ToList();

            ViewBag.FavoriIlanSayilari = favoriIlanSayilari.ToDictionary(f => f.IsIlanID, f => f.FavoriSayisi);

            return View(isilanlari);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IlanSil(int IlanID)
        {
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
            return RedirectToAction("IsIlanlarim");
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IlanBasvuru(int ilanId)
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

            var ogrenci = db.Ogrenciler.FirstOrDefault(x => x.OgrenciID == nextOgrenciID);
            var ilan = db.IsIlanalari.FirstOrDefault(x => x.IsIlaniID == ilanId);

            if (ogrenci != null && ilan != null)
            {

                bool zatenFavoride = db.Basvurular.Any(b => b.Ogrenciler.OgrenciID == nextOgrenciID && b.IsIlanlari.IsIlaniID == ilanId);

                if (!zatenFavoride)
                {
                    var basvuru = new Basvurular
                    {
                        Ogrenciler = ogrenci,
                        IsIlanlari = ilan,
                        IlanDurumu = 0
                    };

                    db.Basvurular.Add(basvuru);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("OgrenciBasvurular", "Ilanlar");
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult OgrenciBasvurular()
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

            var basvurular = db.Basvurular
                .Where(x => x.Ogrenciler.OgrenciID == nextOgrenciID)
                .Include(x => x.IsIlanlari)
                .ToList();

            return View(basvurular);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult IsverenBasvurular()
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

            var basvurular = db.Basvurular
                .Where(x => x.IsIlanlari.Isverenler.IsverenID == nextIsverenID)
                .Include(x => x.Ogrenciler)
                .Include(x => x.IsIlanlari)
                .ToList();

            return View(basvurular);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpPost]
        public ActionResult BasvuruDurumuGuncelle(int basvuruId, int yeniDurum)
        {
            var basvuru = db.Basvurular.FirstOrDefault(x => x.BasvuruID == basvuruId);

            if (basvuru != null)
            {
                basvuru.IlanDurumu = yeniDurum;
                db.SaveChanges();
            }

            return RedirectToAction("IsverenBasvurular");
        }
    }
}
