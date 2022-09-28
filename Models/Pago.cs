
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamargoInmobiliaria{

    public class Pago{

        public int id { get; set; }
        public int nroPago{ get; set; }
        public DateTime fechaPago { get; set;}
        public float Importe { get; set;}

        [Display (Name ="Contrato")]
        public int ContratoId { get; set;}
      [ForeignKey(nameof(ContratoId))]

      public  Contrato c {get; set;}

    }
    


}