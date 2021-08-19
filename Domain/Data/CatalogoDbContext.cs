using Microsoft.EntityFrameworkCore;
using DesafioGlobaltec.Domain.Models;

namespace DesafioGlobaltec.Domain.Data {
    public class CatalogoDbContext : DbContext {
        public CatalogoDbContext(
            DbContextOptions<CatalogoDbContext> options) : base(options) { 
                //
            }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Pessoa>().HasKey(p => p.CodigoPessoa);
        }
    }
}