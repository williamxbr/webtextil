using WEBTextil.Aplicacao;
using WEBTextil.Dominio.Entidades;
using WEBTextil.Dominio.Interfaces.Aplicacao;
using WEBTextil.Dominio.Interfaces.Respositorios;

namespace WEBTextil.Data.Repositorios
{
    public class UsuarioAplicacao : AplicacaoBase<Usuario>, IUsuarioAplicacao
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio) : base(usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public Usuario BuscarUsuario(string username, string password)
        {
            return _usuarioRepositorio.BuscarUsuario(username, password);
        }

        public Usuario BuscaUsuarioPorNome(string username)
        {
            return _usuarioRepositorio.BuscaUsuarioPorNome(username);
        }

        public void RemoveUsuario(int id)
        {
            _usuarioRepositorio.RemoveUsuario(id);
        }
    }
}
