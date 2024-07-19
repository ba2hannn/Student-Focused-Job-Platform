using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Deneme.Models.Managers
{
    public class VeritabaniContext : DbContext
    {
        public DbSet<Universiteler> Universiteler { get; set; }

        public DbSet<Bolum> Bolumler { get; set; }

        public DbSet<Ogrenciler> Ogrenciler { get; set; }

        public DbSet<Isverenler> Isverenler { get; set; }

        public DbSet<IsIlanlari> IsIlanalari { get; set; }

        public DbSet<Iletisim> Iletisim { get; set; }

        public DbSet<FavoriIlanlar> FavoriIlanlar { get; set; }

        public DbSet<DersIcerigi> Icerigler { get; set; }

        public DbSet<Sektorler> Sektorler { get; set; }

        public DbSet<Departmanlar> Departmanlar { get; set; }

        public DbSet<Egitimler> Egitimler { get; set; }

        public DbSet<AdminPaneli> AdminPaneli { get; set; }

        public DbSet<Deneyimler> Deneyimler { get; set; }

        public DbSet<Sertifika> Sertifikalar { get; set; }

        public DbSet<IlgiAlanlari> IlgiAlanlari { get; set; }

        public DbSet<Yetenekler> Yetenekler { get; set; }

        public DbSet<Basvurular> Basvurular { get; set; }

        public DbSet<Destek> Destek { get; set; }






        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new VeritabaniInitializer());
        }

        public class VeritabaniInitializer : CreateDatabaseIfNotExists<VeritabaniContext>
        {
            protected override void Seed(VeritabaniContext context)
            {
                List<Universiteler> universiteler = new List<Universiteler>
                {
                    new Universiteler { UniversiteAdi = "Bartın Üniversitesi"},
                    new Universiteler { UniversiteAdi = "Kocaeli Üniversitesi"}
                };

                foreach (var universite in universiteler)
                {
                    context.Universiteler.Add(universite);
                    context.SaveChanges();
                }

                List<Bolum> bolumler = new List<Bolum>
                {
                    new Bolum { BolumAdi = "Bilgisayar Teknolojisi ve Bilişim Sistemleri", Universite = universiteler[0] },
                    new Bolum { BolumAdi = "Yönetim Bilişim Sistemleri", Universite = universiteler[1] },
                    new Bolum { BolumAdi = "Hemşirelik", Universite = universiteler[1] },
                     new Bolum { BolumAdi = "İnşaat Mühendisliği", Universite = universiteler[1] }
                };

                foreach (var bolum in bolumler)
                {
                    context.Bolumler.Add(bolum);
                    context.SaveChanges();
                }

                List<Sektorler> sektorler = new List<Sektorler>
                {
                    new Sektorler { SektorAdi = "Bilgi Teknolojileri" },
                    new Sektorler { SektorAdi = "Medya ve Tasarım" },
                    new Sektorler { SektorAdi = "Sağlık" },
                    new Sektorler { SektorAdi = "İnşaat ve Gayrimenkul" }
                };

                foreach (var sektor in sektorler)
                {
                    context.Sektorler.Add(sektor);
                    context.SaveChanges();
                }

                List<DersIcerigi> dersIcerikleri = new List<DersIcerigi>
                {
                    new DersIcerigi { IcerigAdi = "Algoritmalara Giriş", Bolum = bolumler[0], Sinif = 1, Sektorler = sektorler[0] },
                    new DersIcerigi { IcerigAdi = "Çizgi Film Tasarımı", Bolum = bolumler[0], Sinif = 1, Sektorler = sektorler[1] },
                    new DersIcerigi { IcerigAdi = "Temel Bilgi Teknolojileri ve Yönetimi", Bolum = bolumler[1], Sinif = 1, Sektorler = sektorler[0] },
                    new DersIcerigi { IcerigAdi = "Veritabanı Yönetimi ve Uygulamaları", Bolum = bolumler[1], Sinif = 2, Sektorler = sektorler[0] },
                    new DersIcerigi { IcerigAdi = "İş Güvenliği", Bolum = bolumler[2], Sinif = 2, Sektorler = sektorler[2] },
                    new DersIcerigi { IcerigAdi = "Hemşirelikte Farmakoloji ve İlaç Yönetimi", Bolum = bolumler[2], Sinif = 3, Sektorler = sektorler[2] },
                    new DersIcerigi { IcerigAdi = "Yapı Malzemeleri ve Teknolojisi", Bolum = bolumler[3], Sinif = 2, Sektorler = sektorler[3] },
                    new DersIcerigi { IcerigAdi = "İnşaat Yönetimi ve Proje Planlama", Bolum = bolumler[3], Sinif = 3, Sektorler = sektorler[3] }
                };

                foreach (var dersIcerigi in dersIcerikleri)
                {
                    context.Icerigler.Add(dersIcerigi);
                    context.SaveChanges();
                }


                List<Departmanlar> departmanlar = new List<Departmanlar>
                {
                    new Departmanlar { DepartmanAdi = "Yazılım Geliştirme" },
                    new Departmanlar { DepartmanAdi = "Ağ ve Güvenlik" },
                    new Departmanlar { DepartmanAdi = "Grafik Tasarım" },
                    new Departmanlar { DepartmanAdi = "Dijital Medya ve İletişim" },
                    new Departmanlar { DepartmanAdi = "Hemşirelik ve Bakım Hizmetleri" },
                    new Departmanlar { DepartmanAdi = "Tıbbi Görüntüleme" },
                    new Departmanlar { DepartmanAdi = "İnşaat Yönetimi" }
                };

                foreach (var departman in departmanlar)
                {
                    context.Departmanlar.Add(departman);
                    context.SaveChanges();
                }

                List<AdminPaneli> adminPaneli = new List<AdminPaneli>
                {
                    new AdminPaneli {AdminMail= "admin@gmail.com", AdminSifre= "admin"}
                };

                foreach (var AdminPaneli in adminPaneli)
                {
                    context.AdminPaneli.Add(AdminPaneli);
                    context.SaveChanges();
                }
            }
        }
    }
}