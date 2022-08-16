using Microsoft.EntityFrameworkCore;
using Mvc.Business.Models;

namespace Mvc.Data.Context
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> context) : base(context) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}
