using Mvc.Business.Interfaces;
using Mvc.Business.Models;
using Mvc.Business.Validations;

namespace Mvc.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (ExecutarValidacao(new FornecedorValidation(), fornecedor) is false)
                return;

            return;
        }

        public Task Atualizar(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
