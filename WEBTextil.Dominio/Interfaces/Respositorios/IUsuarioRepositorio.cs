using WEBTextil.Dominio.Entidades;

namespace WEBTextil.Dominio.Interfaces.Respositorios
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        Usuario BuscarUsuario(string username, string password);
        Usuario BuscaUsuarioPorNome(string username);
        void RemoveUsuario(int id);
    }
}
