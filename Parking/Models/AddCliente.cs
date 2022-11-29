using Parking.Data.Entities;

namespace Parking.Models
{
    public class AddCliente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }

        public string Correo { get; set; }
        //En ves de colocar aqui las colecciones , en el registrar autos , se llama a la entidad en la vista
        //y de ahi pushea los vehiculos ( crear modelo para cliente con plan )
    }
}
