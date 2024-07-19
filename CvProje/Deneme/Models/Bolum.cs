using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Bolumler")]
    public class Bolum
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BolumID { get; set; }
        [StringLength(120), Required]
        public string BolumAdi { get; set; }

        public virtual Universiteler Universite { get; set; }
    }
}