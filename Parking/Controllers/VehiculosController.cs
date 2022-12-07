using Microsoft.AspNetCore.Mvc;
using Parking.Enums;

namespace Parking.Controllers
{
    public class VehiculosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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
    }
}
