namespace WEBTextil.Dominio.Entidades
{
    public class Usuario
	{
		public int UsuarioId { get; set; }
		public string Login { get; set; }
		public string Senha { get; set; }
		public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public bool Admin { get; set; }
        public string Permissao { get; set; }
        public string Imagem { get; set; }
    }
}
