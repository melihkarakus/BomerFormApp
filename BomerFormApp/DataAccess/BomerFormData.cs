using BomerFormApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BomerFormApp.DataAccess
{
    public class BomerFormData : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-VLOIQ6M3\\SQLEXPRESS;initial Catalog=BomerDb; integrated security=true;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<User> Users { get; set; }
    }
}
