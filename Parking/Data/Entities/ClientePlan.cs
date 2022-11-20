using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Data.Entities
{
    public class ClientePlan
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }

        public Plan Plan { get; set; }

        [NotMapped]
        public DateOnly FechaInicio { get; set; }
        [NotMapped]
        public DateOnly FechaFin { get; set; } 

        public bool Vigente { get; set; }

        public bool EnUso { get; set; }

    }
}
