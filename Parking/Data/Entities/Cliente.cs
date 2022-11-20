namespace Parking.Data.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }

        public string Correo { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }

        public ICollection<ClientePlan> ClientePlanes { get; set; }
    }
}
