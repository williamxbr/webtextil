using System.Web.Mvc;

namespace WEBTextil.Web.Role
{
    public class Filtro : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult && filterContext.HttpContext.Request.IsAuthenticated)
                filterContext.HttpContext.Response.Redirect("/Usuario/AcessoNegado");
        }

    }
}