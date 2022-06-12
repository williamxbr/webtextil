using WEBTextil.Dominio.Entidades;
using WEBTextil.Dominio.Interfaces.Respositorios;
using System.Linq;

namespace WEBTextil.Data.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public Usuario BuscarUsuario(string username, string password)
        {
            var teste = Select<Usuario>(new { Login = username, Senha = password });
            return teste.SingleOrDefault();
        }

        public Usuario BuscaUsuarioPorNome(string username)
        {
            return Select<Usuario>(new { Login = username }).SingleOrDefault();
        }

        public void RemoveUsuario(int id)
        {
            try
            {
                var usuario = Find<Usuario>(id);
                Delete(usuario);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
