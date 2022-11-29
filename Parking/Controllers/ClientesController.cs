using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Data.Entities;
using Parking.Enums;
using Parking.Models;
using System.Diagnostics.Metrics;

namespace Parking.Controllers
{
    public class ClientesController : Controller
    {
        private readonly DataContext _context;
        public ClientesController(DataContext context)
        {
            _context = context;
        }
        /*
         * MANEJO DEL CLIENTE
        */
        public async Task<IActionResult> Index()
        {
            /*return View(await _context.Clientes
                .Include(c => c.NumeroVehiculos)
                .Include(cp => cp.ClientePlanes)
                .ToListAsync());*/
            return View(await _context.Clientes.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> CrearCliente()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearCliente(Cliente cliente)
        {
            //if (ModelState.IsValid)
            //{
            // {
            /*
            Cliente cliente = new()
            {
                Id = addCliente.Id,
                Cedula = addCliente.Cedula,
                Nombre = addCliente.Nombre,
            };  */
            _context.Clientes.Add(cliente);
            try
            {

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "ya existe un usuairo con este correo o cedula");
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
            return View(cliente);

        }
        [HttpGet]
        public async Task<IActionResult> EditarCliente(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            Cliente  cliente = await _context.Clientes
               // .Include(v=> v.NumeroVehiculos)
               // .Include(p=>p.NumeroPlanes)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        [HttpPost]
        public async Task<IActionResult> EditarCliente(int id, Cliente cliente)
        {
            /*
            Cliente cliente = new()
            {
                Id = model.Id,
                Cedula = model.Cedula,
                Nombre = model.Nombre,
                Correo = model.Correo,
            };*/
           
            try
            {
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "ya existe un usuairo con este correo o cedula");
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
            return View(cliente);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente=await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

             return View(cliente);
            //return View();
        }
        /*
            MANEJO DE LOS VEHICULOS
         */
        public bool ValidarPlaca(string pl)
        {
            for (int i = 0; i < pl.Length; i++)
            {
                if (i >= 0 && i <= 2)
                {
                    if (!Char.IsLetter(pl[i]))
                    {
                        return false;
                    }
                }
                if (i >= 3 && i <= 4)
                {

                    if (!Char.IsNumber(pl[i]))
                    {
                        return false;
                    }
                }
                if (i == 5)
                {
                    if (!Char.IsNumber(pl[i]) && !Char.IsLetter(pl[i]))
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        public TipoVehiculo ValidarTipoVehiculo(string pl)
        {
            return Char.IsNumber(pl[pl.Length - 1]) ? TipoVehiculo.Carro : TipoVehiculo.Moto;
        }
        [HttpGet]
        public async Task<IActionResult> AgregarVehiculo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cliente cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            AgregarVehiculo model = new()
            {
                IdCliente = cliente.Id,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarVehiculo(AgregarVehiculo model)
        {
            Vehiculo vehiculo = new()
            {
                Id = model.Id,
                Cliente= await _context.Clientes.FindAsync(model.IdCliente),
                Placa= model.Placa,
                Color=model.Color,
                TipoVehiculo=model.TipoVehiculo,
            };
            if (ValidarPlaca(model.Placa))
            {
                _context.Add(vehiculo);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "La placa no es valida");
            }          
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { Id = model.Cliente.Id });

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un vehiculo con esta placa o propietario");
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
            return View(model);
        }
    }
}

