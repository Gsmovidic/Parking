using Microsoft.AspNetCore.Mvc;
using Parking.Data;
using Parking.Data.Entities;

namespace Parking.Controllers
{
    public class ClientePlanController : Controller
    {
        private readonly DataContext _context;
        public ClientePlanController(DataContext context)
        {
            _context = context;
        }


        public async Task ActualizarEnuso(ClientePlan cP, int Orden) //1 si es entrada, 2 si es entrada para saber que hacer  
        {
            cP.EnUso = Orden == 1 ? true : false;
            _context.Update(cP);
            await _context.SaveChangesAsync();
        }
    }
}
