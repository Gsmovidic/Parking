using Parking.Enums;
namespace Parking.Data.Entities
      
{
    public class Parqueadero
    {
        public int Id { get; set; }

        public string Ubicacion { get; set; }

        public int NroCeldasCarro { get; set; }

        public int NroCeldasMoto { get; set; }

        public ICollection<Vehiculo> Vehiculos { get; set;}

        public ICollection<Celda> Celdas { get; set; }

        public ICollection<RegistroEntrada> RegistrosEntradas { get; set; }
    }
}
