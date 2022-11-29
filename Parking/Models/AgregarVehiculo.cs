using Parking.Data.Entities;
using Parking.Enums;

namespace Parking.Models
{
    public class AgregarVehiculo
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }
        public string Placa { get; set; }

        public string Color { get; set; }

        public TipoVehiculo TipoVehiculo { get; set; }


        public Cliente Cliente { get; set; }
    }
}
