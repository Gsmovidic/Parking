namespace Parking.Data.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }

        public string Correo { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }

        public int NumeroVehiculos => Vehiculos == null ? 0 : Vehiculos.Count;

        public ICollection<ClientePlan> ClientePlanes { get; set; }
        public int NumeroPlanes => ClientePlanes == null ? 0 : ClientePlanes.Count;
    }
}
