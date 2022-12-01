using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using teste_mvc.Dados;
using teste_mvc.Models;

namespace teste_mvc.Controllers
{
    public class InstrutoresController : Controller
    {
        private readonly Context _context;

        public InstrutoresController(Context context)
        {
            _context = context;
        }
        private static string _Perfil;
        public static void Perfil(string Nome)
        {
            _Perfil = Nome;

        }
        // GET: Instrutores
        public async Task<IActionResult> Index()
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            var context = _context.Instrutor.Include(i => i.Usuario).Include(i => i.Modalidades);
            return View(await context.ToListAsync());
        }

        // GET: Instrutores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var instrutor = await _context.Instrutor
                .Include(i => i.Usuario)
                .Include(i => i.Modalidades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instrutor == null)
            {
                return NotFound();
            }

            return View(instrutor);
        }

        // GET: Instrutores/Create
        public IActionResult Create()
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Login");
            ViewData["ModalidadeId"] = new SelectList(_context.Modalidade, "Id", "Nome");
            return View();
        }

        // POST: Instrutores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Salario,CPF,ModalidadeId,Id,Nome,DataNasc,Telefone,Email,Cep,Cidade,Bairro,Rua,Numero,UsuarioId")] Instrutor instrutor, [Bind("Id,Login,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
               if(usuario.Login != "")
                {
                    instrutor.Usuario = usuario;
                    //seta permissao
                    usuario.Perfis = Usuario.Perfil.INSTRUTOR;
                    //add usuario em seguida aluno por causa da FK
                    _context.Usuario.Add(usuario);
                }
                
                _context.Add(instrutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Login", instrutor.UsuarioId);
            ViewData["ModalidadeId"] = new SelectList(_context.Modalidade, "Id", "Nome", instrutor.ModalidadeId);
            return View(instrutor);
        }

        // GET: Instrutores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var instrutor = await _context.Instrutor.FindAsync(id);
            if (instrutor == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Login", instrutor.UsuarioId);
            ViewData["ModalidadeId"] = new SelectList(_context.Modalidade, "Id", "Nome", instrutor.ModalidadeId);
            return View(instrutor);
        }

        // POST: Instrutores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Salario,CPF,ModalidadeId,Id,Nome,DataNasc,Telefone,Email,Cep,Cidade,Bairro,Rua,Numero,UsuarioId")] Instrutor instrutor, [Bind("Id,Login,Senha")] Usuario usuario)
        {
            if (id != instrutor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    instrutor.UsuarioId = usuario.Id;
                    usuario.Perfis = Usuario.Perfil.INSTRUTOR;
                    _context.Update(usuario);
                    _context.Update(instrutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstrutorExists(instrutor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Login", instrutor.UsuarioId);
            ViewData["ModalidadeId"] = new SelectList(_context.Modalidade, "Id", "Nome", instrutor.ModalidadeId);
            return View(instrutor);
        }

        // GET: Instrutores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var instrutor = await _context.Instrutor
                .Include(i => i.Usuario)
                .Include(i => i.Modalidades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instrutor == null)
            {
                return NotFound();
            }

            return View(instrutor);
        }

        // POST: Instrutores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("Salario,CPF,ModalidadeId,Id,Nome,DataNasc,Telefone,Email,Cep,Cidade,Bairro,Rua,Numero,UsuarioId")] Instrutor instrutor, [Bind("Id,Login,Senha,Perfis")] Usuario usuario)
        {
            //var instrutor = await _context.Instrutor.FindAsync(id);
            _context.Instrutor.Remove(instrutor);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstrutorExists(int id)
        {
            return _context.Instrutor.Any(e => e.Id == id);
        }
    }
}
