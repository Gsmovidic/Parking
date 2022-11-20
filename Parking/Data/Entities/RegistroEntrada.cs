using Parking.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Data.Entities
{
    public class RegistroEntrada
    {
        public int Id { get; set; }

        public TipoCliente TipoCliente { get; set; }
        [NotMapped]
        public DateOnly FechaEntrada { get; set; }
        [NotMapped]
        public TimeOnly HoraEntrada { get; set; }

        public string PlacaVehiculo { get; set; }

        public Parqueadero Parqueadero {get;set;}
    }
}
