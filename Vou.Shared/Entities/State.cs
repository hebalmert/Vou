using System.ComponentModel.DataAnnotations;

namespace Vou.Shared.Entities
{
    public class State
    {
        public int StateId { get; set; }

        [Display(Name = "Depart/Estado")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }

        //Propiedad Virtual de Consulta
        [Display(Name = "Ciudades")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;


        public Country? Country { get; set; }

        //Relacioens en doble via
        public ICollection<City>? Cities { get; set; }

        public ICollection<Corporate>? Corporates { get; set; }

    }
}
