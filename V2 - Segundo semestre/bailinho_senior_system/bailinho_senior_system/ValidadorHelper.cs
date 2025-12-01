using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system
{
    internal class ValidadorHelper
    {
        public static string VerificarVazio(string valor, string nomeCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return $"O campo '{nomeCampo}' não pode estar vazio.";
            }

            return null; // válido
        }
    }
}
