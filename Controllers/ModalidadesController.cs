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
    public class ModalidadesController : Controller
    {
        private readonly Context _context;
        private static string _Perfil;
        public static void Perfil(string Nome)
        {
            _Perfil = Nome;

        }
        public ModalidadesController(Context context)
        {
            _context = context;
        }

        // GET: Modalidades
        public async Task<IActionResult> Index()
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await _context.Modalidade.ToListAsync());
        }

        // GET: Modalidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modalidade = await _context.Modalidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modalidade == null)
            {
                return NotFound();
            }

            return View(modalidade);
        }

        // GET: Modalidades/Create
        public IActionResult Create()
        {
            if (_Perfil != "ADMIN")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Modalidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Valor")] Modalidade modalidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modalidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modalidade);
        }

        // GET: Modalidades/Edit/5
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

            var modalidade = await _context.Modalidade.FindAsync(id);
            if (modalidade == null)
            {
                return NotFound();
            }
            return View(modalidade);
        }

        // POST: Modalidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Valor")] Modalidade modalidade)
        {
            if (id != modalidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modalidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModalidadeExists(modalidade.Id))
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
            return View(modalidade);
        }

        // GET: Modalidades/Delete/5
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

            var modalidade = await _context.Modalidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modalidade == null)
            {
                return NotFound();
            }

            return View(modalidade);
        }

        // POST: Modalidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modalidade = await _context.Modalidade.FindAsync(id);
            _context.Modalidade.Remove(modalidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModalidadeExists(int id)
        {
            return _context.Modalidade.Any(e => e.Id == id);
        }
    }
}
