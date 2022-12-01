using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace teste_mvc.Filtro
{
    public class Autenticacao : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string[] viewdWithoutLogin =
                new[] { "Login", "Estrutura", "Home", "Novidades" };

            String Controller = ((ControllerBase)context.Controller).
                ControllerContext.ActionDescriptor.ControllerName;

            if ((!viewdWithoutLogin.Contains(Controller)) &&
                (context.HttpContext.Session.GetString("usuarioLogado") == null))
            {
                context.HttpContext.Response.Redirect("/Login/Index");
            }
        }
    }
}
