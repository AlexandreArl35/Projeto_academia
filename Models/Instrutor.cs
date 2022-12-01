using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace teste_mvc.Models
{
    [Table(name: "Instrutores")]
    public class Instrutor : Pessoa
    {
       
        public string Salario { set; get; }

        public virtual Modalidade Modalidades { set; get; }
        public int ModalidadeId { set; get; }

    }
}
