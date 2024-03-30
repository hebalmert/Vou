using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vou.API.Data;
using Vou.Shared.Entities;
using Vou.Shared.Enum;
using Vou.Shared.SystemDTOs;

namespace Vou.API.Helper
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IComboHelper _comboHelper;
        private readonly IConfiguration _configuration;

        public UserHelper(DataContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            IComboHelper comboHelper,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _comboHelper = comboHelper;
            _configuration = configuration;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<bool> DeleteUser(string username)
        {
            User? userAsp = await _userManager.FindByEmailAsync(username);
            if (userAsp == null)
            {
                return false;
            }
            IdentityResult response = await _userManager.DeleteAsync(userAsp);
            return response.Succeeded;
        }

        public async Task<User> AddUserUsuarioAsync(string firstname, string lastname,
                        string email, string phone, string address, string job,
                        int Idcorporate, string ImagenFull, string Origin, bool UserActivo)
        {
            string largo = "AaFfHhnNOPpsSRrErTtDcJjBmM098765#432@1";
            var clave = _comboHelper.GeneratePass(8, largo);

            User user = new User
            {
                FirstName = firstname,
                LastName = lastname,
                FullName = firstname + " " + lastname,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Job = job,
                CorporateId = Idcorporate,
                Photo = ImagenFull,
                UserFrom = Origin,
                Activo = UserActivo
            };

            IdentityResult result = await _userManager.CreateAsync(user, clave); //(modelo, Password)
            if (result != IdentityResult.Success)
            {
                return null!;
            }

            User newUser = await GetUserAsync(email);
            //Solo creamos al usuario dentro del Sistema de UserASP, sin Roles ni Claims.
            //Cuando Creemos el Usuario_RoleDetails, es cuando creamos los Roles y Claimss
            return newUser;
        }


        public async Task<User> AddUserSystemAsync(string firstname, string lastname,
                        string email, string phone, UserType usertype, string address, string job,
                        int Idcorporate, string photo, string Origin, bool UserActivo)
        {
            string largo = "AaFfHhnNOPpsSRrErTtDcJjBmM098765#432@1";
            var clave = _comboHelper.GeneratePass(8, largo);

            User user = new()
            {
                FirstName = firstname,
                LastName = lastname,
                FullName = firstname + " " + lastname,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Job = job,
                CorporateId = Idcorporate,
                Photo = photo,
                UserFrom = Origin,
                UserType = usertype,
                Activo = UserActivo
            };

            IdentityResult result = await _userManager.CreateAsync(user, clave); //(modelo, Password)
            if (result != IdentityResult.Success)
            {
                return null!;
            }

            User newUser = await GetUserAsync(email);
            await AddUserToRoleAsync(newUser, usertype.ToString());
            await AddUserClaims(usertype, email);

            return newUser;
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task RemoveUserToRoleAsync(User user, string roleName)
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task RemoveUserClaims(UserType userType, string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            await _userManager.RemoveClaimAsync(usuario!, new Claim(userType.ToString(), "1"));
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task AddUserClaims(UserType userType, string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            await _userManager.AddClaimAsync(usuario!, new Claim(userType.ToString(), "1"));
        }


        public async Task<User> GetUserAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user!;
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }


        //Sistema de Login
        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        //Metodo para el uso del JWT Token de seguridad.
        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }


        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());

            return user!;
        }


        //Para Validar Correo y Activar la cuenta del usuario\
        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }


        //Recuperacion de Clave automatica del Usuario
        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

    }
}
