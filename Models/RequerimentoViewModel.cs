using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAiMvc.Models
{
    public class RequerimentoViewModel
    {
         public int Id { get; set; }
        public string Momento { get; set; }
        public string TipoRequerimento { get; set; }
        public string IdLocacao { get; set; }
        public string Observacao { get; set; }
        public string Situacao { get; set; }
        public string DataAtualizacao { get; set; }
        public string IdUsuarioAtualizacao { get; set; }
    }
}