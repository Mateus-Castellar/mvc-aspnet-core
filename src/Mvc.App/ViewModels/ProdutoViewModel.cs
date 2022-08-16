using Mvc.App.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Mvc.App.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Descricao { get; set; }

        [Display(Name = "Imagem Do Produto")]
        public IFormFile ImagemUpload { get; set; }

        public string Imagem { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [Display(Name = "Data De Cadastro")]
        [ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        [Display(Name = "Ativo ?")]
        public bool Ativo { get; set; }

        public FornecedorViewModel Fornecedor { get; set; }

        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public Guid FornecedorId { get; set; }
    }
}