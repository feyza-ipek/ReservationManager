using Microsoft.EntityFrameworkCore;
using ReservationManager.Domain.Entities;

namespace ReservationManager.Persistence.DataContext
{
    public class ReservationManagerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ReservationManagerDb");
        }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
