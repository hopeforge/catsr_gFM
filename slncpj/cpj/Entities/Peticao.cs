﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cpj.Entities
{
    public class Peticao
    {
        public int Id { get; set; }
        public int IdProcesso { get; set; }
        [ForeignKey("IdProcesso")]
        public Processo Processo { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
    }
}
