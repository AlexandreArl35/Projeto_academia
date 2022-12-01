using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace teste_mvc.Models
{
    [Table(name: "Pacotes")]
    public class Pacote
    {
        //public Pacote()
        //{
        //    this.Modalidades = new HashSet<Modalidade>().ToList();
        //}
        public int Id { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Descricao { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public double ValorMensalidade { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataInicio { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataTermino { set; get; }

        public virtual ICollection<Aluno> Aluno { set; get; }
        public virtual Modalidade Modalidades { set; get; }
        public int ModalidadeId { get; set; }

    }
}
