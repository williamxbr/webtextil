using WEBTextil.Dominio.Interfaces.Respositorios;
using WEBTextil.Data.Repositorios;
using System.Security.Principal;
using Unity;

namespace WEBTextil.Web.Role
{
    public class Identidade : ICustomPrincipal
    {

        public Identidade(string name)
        {
            Identity = new GenericIdentity(name);

            var container = new UnityContainer();
            container.RegisterType<IUsuarioRepositorio, UsuarioRepositorio>();
            IUsuarioRepositorio UsuarioRepositorio = container.Resolve<IUsuarioRepositorio>();

            var usuario = UsuarioRepositorio.BuscaUsuarioPorNome(name);
            Imagem = usuario.Imagem;
            Permissao = usuario.Permissao;
            Admin = usuario.Admin;
            Ativo = usuario.Ativo;
            Id = usuario.UsuarioId;

        }

        public IIdentity Identity
        {
            get;
            private set;
        }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        public string Imagem { get; set; }
        public int Id { get; set; }
        public string Permissao { get; set; }
        public bool Ativo { get; set; }
        public bool Admin { get; set; }

        public bool IsInRole(string role)
        {
            if (Admin) return true;
            if (!Ativo) return false;

            return Permissao == null ? false : Permissao.IndexOf(role) >= 0;
        }
    }
}