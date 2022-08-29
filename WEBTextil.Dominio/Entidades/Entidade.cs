namespace WEBTextil.Dominio.Entidades
{
    public abstract class Entidade
    {
        protected Entidade()
        {
            Id = Guid.NewGuid();

        }

        public Guid Id { get; set; }
    }

}