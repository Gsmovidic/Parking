using Microsoft.AspNetCore.Mvc;
using Parking.Data;
using Parking.Data.Entities;
using Parking.Enums;

namespace Parking.Controllers
{
    public class ParqueaderoController : Controller
    {
        private readonly DataContext _context;

        public ParqueaderoController(DataContext context)
        {
            _context = context;
        }

        public bool ValidarDisponibilidadCeldas(Parqueadero park , TipoVehiculo tV) 
        {
          return  tV==TipoVehiculo.Carro?  (park.NroCeldasCarro> 0?  true : false)  : (park.NroCeldasMoto > 0? true : false) ; //Metodo para saber si hay celdas disponibles 
        }

        public async Task ActualizarNumeroCeldas(Parqueadero park, TipoVehiculo tV)
        {
            if (tV == TipoVehiculo.Carro) 
            {
                park.NroCeldasCarro = park.NroCeldasCarro - 1;
            }
            else
            {
                park.NroCeldasMoto = park.NroCeldasMoto - 1;
            }
            _context.Update(park);
            await _context.SaveChangesAsync();
        }
    }
}
