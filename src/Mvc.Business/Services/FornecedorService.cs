using Mvc.Business.Interfaces;
using Mvc.Business.Models;
using Mvc.Business.Validations;

namespace Mvc.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IEnderecoRepository enderecoRepository,
            INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            //validar estado do fornecedor e do seu endereco
            if (ExecutarValidacao(new FornecedorValidation(), fornecedor) is false &&
                ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco) is false)
            {
                return;
            }

            if (_fornecedorRepository.Buscar(lbda =>
                lbda.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor cadastrado com este documento");
                return;
            }

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            //validar estado do fornecedor e do seu endereco
            if (ExecutarValidacao(new FornecedorValidation(), fornecedor) is false)
            {
                return;
            }

            if (_fornecedorRepository.Buscar(lbda =>
                lbda.Documento == fornecedor.Documento && lbda.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor cadastrado com este documento");
                return;
            }

            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (ExecutarValidacao(new EnderecoValidation(), endereco) is false)
            {
                return;
            }

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notificar("O Fornecedor possui produtos cadastrados");
                return;
            }

            await _fornecedorRepository.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}