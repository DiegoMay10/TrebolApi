using Microsoft.EntityFrameworkCore;

namespace TrebolAcademy.Models
{
    public class TrebolAcademyContext:DbContext
    {
        public TrebolAcademyContext(DbContextOptions<TrebolAcademyContext> options) : base(options) { }
        public DbSet<Grimorio> Grimory { get; set; } = null!;
        public DbSet<Solicitud> Solicitude { get; set; } = null!;
    }
}
