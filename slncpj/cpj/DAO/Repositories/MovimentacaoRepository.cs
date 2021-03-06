﻿using cpj.DAO.Context;
using cpj.DAO.Repositories.Interfaces;
using cpj.Entities;
using System.Collections.Generic;
using System.Linq;

namespace cpj.DAO.Repositories
{
    public class MovimentacaoRepository : BaseRepository<Movimentacao>, IMovimentacaoRepository
    {
        private readonly CPJContext _context;

        public MovimentacaoRepository(CPJContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Movimentacao> BuscarPorProcesso(int idProcesso)
        {
            return _context.Movimentacao.Where(x => x.IdProcesso == idProcesso).ToList();
        }
    }
}
