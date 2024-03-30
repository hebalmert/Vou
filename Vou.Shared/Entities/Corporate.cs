using System.ComponentModel.DataAnnotations;

namespace Vou.Shared.Entities
{
    public class Corporate
    {
        [Key]
        public int CorporateId { get; set; }

        [Display(Name = "Imagen")]
        public string? ImageId { get; set; }

        [MaxLength(100, ErrorMessage = "El Maximo de caracteres es {0}")]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Display(Name = "Corporacion")]
        public string Name { get; set; } = null!;

        [MaxLength(25, ErrorMessage = "El Maximo de caracteres es {0}")]
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [Display(Name = "Identificacion")]
        public string Document { get; set; } = null!;

        [MaxLength(25, ErrorMessage = "El Maximo de caracteres es {0}")]
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        public string PhoneNumber { get; set; } = null!;


        [MaxLength(256, ErrorMessage = "El campo no puede ser mayor a {0} de largo")]
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion")]
        public string Address { get; set; } = null!;

        //Correo y Plan
        [MaxLength(256, ErrorMessage = "El campo no puede ser mayor a {0} de largo")]
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Range(1, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [Display(Name = "Plan")]
        public int SoftPlanId { get; set; }
        //Tiempo Activo de la cuenta
        //...

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inicio")]
        public DateTime ToStar { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Vencimiento")]
        public DateTime ToEnd { get; set; }

        //Ubicacion Geografica de la Empresa
        [Range(1, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [Display(Name = "Pais")]
        public int CountryId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [Display(Name = "Estado")]
        public int StateId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Debe Seleccionar un {0}")]
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }
        //Fin UIbicacion


        //Cuenta si esta activa o no
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Imagen")]
        public string ImageFullPath => ImageId == string.Empty || ImageId == null
        ? $"https://localhost:7160/Images/NoImage.png"
        : $"https://localhost:7160/Images/ImgCorporate/{ImageId}";
        //? $"https://spi.nexxtplanet.net/Images/NoImage.png"
        //: $"https://spi.nexxtplanet.net/Images/ImgCorporate/{ImageId}";


        //Propiedad de Relaciones entre las Entidades
        //...
        public SoftPlan? SoftPlan { get; set; }

        public Country? Country { get; set; }

        public State? State { get; set; }

        public City? City { get; set; }

        public ICollection<Manager>? Managers { get; set; }
    }
}
