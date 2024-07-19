using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Deneme.Models
{
    [Table("FavoriIlanlar")]
    public class FavoriIlanlar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavoriID { get; set; }

        public virtual Ogrenciler Ogrenciler { get; set; }
        public virtual IsIlanlari IsIlanlari { get; set; }


    }
}