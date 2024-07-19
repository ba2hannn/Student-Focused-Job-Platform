using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("AdminPaneli")]
    public class AdminPaneli
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; }
        [StringLength(120), Required]
        public string AdminMail { get; set; }

        [StringLength(25), Required]
        public string AdminSifre { get; set; }
    }
}