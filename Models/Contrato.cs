using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamargoInmobiliaria{

    public class Contrato{

        [Key]
        [Display (Name ="Número de Contrato")]
        public int Id { get; set; }
        [Display (Name ="Inicio del Contrato")]
        public DateTime fechaInicio { get; set; }
        [Display (Name ="Fin del  Contrato")]
        public DateTime fechaFinal { get; set; }
        [Display (Name ="Ingreso Mensual")]
        public decimal montoMensual { get; set; }

        [Display (Name="Inquilino")]
        public int IdInquilino{ get; set; }

        [ForeignKey(nameof(IdInquilino))]
        public Inquilino Cliente{ get; set; }

        public Inmueble Inmueble {get; set; }
        }

    }

