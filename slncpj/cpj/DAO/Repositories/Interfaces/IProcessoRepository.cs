using cpj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.DAO.Repositories.Interfaces
{
    public interface IProcessoRepository : IBaseRepository<Processo>
    {
        IEnumerable<Processo> BuscaPorRequerente(string requerente);
        IEnumerable<Processo> BuscaPorRequerido(string requerido);
    }
}
