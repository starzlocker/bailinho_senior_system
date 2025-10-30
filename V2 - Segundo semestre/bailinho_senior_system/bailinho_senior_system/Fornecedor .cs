using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system
{
    internal class Fornecedor
    {
        private string cnpj;
        private string nome;
        private string email;
        private string telefone;

        public Fornecedor() { }

        public Fornecedor(
            string cnpj,
            string nome,
            string email, 
            string telefone
        ) {
            setCnpj(cnpj);
            setNome(nome);
            setEmail(email);
            setTelefone(telefone);
        }

        public string getCnpj()
        {
            return cnpj;
        }

        public string getNome()
        {
            return nome;
        }

        public string getEmail()
        {
            return email;
        }

        public string getTelefone()
        {
            return telefone;
        }

        public string setCnpj(string cnpj)
        {
            string validacaoDeVazio = ValidadorHelper.VerificarVazio(cnpj, "CNPJ");
            if (validacaoDeVazio != null) return validacaoDeVazio;

            string cnpjNumeros = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpjNumeros.Length != 14)
                return "O CNPJ deve conter exatamente 14 dígitos.";

            if (!CnpjValido(cnpjNumeros))
                return "O CNPJ informado é inválido.";

            this.cnpj = cnpjNumeros;
            return "sucesso";
        }

        public string setNome(string nome)
        {
            string validacaoDeVazio = ValidadorHelper.VerificarVazio(nome, "nome");
            if (validacaoDeVazio != null) return validacaoDeVazio;

            if (nome.Length > 150)
                return "O nome não pode conter mais que 150 caracteres";

            this.nome = nome;
            return "sucesso";
        }

        public string setEmail(string email)
        {
            string validacaoDeVazio = ValidadorHelper.VerificarVazio(email, "Email");
            if (validacaoDeVazio != null) return validacaoDeVazio;

            if (email.Length > 150)
                return "O email não pode conter mais que 150 caracteres";

            if (!email.Contains("@") || !email.Contains("."))
                return "O email informado é inválido.";

            this.email = email;
            return "sucesso";
        }

        public string setTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return "O campo 'Telefone' não pode estar vazio.";

            string numeros = new string(telefone.Where(char.IsDigit).ToArray());
            if (numeros.Length != 10 && numeros.Length != 11)
                return "O telefone deve conter 10 ou 11 dígitos.";

            this.telefone = numeros;
            return "sucesso";
        }

        private static bool CnpjValido(string cnpj)
        {
            if (cnpj.Distinct().Count() == 1) return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string cnpjCalculado = tempCnpj + digito2;
            return cnpj.EndsWith(cnpjCalculado.Substring(12));
        }
    }
}
