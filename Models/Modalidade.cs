using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace teste_mvc.Models
{
    [Table(name: "Modalidades")]
    public class Modalidade
    {
        public int Id { set; get; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public double Valor { set; get; }

         public virtual ICollection<Pacote> Pacote { set; get; }

         public virtual ICollection<Instrutor> Instrutor { set; get; }


        //public virtual ICollection<Instrutores> Instrutores { get; set; }
       // public virtual int InstrutoresId { set; get; }

        //public virtual ICollection<Pessoas> Pessoas { set; get; }
    }
}
