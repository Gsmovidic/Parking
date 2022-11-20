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
            VehiculosController vehiculoC= new VehiculosController();
            if (ModelState.IsValid)
            {
                string pl = entradaVM.Placa;
                if (pl.Length != 6)
                {
                    return NotFound();
                }
                else
                {
                    if(vehiculoC.ValidarPlaca(pl))
                    {
                        TipoVehiculo tV = vehiculoC.ValidarTipoVehiculo(pl);
                        Vehiculo vehi = await _context.Vehiculos.FirstOrDefaultAsync(v=>v.Placa==pl);
                        if (vehi!=null) 
                        {
                            ClientePlan clientePlan = await _context.ClientesPlanes.FirstOrDefaultAsync(cP=>cP.Cliente.Id==vehi.Cliente.Id && cP.Plan.TipoVehiculo==tV && cP.Vigente);
                        }
                        else 
                        {
                           
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
            return NotFound();
        }
    }
}
