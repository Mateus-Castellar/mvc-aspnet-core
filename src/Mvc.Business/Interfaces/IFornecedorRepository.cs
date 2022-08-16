using Mvc.Business.Models;

namespace Mvc.Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Fornecedor fornecedor);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Fornecedor fornecedor);
    }
}