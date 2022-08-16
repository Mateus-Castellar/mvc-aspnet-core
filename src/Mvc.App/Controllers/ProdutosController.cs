using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mvc.App.ViewModels;
using Mvc.Business.Interfaces;
using Mvc.Business.Models;

namespace Mvc.App.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository,
            IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await
                _produtoRepository.ObterProdutosFornecedores()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null) return NotFound();

            return View(produto);
        }

        public async Task<IActionResult> Create()
        {
            return View(await PopularFornecedores(new ProdutoViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedores(produtoViewModel);

            if (ModelState.IsValid is false) return View(produtoViewModel);

            await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null) return NotFound();

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            if (ModelState.IsValid is false) return View(produtoViewModel);

            await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoViewModel));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null) return NotFound();

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto is null) return NotFound();

            await _produtoRepository.Remover(id);

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

        #endregion
    }
}
