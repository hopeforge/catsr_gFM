using cpj.DAO.Context;
using cpj.DAO.Repositories.Interfaces;
using cpj.Entities;
using System.Collections.Generic;
using System.Linq;

namespace cpj.DAO.Repositories
{
    public class ProcessoRepository : BaseRepository<Processo>, IProcessoRepository
    {
        private readonly CPJContext _context;

        public ProcessoRepository(CPJContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Processo> BuscaPorRequerente(string requerente)
        {
           return _context.Processo.Where(x => x.Requerente.Contains(requerente)).ToList();
        }

        public IEnumerable<Processo> BuscaPorRequerido(string requerido)
        {
            return _context.Processo.Where(x => x.Requerido.Contains(requerido)).ToList();
        }
    }
}
