using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contasapp.Presentation.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("CATEGORIA");
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Id)
                .HasColumnName("ID");
            
            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .HasColumnName("TIPO")
                .IsRequired();

            builder.Property(c => c.UsuarioId)
                .HasColumnName("USUARIO_ID")
                .IsRequired();

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Categorias)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
