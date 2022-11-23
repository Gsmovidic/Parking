using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class EntradaOcacional
    {
        
        public int Id { get; set; }

     
        public string Placa { get; set; }
       
        
        public float ValorHora { get; set; }
       
        public float Hora { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Total")]
        public float Total => (float)ValorHora * Hora;

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Date")]
        public DateTime fecha { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "details")]
        public string? Detalles { get; set; }

        public EntradaVM entrada { get; set; }
    }
}
