using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace teste_mvc.Models
{

    [Table(name: "Responsaveis")]
    public class Responsavel
    {
        public int Id { set; get; }

        [MaxLength(100)]
        public string Nome { set; get; }

        [MaxLength(20)]
        public string Cpf{ set; get; }

        public virtual Aluno Aluno { set; get; }
        public int AlunoId { set; get; }

    }
}
