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
    public class PacotesController : Controller
    {
        private static string _Perfil;
        public static void Perfil(string Nome)
        {
            _Perfil = Nome;

        }
        private readonly Context _context;

        public PacotesController(Context context)
        {
            _context = context;
        }

        // GET: Pacotes
        public async Task<IActionResult> Index()
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            var context = _context.Pacotes.Include(p => p.Modalidades);
            return View(await context.ToListAsync());
        }

        // GET: Pacotes/Details/5
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

            var pacote = await _context.Pacotes
                .Include(p => p.Modalidades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacote == null)
            {
                return NotFound();
            }

            return View(pacote);
        }

        // GET: Pacotes/Create
        public IActionResult Create()
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["ModalidadeId"] = new SelectList(_context.Modalidade, "Id", "Nome");
            return View();
        }

        // POST: Pacotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,ValorMensalidade,DataInicio,DataTermino,ModalidadeId")] Pacote pacote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModalidadeId"] = new SelectList(_context.Modalidade, "Id", "Nome", pacote.ModalidadeId);
            return View(pacote);
        }

        // GET: Pacotes/Edit/5
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

            var pacote = await _context.Pacotes.FindAsync(id);
            if (pacote == null)
            {
                return NotFound();
            }
            ViewData["ModalidadeId"] = new SelectList(_context.Modalidade, "Id", "Nome", pacote.ModalidadeId);
            return View(pacote);
        }

        // POST: Pacotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,ValorMensalidade,DataInicio,DataTermino,ModalidadeId")] Pacote pacote)
        {
            if (id != pacote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacoteExists(pacote.Id))
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
            ViewData["ModalidadeId"] = new SelectList(_context.Modalidade, "Id", "Nome", pacote.ModalidadeId);
            return View(pacote);
        }

        // GET: Pacotes/Delete/5
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

            var pacote = await _context.Pacotes
                .Include(p => p.Modalidades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacote == null)
            {
                return NotFound();
            }

            return View(pacote);
        }

        // POST: Pacotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacote = await _context.Pacotes.FindAsync(id);
            _context.Pacotes.Remove(pacote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacoteExists(int id)
        {
            return _context.Pacotes.Any(e => e.Id == id);
        }
    }
}
