using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace teste_mvc.Models
{
    [Table(name: "Alunos")]
    public class Aluno : Pessoa
    {
        //[Required(ErrorMessage = "Este campo é obrigatório")]
        //[MaxLength(20)]
        //public string CPF { set; get; }
       public string Observacao { set; get; }
        public virtual Pacote Pacote { set; get; }
        public int PacoteId { set; get; }
        public virtual Responsavel Responsavel { set; get; }
      

    }
}
