using Parking.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Data.Entities
{
    public class ClientePlan
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }

        public Plan Plan { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; } 

        public bool Vigente { get; set; }

        public bool EnUso { get; set; }

        public TipoVehiculo TipoVehiculo { get; set; }

    }
}
