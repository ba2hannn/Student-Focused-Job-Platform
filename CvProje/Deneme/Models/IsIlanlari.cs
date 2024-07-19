using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("IsIlanları")]
    public class IsIlanlari
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IsIlaniID { get; set; }

        [StringLength(500), Required]
        public string IlanBaslik { get; set; }
        [StringLength(500), Required]
        public string Aciklama { get; set; }
        [StringLength(500), Required]
        public string Gereksinimler { get; set; }

        public virtual Isverenler Isverenler { get; set; }
            
        public virtual Sektorler Sektorler { get; set; }
        public virtual Departmanlar Departmanlar { get; set; }

    }
}