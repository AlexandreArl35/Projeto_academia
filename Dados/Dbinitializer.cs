using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teste_mvc.Models;

namespace teste_mvc.Dados
{
    public class Dbinitializer
    {
        public static void Initializer(Context context)
        {
            context.Database.EnsureCreated();
           
            

        }
    }
}
