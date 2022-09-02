using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamargoInmobiliaria{

public class Inmueble{

    
        [Display (Name ="Código")]
    public int inmueblesId { get; set; }
    public string Uso { get; set; }
    public string Tipo { get; set; }
    public int  Ambientes { get; set; }
    public string Direccion {get; set;}
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    public float Precio { get; set; }
    
        [Display (Name ="Dueño")]
    public int PropietarioId { get; set;}
      [ForeignKey(nameof(PropietarioId))]

    public  Propietario Duenio {get; set;}

}
}