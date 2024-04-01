using Vou.Shared.Enum;

namespace Vou.Shared.Responses
{
    public class UserResponse
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Document { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Address { get; set; }

        public string UserFrom { get; set; } = null!;

        public UserType UserType { get; set; }

        public string ImageId { get; set; } = null!;

        public string? ImageFullPath { get; set; }

        //Corporacion a la que pertenece el Usuario
        public int CorporateId { get; set; }
    }
}
