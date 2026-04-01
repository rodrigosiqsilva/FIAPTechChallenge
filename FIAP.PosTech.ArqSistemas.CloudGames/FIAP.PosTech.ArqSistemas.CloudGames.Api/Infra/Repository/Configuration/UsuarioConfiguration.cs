using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("PessoaFisica");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
            builder.Property(p => p.Senha).HasColumnType("VARCHAR(15)").IsRequired();
            builder.Property(p => p.Email).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(200)").IsRequired();
            builder.Property(p => p.Administrador).HasColumnType("BIT").IsRequired();
        }
    }
}
