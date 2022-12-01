
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using teste_mvc.Models;
using teste_mvc.Dados;
using Microsoft.EntityFrameworkCore;

namespace teste_mvc.Controllers
{
    public class AlunoController : Controller
    {
        private static int _IdAluno;
        private static string _Perfil;
        public static void Perfil(int id,string perfil)
        {
            _IdAluno = id;
            _Perfil = perfil;
        }
        private Context _context;

        public AlunoController(Context context)
        {
            this._context = context;
        }
       
        public ActionResult AlunoPessoaisAsync()
        {
            if(_Perfil != "ALUNO" && _Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            List<Aluno> _LISTA;
            List<Pacote> _LstPacote;
            List<Modalidade> _LstModalidade;
            List<Responsavel> _LstResponsavel;
            try
            {
                 _LISTA = _context.Alunos
                  .Where(x => x.UsuarioId == _IdAluno).ToList();
                _LstPacote = _context.Pacotes.Where(x => x.Id == _LISTA[0].PacoteId).ToList();

                _LstModalidade = _context.Modalidade.Where(x => x.Id == _LstPacote[0].ModalidadeId).ToList();

                _LstResponsavel = _context.responsavel.Where(x => x.AlunoId == _LISTA[0].Id).ToList();
            }
            catch(Exception ex)
            {
                TempData["MsgRecusado"] = "INVALIDO";
                return RedirectToAction("Index", "Home");
            }
            //_LISTA[0].DataNasc = ;
         
            

            ViewBag.LISTA = _LISTA;
            ViewBag.Pac = _LstPacote;
            ViewBag.Resp = _LstResponsavel;
            ViewBag.Mod = _LstModalidade;
            return View();
        }


    }
}
