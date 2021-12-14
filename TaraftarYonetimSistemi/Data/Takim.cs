using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaraftarYonetimSistemi.Data
{
    [Table("Takimlar")]
    public class Takim
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Ad { get; set; }
        public virtual ICollection<Kisi> Taraftarlar { get; set; } //birden çok taraftar(collection)
    }
}
