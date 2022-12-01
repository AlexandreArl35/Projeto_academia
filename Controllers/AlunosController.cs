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
    public class AlunosController : Controller
    {
        private readonly Context _context;

        public AlunosController(Context context)
        {
            _context = context;
        }

        private static string _Perfil;
        public static void Perfil(string Nome)
        {
            _Perfil = Nome;

        }
        // GET: Alunos
        public async Task<IActionResult> Index()
        {
           if(_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            var context = _context.Alunos.Include(a => a.Usuario).Include(a => a.Pacote);
            return View(await context.ToListAsync());
        }

        // GET: Alunos/Details/5
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

            var aluno = await _context.Alunos
                .Include(a => a.Usuario)
                .Include(a => a.Pacote)
                .Include(a => a.Responsavel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }
           
            return View(aluno);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Login");
            ViewData["PacoteId"] = new SelectList(_context.Pacotes, "Id", "Descricao");
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CPF,Observacao,PacoteId,Id,Nome,DataNasc,Telefone,Email,Cep,Cidade,Bairro,Rua,Numero,UsuarioId")] Aluno aluno, [Bind("Id,Login,Senha")] Usuario usuario, [Bind("Id,Nome,Cpf,AlunoId")] Responsavel responsavel)
        {
            if (ModelState.IsValid)
            {

                if (usuario.Login != "")
                {
                    //coloca id usuario em aluno
                    aluno.Usuario = usuario;
                    //seta permissao
                    usuario.Perfis = Usuario.Perfil.ALUNO;
                    //add usuario em seguida aluno por causa da FK
                    _context.Usuario.Add(usuario);
                }
                
                _context.Add(aluno);

                if (responsavel.Nome != "" || responsavel.Cpf != "")
                {
                    responsavel.Aluno = aluno;
                    _context.responsavel.Add(responsavel);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Usuario, "Id", "Nome", responsavel.AlunoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Login", aluno.UsuarioId);
            ViewData["PacoteId"] = new SelectList(_context.Pacotes, "Id", "Descricao", aluno.PacoteId);
            return View(aluno);
        }

        // GET: Alunos/Edit/5
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

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Login", aluno.UsuarioId);
            ViewData["PacoteId"] = new SelectList(_context.Pacotes, "Id", "Descricao", aluno.PacoteId);
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CPF,Observacao,PacoteId,Id,Nome,DataNasc,Telefone,Email,Cep,Cidade,Bairro,Rua,Numero,UsuarioId")] Aluno aluno,[Bind("Id,Login,Senha")] Usuario usuario, [Bind("Id,Nome,Cpf,AlunoId")] Responsavel responsavel)
        {
          
            if (id != aluno.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    aluno.UsuarioId = usuario.Id;
                    //aluno.Usuario = usuario;
                    //aluno.UsuarioId = idUsu;
                    usuario.Perfis = Usuario.Perfil.ALUNO;
                    _context.Update(usuario);
                    _context.Update(aluno);
                    if (responsavel.Cpf != "" || responsavel.Nome != "")
                    {
                        //  responsavel.AlunoId = id;
                        responsavel.AlunoId = aluno.Id;
                        _context.Update(responsavel);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    if (!AlunoExists(aluno.Id) || !UsuarioExists(usuario.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Usuario, "Id", "Nome", responsavel.AlunoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Login", aluno.UsuarioId);
            ViewData["PacoteId"] = new SelectList(_context.Pacotes, "Id", "Descricao", aluno.PacoteId);
            return View(aluno);
        }

        // GET: Alunos/Delete/5
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

            var aluno = await _context.Alunos
                .Include(a => a.Usuario)
                .Include(a => a.Pacote)
                .Include(a => a.Responsavel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [Bind("CPF,Observacao,PacoteId,Id,Nome,DataNasc,Telefone,Email,Cep,Cidade,Bairro,Rua,Numero,UsuarioId")] Aluno aluno, [Bind("Id,Login,Senha,Perfis")] Usuario usuario, [Bind("Id,Nome,Cpf,AlunoId")] Responsavel responsavel)
        {

            _context.Alunos.Remove(aluno);
            _context.responsavel.Remove(responsavel);
             _context.Usuario.Remove(usuario);
          
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
        private bool ResponsavelExists(int id)
        {
            return _context.responsavel.Any(e => e.Id == id);
        }
    }
}
