using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Sertifikalar")]
    public class Sertifika
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SertifikaID { get; set; }
        [StringLength(120), Required]
        public string SertifikaAdi { get; set; }

        [StringLength(120), Required]
        public string SertifikaAciklamasi { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }

    }
}