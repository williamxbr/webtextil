using WEBTextil.Dominio.Entidades;
using WEBTextil.Web.Helpers;
using WEBTextil.Web.Role;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace WEBTextil.Web.Role
{
    public static class GerenciadorUsuario
    {

        public static int LogID { get; set; }

        public static Identidade Identidade
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    try
                    {
                        // The user is authenticated. Return the user from the forms auth ticket.
                        return ((Identidade)(HttpContext.Current.User));
                    }
                    catch (Exception)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        var serializer = new JavaScriptSerializer();
                        return serializer.Deserialize<Identidade>(id.Ticket.UserData);
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public static void Autenticar(Usuario usuario, HttpResponseBase response)
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = usuario.UsuarioId;
            serializeModel.Nome = usuario.Login;
            serializeModel.NomeCompleto = usuario.Nome;
            serializeModel.Imagem = string.Empty;

            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                     usuario.Login,
                     DateTime.Now,
                     DateTime.Now.AddDays(30),
                     true,
                     userData,
                     FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

        public static void Logoff(HttpSessionStateBase session, HttpResponseBase response)
        {
            // Delete the user details from cache.
            session.Abandon();

            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();

            // Clear authentication cookie.
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            response.Cookies.Add(cookie);
        }
    }
}