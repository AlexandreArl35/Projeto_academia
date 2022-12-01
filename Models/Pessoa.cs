using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace teste_mvc.Models
{
    [Table(name: "Pessoas")]
    public class Pessoa
    {
        
        public int Id { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100)]
        public string Nome { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20)]
        public string CPF { set; get; }

        public DateTime DataNasc { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20)]
        public string Telefone { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100)]
        public string Email { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(15)]
        public string Cep { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100)]
        public string Cidade { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100)]
        public string Bairro { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100)]
        public string Rua { set; get; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(10)]
        public string Numero { set; get; }

        public virtual Usuario Usuario { set; get; }

        public int UsuarioId { set; get; }

    }
}
