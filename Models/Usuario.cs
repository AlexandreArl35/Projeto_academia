using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace teste_mvc.Models
{
    [Table(name: "Usuarios")]
    public class Usuario
    {
        public int Id { set; get; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Campo Login é obrigatório")]
        public string Login { set; get; }

        [StringLength(20, MinimumLength = 6)]
        public string Senha { set; get; }

        public virtual Pessoa Pessoa { set; get; }
      

        
        public enum Perfil
        {
            ADMIN,
            INSTRUTOR,
            ALUNO
        }

        public virtual Perfil Perfis { set; get; }
       
        
        // public virtual Perfil Perfil { set; get; }
        // public virtual int PerfilId { set; get; }
    }
}
