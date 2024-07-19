using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("DersIcerigi")]
    public class DersIcerigi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IcerigID { get; set; }
        [StringLength(50), Required]
        public string IcerigAdi { get; set; }

        [Required]
        public int Sinif { get; set; }

        public virtual Sektorler Sektorler { get; set; }

        public virtual Bolum Bolum { get; set; }



    }
}