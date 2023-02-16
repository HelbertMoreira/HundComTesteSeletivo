using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Controllers
{
    public class TopicosController : Controller
    {
        private readonly AppDbContext _context;

        public TopicosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Topicos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Topicos.ToListAsync());
        }

        // GET: Topicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Topicos == null)
            {
                return NotFound();
            }

            var topico = await _context.Topicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topico == null)
            {
                return NotFound();
            }

            return View(topico);
        }

        // GET: Topicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tema")] Topico topico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topico);
        }

        // GET: Topicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Topicos == null)
            {
                return NotFound();
            }

            var topico = await _context.Topicos.FindAsync(id);
            if (topico == null)
            {
                return NotFound();
            }
            return View(topico);
        }

        // POST: Topicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tema")] Topico topico)
        {
            if (id != topico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicoExists(topico.Id))
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
            return View(topico);
        }

        // GET: Topicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Topico == null)
            {
                return NotFound();
            }

            var topico = await _context.Topico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topico == null)
            {
                return NotFound();
            }

            return View(topico);
        }

        // POST: Topicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Topico == null)
            {
                return Problem("Entity set 'AddDbContext.Topico'  is null.");
            }
            var topico = await _context.Topico.FindAsync(id);
            if (topico != null)
            {
                _context.Topico.Remove(topico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicoExists(int id)
        {
          return _context.Topico.Any(e => e.Id == id);
        }
    }
}
