using cpj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.DAO.Repositories.Interfaces
{
    public interface IMovimentacaoRepository : IBaseRepository<Movimentacao>
    {
        IEnumerable<Movimentacao> BuscarPorProcesso(int idProcesso);
    }
}
