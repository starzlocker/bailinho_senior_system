using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class Cliente
    {
        private int id;

        private string nome;

        private DateTime dt_nascimento;

        private char genero;

        private string cpf;

        private string telefone;

        public Cliente() { }

        public Cliente(
            int id,
            string nome,
            DateTime dataNascimento,
            char genero,
            string cpf,
            string telefone
        )
        {
            setId(id);
            setNome(nome);
            setDt_nascimento(dataNascimento);
            setGenero(genero);
            setCPF(cpf);
            setTelefone(telefone);
        }

        public int getId()
        {
            return id;
        }

        public string getNome()
        {
            return nome;
        }

        public DateTime getDt_nascimento()
        {
            return dt_nascimento;
        }

        public char getGenero()
        {
            return genero;
        }

        public string getCpf()
        {
            return cpf;
        }

        public string getTelefone()
        {
            return telefone;
        }

        private void setId(int id)
        {
            this.id = id;
        }

        public void setNome(string nome)
        {
            string validacaoDeVazio = ValidadorHelper.VerificarVazio(nome, "nome");

            if (validacaoDeVazio != null) throw new ArgumentException(validacaoDeVazio);

            if (nome.Length > 150)
            {
                throw new ArgumentException("O nome não pode conter mais que 150 caracteres");
            }

            this.nome = nome;
        }

        public void setDt_nascimento(DateTime dt_nascimento)
        {
            if (dt_nascimento == DateTime.MinValue)
                throw new ArgumentException("O campo 'Data de Nascimento' não pode estar vazio.");

            if (dt_nascimento > DateTime.Today)
                throw new ArgumentException("A 'Data de Nascimento' não pode ser uma data futura.");

            if (dt_nascimento.Year < 1900)
                throw new ArgumentException("A 'Data de Nascimento' é inválida.");

            this.dt_nascimento = dt_nascimento;
        }

        public void setGenero(char genero)
        {
            char generoUpper = char.ToUpper(genero);

            if (generoUpper != 'M' && generoUpper != 'F')
                throw new ArgumentException("O campo 'Gênero' deve ser 'M' ou 'F'.");

            this.genero = genero;
        }

        public void setCPF(string cpf)
        {
            string validacaoDeVazio = ValidadorHelper.VerificarVazio(cpf, "CPF");

            if (validacaoDeVazio != null) throw new ArgumentException(validacaoDeVazio);

            string cpfNumeros = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpfNumeros.Length != 11)
                throw new ArgumentException("O CPF deve conter exatamente 11 dígitos.");

            if (!CpfValido(cpfNumeros))
                throw new ArgumentException("O CPF informado é inválido.");

            this.cpf = cpf;
        }

        public void setTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                throw new ArgumentException("O campo 'Telefone' não pode estar vazio.");

            string numeros = new string(telefone.Where(char.IsDigit).ToArray());

            if (numeros.Length != 10 && numeros.Length != 11)
                throw new ArgumentException("O telefone deve conter 10 ou 11 dígitos.");

            this.telefone = telefone;
        }

        private static bool CpfValido(string cpf)
        {
            if (cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string cpfCalculado = tempCpf + digito2;

            return cpf.EndsWith(cpfCalculado.Substring(9));
        }
    }
}
