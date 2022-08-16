using Microsoft.EntityFrameworkCore;
using Mvc.Business.Interfaces;
using Mvc.Business.Models;
using Mvc.Data.Context;

namespace Mvc.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppMvcContext context) : base(context) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await _context.Produtos
                .AsNoTracking()
                .Include(produto => produto.Fornecedor)
                .FirstOrDefaultAsync(produto => produto.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await _context.Produtos
                .AsNoTracking()
                .Include(produto => produto.Fornecedor)
                .OrderBy(produto => produto.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(produto => produto.FornecedorId == fornecedorId);
        }
    }
}