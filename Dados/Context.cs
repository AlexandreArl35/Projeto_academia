using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using teste_mvc.Models;

namespace teste_mvc.Dados
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Aluno> Alunos { set; get; }
        public DbSet<Instrutor> Instrutor { set; get; }
        public DbSet<Modalidade> Modalidade { get; set; }
        public DbSet<Pacote> Pacotes { set; get; }
       // public DbSet<Perfil> Perfil { set; get; }
        public DbSet<Pessoa> Pessoa { set; get; }
        public DbSet<Responsavel> responsavel { set; get; }
        public DbSet<Usuario> Usuario { set; get; }
    }
}
