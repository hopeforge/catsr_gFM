using cpj.DAO.Context;
using cpj.DAO.Repositories.Interfaces;
using cpj.Entities;
using System.Collections.Generic;
using System.Linq;

namespace cpj.DAO.Repositories
{
    public class PeticaoRepository : BaseRepository<Peticao>, IPeticaoRepository
    {
        private readonly CPJContext _context;

        public PeticaoRepository(CPJContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Peticao> BuscarPorProcesso(int idProcesso)
        {
            return _context.Peticao.Where(x => x.IdProcesso == idProcesso).ToList();

        }
    }
}
