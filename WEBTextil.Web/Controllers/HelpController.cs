using WEBTextil.Web.Helpers;
using WEBTextil.Web.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace WEBTextil.Web.Controllers
{
    [Filtro(Roles = "Admin")]
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Metodos()
        {

            var lista = new List<CodigoLinha>();

            foreach(var controle in BuscarTodosControllers())
            {
                lista.AddRange(CodeGenerationHelper.ListaMetodos(Type.GetType(controle.FullName)));
            }

            return View(lista);
        }

        public ActionResult UrlAjax()
        {

            var lista = new List<CodigoLinha>();

            foreach (var controle in BuscarTodosControllers())
            {
                lista.AddRange(CodeGenerationHelper.ListaUrlAjax(Type.GetType(controle.FullName)));
            }

            return View(lista);

        }

        public ActionResult Gera(string nomeTabela)
        {
            Type entidade = Type.GetType($"WEBTextil.Dominio.Entidades.{nomeTabela}, WEBTextil.Dominio");
            if (entidade != null)
            {
                var lista = CodeGenerationHelper.ListaDeletarJs(entidade);
                lista.AddRange(CodeGenerationHelper.ListaListagemJsGrid(entidade));
                lista.AddRange(CodeGenerationHelper.ListaEditarJs(entidade));
                lista.AddRange(CodeGenerationHelper.ListaGravarJs(entidade));
                lista.AddRange(CodeGenerationHelper.ListaController(entidade));
                lista.AddRange(CodeGenerationHelper.ListaViewLista(entidade));
                lista.AddRange(CodeGenerationHelper.ListaViewIndex(entidade));
                lista.AddRange(CodeGenerationHelper.ListaViewNovo(entidade));
                lista.AddRange(CodeGenerationHelper.ListaViewEditar(entidade));

                return View(lista);
            }

            return null;
        }


        private IEnumerable<Type> BuscarTodosControllers()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).ToList();
        }


    }
}