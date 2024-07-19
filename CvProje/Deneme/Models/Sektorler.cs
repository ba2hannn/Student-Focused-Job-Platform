using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    public class Sektorler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SektorID { get; set; }
        [StringLength(50), Required]
        public string SektorAdi { get; set; }

    }
}