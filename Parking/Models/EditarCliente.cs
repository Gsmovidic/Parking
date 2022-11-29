using Parking.Data.Entities;

namespace Parking.Models
{
    public class EditarCliente : AddCliente
    {
        public ICollection<Vehiculo> Vehiculos { get; set; }

        public int NumeroVehiculos => Vehiculos == null ? 0 : Vehiculos.Count;

        public ICollection<ClientePlan> ClientePlanes { get; set; }
        public int NumeroPlanes => ClientePlanes == null ? 0 : ClientePlanes.Count;
    }
}
