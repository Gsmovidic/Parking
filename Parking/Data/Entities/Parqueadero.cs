using Parking.Enums;
namespace Parking.Data.Entities
      
{
    public class Parqueadero
    {
        public int Id { get; set; }

        public string Ubicacion { get; set; }

        public int NroCeldasCarro => Celdas == null ? 0 : Celdas.Count(c=>c.TipoVehiculoCelda==TipoVehiculo.Carro);

        public int NroCeldasMoto => Celdas == null ? 0 : Celdas.Count(c=>c.TipoVehiculoCelda==TipoVehiculo.Moto);

        public ICollection<Vehiculo> Vehiculos { get; set;}

        public ICollection<Celda> Celdas { get; set; }

        public ICollection<RegistroEntrada> RegistrosEntradas { get; set; }
    }
}
