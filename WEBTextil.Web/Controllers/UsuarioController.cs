using WEBTextil.Dominio.Entidades;
using WEBTextil.Dominio.Interfaces.Aplicacao;
using WEBTextil.Web.Helpers;
using WEBTextil.Web.Role;
using WEBTextil.Web.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBTextil.Web.Controllers
{
    public class UsuarioController : Controller
    {
        readonly IUsuarioAplicacao UsuarioAplicacao;

        public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
        {
            UsuarioAplicacao = usuarioAplicacao;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [Filtro(Roles = "Admin")]
        public ActionResult Criar()
        {
            return View();
        }

        [Filtro(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Senha = Sha1.Encode(usuario.Senha);
                UsuarioAplicacao.Adicionar(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [Filtro(Roles = "Admin")]
        public ActionResult Editar(int id)
        {
            var usuario = UsuarioAplicacao.BuscarId(id);
            return View(usuario);
        }

        [Filtro(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Senha = Sha1.Encode(usuario.Senha);
                UsuarioAplicacao.Atualizar(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [Filtro(Roles = "Admin")]
        public JsonResult DeletarUsuario(int id)
        {
            bool sucesso;
            string mensagem;
            try
            {
                UsuarioAplicacao.RemoveUsuario(id);
                sucesso = true;
                mensagem = "USUARIO DELETADO COM SUCESSO!";
            }
            catch (Exception e)
            {
                sucesso = false;
                mensagem = e.Message;
            }

            return Json(new { sucesso, mensagem }, JsonRequestBehavior.AllowGet);

        }

        [Filtro(Roles = "Admin")]
        public JsonResult AtivarUsuario(int id, bool ativa)
        {
            try
            {
                var usuario = UsuarioAplicacao.BuscarId(id);
                usuario.Ativo = ativa;
                UsuarioAplicacao.Atualizar(usuario);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.DenyGet);
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(UsuarioViewModel usuario)
        {
            try
            {
                string SenhaSHA1 = Sha1.Encode(usuario.Senha);
                Usuario user = UsuarioAplicacao.BuscarUsuario(usuario.Login, SenhaSHA1);

                if (user == null) throw new Exception($"Usuario {usuario.Login} não encontrado! Por favor verifique o nome ou a senha!");

                if (!user.Ativo) throw new Exception($"Usuario {usuario.Login} desativado! Por favor entrar em contato com o administrador do sistema");

                GerenciadorUsuario.Autenticar(user, Response);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                GerenciadorUsuario.Logoff(Session, Response);

                ModelState.AddModelError("", ex.Message);
                return View(usuario);

                throw;
            }
        }

        public ActionResult Logout()
        {
            GerenciadorUsuario.Logoff(Session, Response);
            return RedirectToAction("Login", "Usuario");
        }

        public ActionResult AcessoNegado()
        {
            return View();
        }

        [Filtro(Roles = "Admin")]
        public ActionResult Lista()
        {
            return View();
        }

        [Filtro(Roles = "Admin")]
        public JsonResult ListarUsuario()
        {
            return Json(UsuarioAplicacao.BuscarTodos(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public JsonResult BuscarUsuario(int id)
        {
            var retorno = UsuarioAplicacao.BuscarId(id);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Perfil(HttpPostedFileBase foto)
        {
            if (foto != null && foto.ContentLength > 0)
            {
                string directory = @"D:\Temp\";

                if (foto.ContentLength > 10240)
                {
                    ModelState.AddModelError("photo", "The size of the file should not exceed 10 KB");
                    return View();
                }

                var supportedTypes = new[] { "jpg", "jpeg", "png" };

                var fileExt = System.IO.Path.GetExtension(foto.FileName).Substring(1);

                if (!supportedTypes.Contains(fileExt))
                {
                    ModelState.AddModelError("photo", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");
                    return View();
                }

                var fileName = Path.GetFileName(foto.FileName);
                foto.SaveAs(Path.Combine(directory, fileName));
            }

            return RedirectToAction("Index");
        }

        public JsonResult GravarUsuario(Usuario usuario)
        {
            try
            {
                if (usuario.UsuarioId > 0)
                {
                    UsuarioAplicacao.Atualizar(usuario);
                }
                else
                {
                    UsuarioAplicacao.Adicionar(usuario);
                }

                return Json(new { sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}