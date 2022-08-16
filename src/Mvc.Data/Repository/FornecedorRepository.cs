using Microsoft.EntityFrameworkCore;
using Mvc.Business.Interfaces;
using Mvc.Business.Models;
using Mvc.Data.Context;

namespace Mvc.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppMvcContext context) : base(context) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await _context.Fornecedores
                .AsNoTracking()
                .Include(fornecedor => fornecedor.Endereco)
                .FirstOrDefaultAsync(fornecedor => fornecedor.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await _context.Fornecedores
               .AsNoTracking()
               .Include(fornecedor => fornecedor.Produtos)
               .Include(fornecedor => fornecedor.Endereco)
               .FirstOrDefaultAsync(fornecedor => fornecedor.Id == id);
        }
    }
}