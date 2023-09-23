using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamargoInmobiliaria
{
    public class Pago{

        [Display(Name="Código")]
        public int Id { get; set; }

        [Display(Name="Numero de Pago")]
        public int nroPago { get; set; }
        public DateTime fechaPago { get; set; }
        public decimal importe { get; set; }

        [Display(Name ="Número de Contrato")]
        public int? ContratoId {get; set;}
        [ForeignKey(nameof(ContratoId))]
        public Contrato contrato {get; set;}
    }
}

