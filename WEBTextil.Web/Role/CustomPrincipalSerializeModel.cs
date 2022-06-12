using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBTextil.Web.Role
{
    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        public string Imagem { get; set; }
    }
}