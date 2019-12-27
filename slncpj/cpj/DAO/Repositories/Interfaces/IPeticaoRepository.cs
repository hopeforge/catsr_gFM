using cpj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.DAO.Repositories.Interfaces
{
    public interface IPeticaoRepository : IBaseRepository<Peticao>
    {
        IEnumerable<Peticao> BuscarPorProcesso(int idProcesso);

    }
}
