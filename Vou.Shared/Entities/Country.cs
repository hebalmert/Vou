using System.ComponentModel.DataAnnotations;

namespace Vou.Shared.Entities
{
    public class Country
    {
        public int CountryId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es Obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} Caracter")]
        [Display(Name = "Pais")]
        public string Name { get; set; } = null!;

        //Propiedad Virtual de consulta
        [Display(Name = "Estados/Departamentos")]
        public int StatesNumber => States == null ? 0 : States.Count;

        //Relacioens en doble via
        public ICollection<State>? States { get; set; }

        public ICollection<Corporate>? Corporates { get; set; }
    }
}
