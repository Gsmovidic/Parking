﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Data.Entities;

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
    }
}
