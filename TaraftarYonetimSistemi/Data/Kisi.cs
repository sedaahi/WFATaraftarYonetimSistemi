using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaraftarYonetimSistemi.Data
{
    [Table("Kisiler")]
    public class Kisi
    {

        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Ad { get; set; }
        public int? TakimId { get; set; } //foreign key TakimId yaptık(classın ismi+id gelenek) class aşağıda Takim takim dediğimiz class ve ?int=> nullable yaptık
        public virtual Takim Takim { get; set; } //foreign key  için Takim dedik ve yukarıda TakimId olmalu

    }
}
