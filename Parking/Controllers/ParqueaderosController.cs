using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Data.Entities;
using Parking.Models;

namespace Parking.Controllers
{
    public class ParqueaderosController : Controller
    {
        private readonly DataContext _context;
        public ParqueaderosController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parqueaderos.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> CrearParqueadero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearParqueadero(Parqueadero parqueadero)
        {

            _context.Parqueaderos.Add(parqueadero);
            try
            {

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "ya existe un parqueadero con este nombre");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            //}
            return View(parqueadero);

        }
        public async Task<IActionResult> Detalles(int? id)
        {

            if (id == null || _context.Parqueaderos == null)
            {
                return NotFound();
            }

            var parqueadero = await _context.Parqueaderos.FirstOrDefaultAsync(m => m.Id == id);
            if (parqueadero == null)
            {
                return NotFound();
            }

            return View(parqueadero);
            //return View();
        }
        [HttpGet]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null || _context.Parqueaderos == null)
            {
                return NotFound();
            }

            var parqueadero = await _context.Parqueaderos
                //.Include(c => c.Celdas)
               // .ThenInclude(v => v.Vehiculos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parqueadero == null)
            {
                return NotFound();
            }

            return View(parqueadero);
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmed(int id)
        {
            if (_context.Parqueaderos == null)
            {
                return Problem("Entity set 'DataContext.Parqueaderos'  is null.");
            }
            var parqueadero = await _context.Parqueaderos.FindAsync(id);
            if (parqueadero != null)
            {
                _context.Parqueaderos.Remove(parqueadero);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
