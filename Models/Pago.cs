
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamargoInmobiliaria{

    public class Pago{

        public int id { get; set; }
        public DateTime fechaPago { get; set;}
        public float importe { get; set;}

        [Display (Name ="Inquilino")]
        public int InquilinoId { get; set;}
      [ForeignKey(nameof(InquilinoId))]

      public  Inquilino inq {get; set;}

    }
    


}