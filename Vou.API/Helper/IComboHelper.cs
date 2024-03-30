namespace Vou.API.Helper
{
    public interface IComboHelper
    {
        //Sistema para Generacion automatica de Clave
        //Se pasa longitud de la clave y caracteres con la que puede hacer la clave
        string GeneratePass(int longitud, string caracteres);
    }
}
