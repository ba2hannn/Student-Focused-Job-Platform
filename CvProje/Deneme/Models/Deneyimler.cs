using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Deneyimler")]
    public class Deneyimler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeneyimID { get; set; }
        [StringLength(120), Required]
        public string DeneyimAdi { get; set; }

        [StringLength(120), Required]
        public string Deneyimİcerigi { get; set; }

        [StringLength(5000), Required]
        public string DeneyimDetayi { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
    }
}