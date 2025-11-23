using Microsoft.EntityFrameworkCore;
using Mentorax.Api.Models;

namespace Mentorax.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Mentorado> Mentorados { get; set; }
        public DbSet<Mentor> Mentores { get; set; }
        public DbSet<Questionario> Questionarios { get; set; }
        public DbSet<PlanoMentoria> PlanosMentoria { get; set; }
        public DbSet<TarefaMentoria> TarefasMentoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // configure relationships or indexes here
        }
    }
}
