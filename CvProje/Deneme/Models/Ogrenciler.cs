using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Ogrenciler")]
    public class Ogrenciler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OgrenciID { get; set; }
        [StringLength(50), Required]
        public string OgrenciAdi { get; set; }

        [StringLength(50), Required]
        public string OgrenciSoyadi { get; set; }
        [StringLength(100), Required]
        public string OgrenciMail { get; set; }

        [StringLength(11), Required]
        public string TelefonNumarasi { get; set; }

        [StringLength(25), Required]
        public string Sifre { get; set; }

    }
}