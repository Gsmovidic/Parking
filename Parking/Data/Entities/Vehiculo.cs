using Parking.Enums;

namespace Parking.Data.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }

        public string Color { get; set; }

        public TipoVehiculo TipoVehiculo { get; set; }

        public Cliente Cliente { get; set; }

        public Parqueadero Parqueadero { get; set; }


    }
}
