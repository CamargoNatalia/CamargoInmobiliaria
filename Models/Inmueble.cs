using System.ComponentModel.DataAnnotations;

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
        public int Id { get; set; }
        public string Direccion { get; set; }
        public int Ambientes { get; set; }
        public int Superficie { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public decimal Precio { get; set; }
        public string Tipo { get; set; }
        public  string Uso { get; set; }

    }
}


