using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CamargoInmobiliaria;

namespace CamargoInmobiliaria{

public class Contrato{

    [Display (Name ="Código")]
    public int Id{ get; set; }
    [Display (Name ="Fecha Inicial")]
    public DateTime fechaInicio { get; set; }
    [Display (Name ="Fecha de Finalizacion")]
    public DateTime fechaFin { get; set; }
    public float montoMensual {get; set; }
    
     [Display (Name ="Inquilino")]
    public int InquilinoId { get; set;}
      [ForeignKey(nameof(InquilinoId))]
   
    public Inquilino inq {get; set;}

    [Display (Name ="Inmuebles")] 
    public int InmuebleId { get; set;}
      [ForeignKey(nameof(InmuebleId))]

    public Inmueble inm {get; set;}
}
}