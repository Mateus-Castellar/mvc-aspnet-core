using Microsoft.EntityFrameworkCore;
using Mvc.Business.Models;

namespace Mvc.Data.Context
{
    public class AppMvcContext : DbContext
    {
        public AppMvcContext(DbContextOptions<AppMvcContext> context) : base(context) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppMvcContext).Assembly);

            //desabilitar cascade delete
            foreach (var relacionamento in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(lbda => lbda.GetForeignKeys()))
            {
                relacionamento.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
