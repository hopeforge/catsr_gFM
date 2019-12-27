using cpj.Entities;
using Microsoft.EntityFrameworkCore;

namespace cpj.DAO.Context
{
    public class CPJContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Conta> Conta { get; set; }
        public DbSet<Portal> Portal { get; set; }
        public DbSet<Peticao> Peticao { get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }
        public DbSet<Processo> Processo { get; set; }
        public CPJContext(DbContextOptions<CPJContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>()
            .HasOne(p => p.Portal)
            .WithMany(b => b.Contas)
            .IsRequired();
        }
       

    }
}
