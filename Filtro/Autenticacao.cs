using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace teste_mvc
{
    public class Autenticacao : IActionFilter
    {
        private static string _Perfil;
        public static void Perfil(string Nome)
        {
            _Perfil = Nome;

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string[] viewdWithoutLogin; 
            if(_Perfil == "ADMIN")
            {
                viewdWithoutLogin =
                new[] { "Alunos","Instrutores","Modalidades", "Pacotes", "Responsaveis","Usuarios"};
            }else if(_Perfil == "ALUNO")
            {
                viewdWithoutLogin =
                new[] { "AlunoPessoais"};
            }
            else if (_Perfil == "INSTRUTOR")
            {
                viewdWithoutLogin =
                new[] { "Login", "Home" };
                
            }
            else
            {
                viewdWithoutLogin =
               new[] { "Login", "Home" };
            }
            



            String Controller = ((ControllerBase)context.Controller).
                ControllerContext.ActionDescriptor.ControllerName;
            if ((!viewdWithoutLogin.Contains(Controller)) &&
                (context.HttpContext.Session.GetString("IdUsuarioLogado") == null))
            {
               
                context.HttpContext.Response.Redirect("/Home");
            }
          

        }
    }
}