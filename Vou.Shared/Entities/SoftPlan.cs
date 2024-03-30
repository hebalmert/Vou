using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vou.Shared.Entities
{
    public class SoftPlan
    {
        [Key]
        public int SoftPlanId { get; set; }

        [MaxLength(50, ErrorMessage = "El Maximo de caracteres es {0}")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Display(Name = "Plan")]
        public string Name { get; set; } = null!;

        [Range(1, double.MaxValue, ErrorMessage = "El {0} es Obligatorio")]
        [Display(Name = "Maximo Mikrotik")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public int MaxMikrotik { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "El {0} es Obligatorio")]
        [Display(Name = "Mes(es)")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public int TimeMonth { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; }


        //Relacioens en doble via
        public ICollection<Corporate>? Corporates { get; set; }
    }
}
