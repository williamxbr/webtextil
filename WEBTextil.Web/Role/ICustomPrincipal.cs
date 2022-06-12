using System.Security.Principal;

namespace WEBTextil.Web.Role
{
    interface ICustomPrincipal : IPrincipal
    {
        int Id { get; set; }
        string Nome { get; set; }
        string NomeCompleto { get; set; }
        string Imagem { get; set; }
    }
}
