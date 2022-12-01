using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using teste_mvc.Dados;
using teste_mvc.Models;

namespace teste_mvc.Controllers
{
    public class LoginController : Controller
    {
        private Context _context;

        public LoginController(Context context)
        {
            this._context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            Autenticacao.Perfil("");
            AlunosController.Perfil("");
            AlunoController.Perfil(0,"");
            InstrutoresController.Perfil("");
            ModalidadesController.Perfil("");
            PacotesController.Perfil(""); 

            HttpContext.Session.Clear();
            // HttpContext.RequestAborted.IsCancellationRequested.ToString();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Logar(Usuario usuarioModel)
        {
            var LoginValidate = ModelState["Login"];
            var SenhaValidate = ModelState["Senha"];
            if (SenhaValidate.RawValue == "")
            {
                ViewBag.ErrorLogin = "Campo senha não pode ser vazio";
                return View("Login");
            }
            else if ((LoginValidate != null && !LoginValidate.Errors.Any()) ||
                (SenhaValidate != null && !SenhaValidate.Errors.Any()))
            { //Validações estão OK
                string usuario = usuarioModel.Login;
                string senha = usuarioModel.Senha;
                //Busca objeto no banco de dados

                var usuarioObj = _context.Usuario.Where(linha =>
                        linha.Login.Equals(usuario) &&
                        linha.Senha.Equals(senha)).FirstOrDefault();
               
                if (usuarioObj != null)
                {//Usuario existente no banco de dados
                    Autenticacao.Perfil(usuarioObj.Perfis.ToString());
                    AlunosController.Perfil(usuarioObj.Perfis.ToString());
                    AlunoController.Perfil(usuarioObj.Id, usuarioObj.Perfis.ToString());
                    InstrutoresController.Perfil(usuarioObj.Perfis.ToString());
                    ModalidadesController.Perfil(usuarioObj.Perfis.ToString());
                    PacotesController.Perfil(usuarioObj.Perfis.ToString());
                    TempData["usuarioLogado"] = usuarioObj.Login.ToString();
                    HttpContext.Session.
                    SetString("IdUsuarioLogado", usuarioObj.Id.ToString());
                    HttpContext.Session.
                        SetString("NomeUsuarioLogado", usuarioObj.Login.ToString());
                    HttpContext.Session.
                       SetString("Acesso", usuarioObj.Perfis.ToString());

                    if (usuarioObj.Perfis == Usuario.Perfil.ADMIN)
                    {
                        return RedirectToAction("Index", "Menu");
                    }
                    else if(usuarioObj.Perfis == Usuario.Perfil.INSTRUTOR)
                    {
                        ViewBag.ErrorLogin = "Caro Instrutor, Sua Area de acesso estará disponivel em breve";
                        return RedirectToAction("Login", "Logar");
                    }
                    else if(usuarioObj.Perfis == Usuario.Perfil.ALUNO)
                    {
                        return RedirectToAction("AlunoPessoais", "Aluno");
                    }
                    else
                    {
                        ViewBag.ErrorLogin = "Ocorreu um erro, por favor consulte o administrador";
                        return View("Login");
                    }
                }
                else
                {
                    
                    ViewBag.ErrorLogin = "Credenciais inválidas";
                    return View("Login");
                }
            }
            else { return View("Login"); }
        }


       
    }
}

