using Microsoft.AspNetCore.Identity;
using Vou.Shared.Entities;
using Vou.Shared.Enum;
using Vou.Shared.SystemDTOs;

namespace Vou.API.Helper
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<bool> DeleteUser(string username);



        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);


        Task RemoveUserToRoleAsync(User user, string roleName);

        Task RemoveUserClaims(UserType userType, string email);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task AddUserClaims(UserType userType, string email);


        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();




        //Se pasa el Usuario y el Pass, para ver si es Valido el Usuario, nos da un Result
        //metodo para implementar la socitud del token de seguridad.
        Task<SignInResult> ValidatePasswordAsync(User user, string password);

        //Para Registro de UserASP desde el modulo de Usuarios / Sistema de Corporacion
        Task<User> AddUserUsuarioAsync(string firstname, string lastname,
                        string email, string phone, string address, string job,
                        int Idcorporate, string ImagenFile, string Origin, bool UserActivo);

        //Para Registro de UserASP desde el modulo de SeedDB y Manager
        Task<User> AddUserSystemAsync(string firstname, string lastname,
                        string email, string phone, UserType usertype, string address, string job,
                        int Idcorporate, string ImagenFile, string Origin, bool UserActivo);


        //Para Administrar El Cambio de Clave de los usuarios
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<User> GetUserAsync(Guid userId);


        //Metodos para Validar el Correo del Usuario para activar la cuenta

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);


        //Recuperacion de la Clave de Usuario
        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
    }
}
