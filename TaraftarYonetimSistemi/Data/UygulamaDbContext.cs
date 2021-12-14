using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaraftarYonetimSistemi.Data
{
    public class UygulamaDbContext: DbContext
    {
        public UygulamaDbContext():base("name=BaglantiCumlem")
        {

        }
        public DbSet<Takim> Takimlar { get; set; }
        public DbSet<Kisi> Kisiler { get; set; }
    }
}
