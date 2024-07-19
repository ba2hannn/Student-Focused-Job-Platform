using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Iletisim")]
    public class Iletisim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IletisimID { get; set; }

        [StringLength(5000)]
        public string OgrenciMesaj { get; set; }

        [StringLength(5000)]
        public string IsverenMesaj { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
        public virtual IsIlanlari IsIlanlari { get; set; }
    }
}