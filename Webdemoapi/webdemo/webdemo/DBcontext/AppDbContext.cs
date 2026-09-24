using Microsoft.EntityFrameworkCore;
using webdemo.Models;

namespace webdemo.DBcontext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
        public DbSet<Hanghoa> Hanghoas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hanghoa>().HasData(
                new Hanghoa { MaHangHoa = "HH001", TenHangHoa = "Sữa tươi Vinamilk", DonGia = 32000, SoLuong = 100  },
                new Hanghoa { MaHangHoa = "HH002", TenHangHoa = "Bánh Chocopie", DonGia = 55000, SoLuong = 50 },
                new Hanghoa { MaHangHoa = "HH003", TenHangHoa = "Mì Tôm Hảo Hảo", DonGia = 4500, SoLuong = 500 }
                );
        }
    }
}
