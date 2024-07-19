using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Basvurular")]
    public class Basvurular
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BasvuruID { get; set; }

        public int IlanDurumu { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
        public virtual IsIlanlari IsIlanlari { get; set; }

    }
}