using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Yetenekler")]
    public class Yetenekler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YetenekID { get; set; }
        [StringLength(120), Required]
        public string YetenekAdi { get; set; }

        [StringLength(120), Required]
        public string Yetenekİcerigi { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
    }
}