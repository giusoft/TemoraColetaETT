using Microsoft.EntityFrameworkCore;
using TemoraColetaETT.Domain.Entities;

namespace TemoraColetaETT.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
