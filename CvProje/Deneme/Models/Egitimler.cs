using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Egitimler")]
    public class Egitimler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EgitimID { get; set; }
        [Required]
        public int Sinif { get; set; }
        [Required]
        public float GNO { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }

        public virtual Universiteler Universiteler { get; set; }

        public virtual Bolum Bolum { get; set; }

    }
}