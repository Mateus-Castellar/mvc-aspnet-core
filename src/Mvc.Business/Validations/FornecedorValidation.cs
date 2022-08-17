using FluentValidation;
using Mvc.Business.Models;
using Mvc.Business.Validations.Documentos;

namespace Mvc.Business.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(fornecedor => fornecedor.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(fornecedor => fornecedor.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(fornecedor => fornecedor.Documento.Length)
                    .Equal(CpfValidacao.TamanhoCpf).WithMessage("O campo Documento precisa ter {ComparsionValue} caracteres e foi informado {PropertyValue}");

                RuleFor(fornecedor => CpfValidacao.Validar(fornecedor.Documento))
                .Equal(true).WithMessage("O Documento fornecido é inválido");
            });

            When(fornecedor => fornecedor.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(fornecedor => fornecedor.Documento.Length)
                    .Equal(CnpjValidacao.TamanhoCnpj).WithMessage("O campo Documento precisa ter {ComparsionValue} caracteres e foi informado {PropertyValue}");

                RuleFor(fornecedor => CnpjValidacao.Validar(fornecedor.Documento))
                .Equal(true).WithMessage("O Documento fornecido é inválido");
            });
        }
    }
}