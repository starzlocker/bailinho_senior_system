using System;
using System.Linq;
using System.Text.RegularExpressions;
using bailinho_senior_system.models; // Assumindo que este é o namespace correto para o modelo

namespace bailinho_senior_system.models
{
    public class Cliente // Marcada como public
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Id não pode ser negativo.", nameof(value));
                id = value;
            }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome não pode estar vazio.");

                if (value.Length > 150)
                    throw new ArgumentException("O nome não pode conter mais que 150 caracteres");

                this.nome = value;
            }
        }

        private DateTime dt_nascimento;
        public DateTime DtNascimento // Propriedade em PascalCase
        {
            get { return dt_nascimento; }
            set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentException("O campo 'Data de Nascimento' não pode estar vazio.");

                if (value > DateTime.Today)
                    throw new ArgumentException("A 'Data de Nascimento' não pode ser uma data futura.");

                if (value.Year < 1900)
                    throw new ArgumentException("A 'Data de Nascimento' é inválida.");

                this.dt_nascimento = value;
            }
        }

        private char genero;
        public char Genero
        {
            get { return genero; }
            set
            {
                char generoUpper = char.ToUpper(value);

                if (generoUpper != 'M' && generoUpper != 'F')
                    throw new ArgumentException("O campo 'Gênero' deve ser 'M' ou 'F'");

                this.genero = generoUpper;
            }
        }

        private string cpf;
        public string Cpf
        {
            get { return cpf; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("CPF não pode estar vazio.");

                string cpfNumeros = new string(value.Where(char.IsDigit).ToArray());

                if (cpfNumeros.Length != 11)
                    throw new ArgumentException("O CPF deve conter exatamente 11 dígitos.");

                if (!CpfValido(cpfNumeros))
                    throw new ArgumentException("O CPF informado é inválido. " + cpfNumeros);

                this.cpf = cpfNumeros; // Armazena apenas os números
            }
        }

        private string telefone;
        public string Telefone
        {
            get { return telefone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O campo 'Telefone' não pode estar vazio.");

                string numeros = new string(value.Where(char.IsDigit).ToArray());

                if (numeros.Length != 10 && numeros.Length != 11)
                    throw new ArgumentException("O telefone deve conter 10 ou 11 dígitos.");

                this.telefone = numeros; // Armazena apenas os números
            }
        }

        // Construtor padrão e construtor de inicialização (ajustado para usar Propriedades)
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
            this.Id = id;
            this.Nome = nome;
            this.DtNascimento = dataNascimento;
            this.Genero = genero;
            this.Cpf = cpf;
            this.Telefone = telefone;
        }

        // Método estático de validação de CPF (mantido da sua implementação)
        private static bool CpfValido(string cpf)
        {
            if (cpf.Distinct().Count() == 1) return false;

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