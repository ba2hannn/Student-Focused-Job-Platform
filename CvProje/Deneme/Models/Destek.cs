using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Destek")]
    public class Destek
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DestekID { get; set; }

        [StringLength(5000)]
        public string OgrenciDestek { get; set; }

        [StringLength(5000)]
        public string IsverenDestek { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
        public virtual Isverenler Isverenler { get; set; }

    }
}