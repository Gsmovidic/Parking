using Parking.Enums;

namespace Parking.Data.Entities
{
    public class Plan
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public float ValorPlan { get; set; }

        public TipoVehiculo TipoVehiculo { get; set; }

        public bool Disponibilidad { get; set; }

        public ICollection<ClientePlan> ClientePlanes { get; set; }
    }
}
