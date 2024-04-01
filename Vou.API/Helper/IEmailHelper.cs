using Vou.Shared.Responses;

namespace Vou.API.Helper
{
    public interface IEmailHelper
    {
        //Sistema para Confirmar las Cuentas de Usuario desde el Correo
        Task<Response> ConfirmarCuenta(string to, string NameCliente,
            string subject, string body);
    }
}
