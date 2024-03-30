﻿using System.Text;

namespace Vou.API.Helper
{
    public class ComboHelper : IComboHelper
    {
        //Sistema para Generacion automatica de Clave
        //Se pasa longitud de la clave y caracteres con la que puede hacer la clave
        public string GeneratePass(int longitud, string caracteres)
        {
            StringBuilder res = new();
            Random rnd = new();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }
    }
}
