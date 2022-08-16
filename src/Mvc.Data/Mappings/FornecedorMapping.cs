﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mvc.Business.Models;

namespace Mvc.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(lbda => lbda.Id);

            builder.Property(lbda => lbda.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(lbda => lbda.Documento)
               .IsRequired()
               .HasColumnType("varchar(14)");

            //1 fornecedor para 1 endereco
            builder.HasOne(fornecedor => fornecedor.Endereco)
                .WithOne(endereco => endereco.Fornecedor);

            //1 fornecedor para N produtos
            builder.HasMany(fornecedor => fornecedor.Produtos)
                .WithOne(produto => produto.Fornecedor)
                .HasForeignKey(produto => produto.FornecedorId);//Chave estrangeira

            builder.ToTable("Fornecedores");
        }
    }
}
