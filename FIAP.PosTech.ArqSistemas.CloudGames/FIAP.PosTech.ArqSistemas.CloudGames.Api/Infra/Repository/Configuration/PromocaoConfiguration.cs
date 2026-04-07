using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository.Configuration
{
    public class PromocaoConfiguration : IEntityTypeConfiguration<Promocao>
    {
        public void Configure(EntityTypeBuilder<Promocao> builder)
        {
            builder.ToTable("Promocao");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Descricao).HasColumnType("VARCHAR(200)").IsRequired();
            builder.Property(p => p.Ativa).HasColumnType("BIT").IsRequired();
        }
    }
}
