using System.Collections.Generic;

namespace cpj.Entities
{
    /// <summary>
    /// Classe do usuário do sistema
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<Conta> Contas { get; set; }
    }
}
