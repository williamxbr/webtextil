using WEBTextil.Dominio.Entidades;

namespace WEBTextil.Dominio.Interfaces.Aplicacao
{
    public interface IUsuarioAplicacao : IAplicacaoBase<Usuario>
    {
        void RemoveUsuario(int id);
        Usuario BuscarUsuario(string username, string password);
        Usuario BuscaUsuarioPorNome(string username);
    }
}
