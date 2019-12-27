namespace cpj.Entities
{
    /// <summary>
    /// Conta cadastrada em portais da justiça brasileira.
    /// </summary>
    public class Conta
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int PortalId { get; set; }
        public Portal Portal { get; set; }

    }
}
