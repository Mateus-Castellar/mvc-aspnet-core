﻿namespace Mvc.Business.Models
{
    public class Produto : Entity
    {
        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime Date { get; set; }
        public bool Ativo { get; set; }

        //EfCore Relacionamento
        public Fornecedor Fornecedor { get; set; }
    }
}
