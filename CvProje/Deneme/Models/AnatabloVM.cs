using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class AnatabloVM
    {
        public List<Bolum> Bolumler { get; set; }
        public List<Universiteler> Universiteler { get; set; }
        public List<Ogrenciler> Ogrenciler { get; set; }
        public List<Isverenler> Isverenler { get; set; }
        public List<IsIlanlari> IsIlanlari { get; set; }
        public List<Iletisim> Iletisim { get; set; }
        public List<FavoriIlanlar> FavoriIlanlar { get; set; }
        public List<Sektorler> Sektorler { get; set; }
        public List<DersIcerigi> Icerigler { get; set; }
        public List<Departmanlar> Departmanlar { get; set; }
        public List<AdminPaneli> AdminPaneli { get; set; }
        public List<Egitimler> Egitimler { get; set; }
        public List<Deneyimler> Deneyimler { get; set; }
        public List<Sertifika> Sertifikalar { get; set; }
        public List<IlgiAlanlari> ilgiAlanlari { get; set; }
        public List<Yetenekler> Yetenekler { get; set; }
        public List<Basvurular> Basvurular { get; set; }
        public List<Destek> Destek { get; set; }





    }
}