using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Universiteler")]
    public class Universiteler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UniversiteID { get; set; }
        [StringLength(100), Required]
        public string UniversiteAdi { get; set; }
    }
}
