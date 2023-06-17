using Microsoft.EntityFrameworkCore;

namespace CS_Ogretici.Models
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-8FCT5ET; database=CS_Ogretici_VeriTabani; user=sa; password=elma5113; integrated security=true; TrustServerCertificate=True");
        }
        public DbSet<Basliklar> Basliklars { get; set; }
        public DbSet<Kullanicilar> Kullanicilars { get; set; }

        public DbSet<Yorumlar> Yorumlars { get; set; }

    }
}
