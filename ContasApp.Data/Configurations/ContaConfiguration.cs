using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contasapp.Presentation.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("CONTA");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID");

            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Data)
                .HasColumnName("DATA")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.Valor)
                .HasColumnName("VALOR")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Observacoes)
                .HasColumnName("OBSERVACOES")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.UsuarioId)
                .HasColumnName("USUARIO_ID")
                .IsRequired();

            builder.HasOne(c => c.Categoria)
                .WithMany(ca => ca.Contas)
                .HasForeignKey(c => c.CategoriaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Usuario)
               .WithMany(u => u.Contas)
               .HasForeignKey(c => c.UsuarioId)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
