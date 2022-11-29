using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Data.Entities;
using Parking.Models;

namespace Parking.Controllers
{
    public class ClientesController : Controller
    {
        private readonly DataContext _context;
        public ClientesController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
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
    }
}

