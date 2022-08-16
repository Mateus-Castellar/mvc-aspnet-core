using Microsoft.EntityFrameworkCore;
using Mvc.Business.Interfaces;
using Mvc.Business.Models;
using Mvc.Data.Context;

namespace Mvc.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AppMvcContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await _context.Enderecos
                .AsNoTracking()
                .FirstOrDefaultAsync(endereco => endereco.FornecedorId == fornecedorId);
        }
    }
}