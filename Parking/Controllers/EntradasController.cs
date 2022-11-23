using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Data.Entities;
using Parking.Enums;
using Parking.Models;

namespace Parking.Controllers
{
    public class EntradasController : Controller
    {

        private readonly DataContext _context;

        public EntradasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Entrada()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Entrada(EntradaVM entradaVM)
        {
            VehiculosController vehiculoC = new VehiculosController();
            if (ModelState.IsValid)
            {
                string pl = entradaVM.Placa;
                if (pl.Length != 6)
                {
                    return NotFound();
                }
                else
                {
                    if (vehiculoC.ValidarPlaca(pl))
                    {
                        TipoVehiculo tV = vehiculoC.ValidarTipoVehiculo(pl);
                        Vehiculo vehi = await _context.Vehiculos.FirstOrDefaultAsync(v => v.Placa == pl);
                        if (vehi != null)
                        {
                            ClientePlan clientePlan = await _context.ClientesPlanes.FirstOrDefaultAsync(cP => cP.Cliente.Id == vehi.Cliente.Id && cP.Plan.TipoVehiculo == tV && cP.Vigente);
                        }
                        else
                        {
                            //EntradaOcional(entradaVM);
                            
                            return base.RedirectToAction(nameof(Models.EntradaOcacional));
                        }
                    }
                    else
                    {
                        return NotFound();
                    }


                }
            }

            else
            {
                return NotFound();
            }
            return View(entradaVM);
        }
        [HttpGet]
        public async Task<IActionResult> EntradaOcacional()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EntradaOcacional(EntradaOcacional entradaOC)
        {
            //if (ModelState.IsValid)
            //{
                Recibo recibo = new()
                {
                    Id = entradaOC.Id,
                    ValorHora = entradaOC.ValorHora,
                    Hora = entradaOC.Hora,
                    fecha = entradaOC.fecha,
                    Detalles = entradaOC.Detalles
                };
                _context.Recibos.Add(recibo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                try
                {
                    _context.Recibos.Add(recibo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Fallo la creacion de recibo (ocacional) Depura");
                }
            //}
                        
          
            return View(entradaOC);
        }
    }
}
