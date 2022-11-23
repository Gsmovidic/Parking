using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parking.Data.Entities
{
    public class Recibo
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Price")]
        [Required(ErrorMessage = "The field {0} is Required")]
        public float ValorHora { get; set; }
        [Required(ErrorMessage = "The field {0} is Required")]
        public float Hora { get;set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Total")]
        public float Total =>(float) ValorHora * Hora;

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Date")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public DateTime fecha { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "details")]
        public string? Detalles { get; set; }

    }
}
