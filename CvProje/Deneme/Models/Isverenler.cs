using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Isverenler")]
    public class Isverenler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IsverenID { get; set; }
        [StringLength(100), Required]
        public string SirketAdi { get; set; }

        [StringLength(100), Required]
        public string SirketMail { get; set; }

        [StringLength(11), Required]
        public string telefon { get; set; }

        [StringLength(350), Required]
        public string SirketAdres { get; set; }

        [Required]
        public string Sifre { get; set; }
    }
}