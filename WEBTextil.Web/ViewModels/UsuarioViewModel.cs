using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WEBTextil.Web.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int UsuarioId { get; set; }
        [MaxLength(10, ErrorMessage = "Login não pode ter mais que 10 caracteres")]
        [Required(ErrorMessage = "Preencha o campo Login")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        [DisplayName("Login")]
        public string Login { get; set; }
        [MaxLength(10, ErrorMessage = "Senha não pode ter mais que 10 caracteres")]
        [Required(ErrorMessage = "Preencha o campo Senha")]
        [MinLength(2, ErrorMessage = "Minimo {0} caracteres")]
        [DisplayName("Senha")]
        public string Senha { get; set; }
        [MaxLength(30, ErrorMessage = "Nome do usuário não pode ter mais que 30 caracteres")]
        [Required(ErrorMessage = "Preencha o campo Nome")]
        public string Nome { get; set; }
    }
}