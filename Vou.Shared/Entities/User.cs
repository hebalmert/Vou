using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vou.Shared.Enum;

namespace Vou.Shared.Entities
{
    public class User :  IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "El campo no puede ser mayor a {0} de largo")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50, ErrorMessage = "El campo no puede ser mayor a {0} de largo")]
        public string LastName { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "El campo no puede ser mayor a {0} de largo")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [MaxLength(50, ErrorMessage = "El {0} no puede tener mas de {1} Caracteres.")]
        [Display(Name = "Puesto Trabajo")]
        public string Job { get; set; } = null!;

        //Identificacion de Origenes y Role del Usuario
        [Display(Name = "Origen")]
        public string? UserFrom { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }
        //Fin.........

        [Display(Name = "Imagen")]
        public string? Photo { get; set; }

        [NotMapped]
        [Display(Name = "FullPath")]
        public string? PhotoPath { get; set; }

        //TODO:Change Addres to Image
        public string ImageFullPath => Photo == string.Empty || Photo == null
        ? $"https://localhost:7246/Images/NoImage.png"
        : $"https://localhost:7246/Images/ImgUser/{Photo}";
        //? $"https://spi.nexxtplanet.net/Images/NoImage.png"
        //: $"https://spi.nexxtplanet.net/Images/ImgUser/{Photo}";

        //...
        public int? CorporateId { get; set; }

        public Corporate? Corporate { get; set; }

        //Verificacion de usuario activo
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
    }
}
