using Parking.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Parking.Data.Entities
{
    public class Celda
    {
        public int Id { get; set; }

        public string Ubicación { get; set; }

        public TipoVehiculo TipoVehiculoCelda { get; set; }

        public bool Disponible { get; set; }

        public Parqueadero Parqueadero { get; set; }

        [AllowNull]
        public string? PlacaVehiculo  { get ; set; }
    }
}
