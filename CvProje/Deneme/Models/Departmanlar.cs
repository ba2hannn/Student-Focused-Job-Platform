using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("Departmanlar")]
    public class Departmanlar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmanID { get; set; }
        [StringLength(50), Required]
        public string DepartmanAdi { get; set; }
    }
}