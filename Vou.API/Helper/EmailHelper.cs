using SendGrid.Helpers.Mail;
using SendGrid;

namespace Vou.API.Helper
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IConfiguration _configuration;
        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Shared.Responses.Response> ConfirmarCuenta(string to,
             string NameCliente, string subject, string body)
        {
            //En este punto tomamos los calore de los AppSetting.Json
            //y lo igualamos a variables para poderlos manipular
            var apiKey = _configuration.GetValue<string>("SENDGRID_API_KEY");
            var email = _configuration.GetValue<string>("SENDGRID_FROM");
            var nombre = _configuration.GetValue<string>("SENDGRID_NOMBRE");

            //Cargamos la Utilidad de SendGrid, que es el sistema de envio de datos.
            var cliente = new SendGridClient(apiKey);
            var from = new EmailAddress(email, nombre);
            var tO = new EmailAddress(to, NameCliente);
            var mensajeTextoPlano = "Sistema de Acticacion de Cuenta";
            var singleEmail = MailHelper.CreateSingleEmail(from, tO, subject,
                mensajeTextoPlano, body);

            var respuesta = await cliente.SendEmailAsync(singleEmail);
            if (respuesta.IsSuccessStatusCode)
            {
                return new Shared.Responses.Response { IsSuccess = true };
            }
            else
            {
                return new Shared.Responses.Response { IsSuccess = false };
            }

        }
    }
}
