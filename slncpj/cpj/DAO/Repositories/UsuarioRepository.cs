using cpj.DAO.Context;
using cpj.DAO.Repositories.Interfaces;
using cpj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.DAO.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly CPJContext _context;

        public UsuarioRepository(CPJContext context) : base(context)
        {
            _context = context;
        }
    }
}
