﻿using cpj.DAO.Context;
using cpj.DAO.Repositories.Interfaces;
using cpj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.DAO.Repositories
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        private readonly CPJContext _context;

        public ContaRepository(CPJContext context) : base(context)
        {
            _context = context;
        }
    }
}
