using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.App.Extensions;
using Mvc.App.ViewModels;
using Mvc.Business.Interfaces;
using Mvc.Business.Models;

namespace Mvc.App.Controllers
{
    [Authorize]
    [Route("Produtos")]
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository,
            IMapper mapper, IProdutoService produtoService, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _produtoService = produtoService;
        }

        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await
                _produtoRepository.ObterProdutosFornecedores()));
        }

        [AllowAnonymous]
        [Route("detalhes-do-produtos/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null) return NotFound();

            return View(produto);
        }

        [ClaimsAuthorize("Administrador", "Adicionar")]
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            return View(await PopularFornecedores(new ProdutoViewModel()));
        }

        [ClaimsAuthorize("Administrador", "Adicionar")]
        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedores(produtoViewModel);

            if (ModelState.IsValid is false) return View(produtoViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";

            if (await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo) is false)
                return View(produtoViewModel);

            //passa o valor para o campo que é persistido na base de dados
            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            if (OperacaoValida() is false) return View(produtoViewModel);

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Administrador", "Editar")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null) return NotFound();

            return View(produto);
        }

        [ClaimsAuthorize("Administrador", "Editar")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizado = await ObterProduto(id);

            produtoViewModel.Fornecedor = produtoAtualizado.Fornecedor;
            produtoViewModel.Imagem = produtoAtualizado.Imagem;

            if (ModelState.IsValid is false) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload is not null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo) is false)
                    return View(produtoViewModel);

                produtoAtualizado.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizado.Nome = produtoViewModel.Nome;
            produtoAtualizado.Descricao = produtoViewModel.Descricao;
            produtoAtualizado.Valor = produtoViewModel.Valor;
            produtoAtualizado.Ativo = produtoViewModel.Ativo;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizado));

            if (OperacaoValida() is false) return View(produtoViewModel);

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Administrador", "Excluir")]
        [Route("deletar-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null) return NotFound();

            return View(produto);
        }

        [ClaimsAuthorize("Administrador", "Excluir")]
        [Route("deletar-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null) return NotFound();

            await _produtoService.Remover(id);

            if (OperacaoValida() is false) return View(produto);

            TempData["Sucesso"] = "Produto removido com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        #region Metodos Auxialiares

        public async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await
                _produtoRepository.ObterProdutoFornecedor(id));

            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await
                _fornecedorRepository.ObterTodos());

            return produto;
        }

        public async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await
                _fornecedorRepository.ObterTodos());

            return produto;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            //caminho para salvar arquivo
            var path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já possui uma imagem com este nome");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

        #endregion
    }
}