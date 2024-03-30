using System.ComponentModel.DataAnnotations;
using Vou.Shared.Entities;

namespace Vou.Shared.EntitiesSoft
{
    public class Register
    {
        public int RegisterId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Display(Name = "Crear Tickets")]
        public int OrderTickets { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Display(Name = "Ticket")]
        public int Tickets { get; set; }

        //Control para cada venta que se realiza por un Administrador, sea en 
        //Venta One o venta por cantidad
        [Range(0, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Display(Name = "Venta")]
        public int Sells { get; set; }

        //El numero de control de cada venta SellOneCachier que hace cada Cajero
        [Range(0, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Display(Name = "Venta Cachier")]
        public int SellCachier { get; set; }

        //Este lleva el control de cada comision que se genera en cada venta de ticket
        [Range(0, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Display(Name = "Control Comision")]
        public int PorcentCacheir { get; set; }


        //Control de pago de Comisiones.
        [Range(0, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Display(Name = "Pago Comisiones")]
        public int PayPorcentCacheir { get; set; }


        //A que Corporacion Pertenece
        public int CorporateId { get; set; }

        public Corporate? Corporate { get; set; }
    }
}
