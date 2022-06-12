using WEBTextil.Aplicacao;
using WEBTextil.Data.Repositorios;
using WEBTextil.Dominio.Interfaces.Aplicacao;
using WEBTextil.Dominio.Interfaces.Respositorios;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace WEBTextil.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType(typeof(IAplicacaoBase<>), typeof(AplicacaoBase<>));
            container.RegisterType(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

            container.RegisterType<IUsuarioAplicacao,UsuarioAplicacao>();
            container.RegisterType<IUsuarioRepositorio,UsuarioRepositorio>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}