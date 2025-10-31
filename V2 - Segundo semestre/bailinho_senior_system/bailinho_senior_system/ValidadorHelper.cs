using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system
{
    internal class ValidadorHelper
    {
        /// <summary>
        /// Verifica se um campo string está vazio, nulo ou contém apenas espaços.
        /// </summary>
        /// <param name="valor">Valor da string a ser validada</param>
        /// <param name="nomeCampo">Nome do campo (para a mensagem de erro)</param>
        /// <returns>Mensagem de erro se for inválido, ou null se estiver válido</returns>
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
