using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamargoInmobiliaria
{

    public class Inmueble{
/*
        public enum UsoInm{
            comercial,
            residencial
        }
        public enum TipoInm{
            casa,
            departamento,
            local,
            deposito
        }
        */

        [Key]
        [Display (Name="Código")]
        public int InmuebleId{ get; set; }
        [Display(Name ="Dirección")] 
        public string Direccion { get; set; }
        public int Ambientes { get; set; }
        public int Superficie { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public double Precio { get; set; }
        public string Tipo { get; set; }
        public  string Uso { get; set; }

        [Display (Name="Propietario")]
        public int IdPropietario{ get; set; }

        [ForeignKey(nameof(IdPropietario))]
        public Propietario Propietario{ get; set; }

    }
}


