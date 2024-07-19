using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("IlgiAlanlari")]
    public class IlgiAlanlari
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IlgiID { get; set; }
        [StringLength(50), Required]
        public string IlgiAdi { get; set; }

        [StringLength(500), Required]
        public string IlgiAciklamasi { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
    }
}