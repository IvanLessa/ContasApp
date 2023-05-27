using ContasApp.Data.Contexts;
using ContasApp.Data.Entities;

namespace ContasApp.Data.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>
    {
        public List<Categoria>? GetByUsuario(Guid? usuarioId)
        {
            using (var context = new DataContext())
            {
                return context.Categoria?
                    .Where(c => c.UsuarioId == usuarioId)
                    .OrderBy(c => c.Nome)
                    .ToList();
            }
        }
    }
}

