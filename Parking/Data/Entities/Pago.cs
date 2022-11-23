using Parking.Enums;

namespace Parking.Data.Entities
{
    public class Pago
    {
        public int Id { get; set; }
        public float CantidadDinero { get; set; }
        public TipoPago TipoPago { get; set; } 
    }
}
