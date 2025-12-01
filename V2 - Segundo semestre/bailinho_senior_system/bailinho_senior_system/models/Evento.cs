using System;

namespace bailinho_senior_system.models
{
    public class Evento
    {
        public Evento() { }
        public Evento(int id, string nome, string descricao, DateTime data, TimeSpan hora, 
                        decimal valor_entrada, int id_endereco, Endereco endereco)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Data = data;
            Hora = hora;
            ValorEntrada = valor_entrada;
            IdEndereco = id_endereco;
            Endereco = endereco;
            EnderecoCompleto = endereco.EnderecoCompleto;
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Id não pode ser negativo", nameof(value));
                id = value;
            }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                string validacaoDeVazio = ValidadorHelper.VerificarVazio(value, "Nome");

                if (validacaoDeVazio != null)
                    throw new ArgumentException(validacaoDeVazio);

                if (value.Length > 150)
                    throw new ArgumentException("O nome do evento não pode conter mais que 150 caracteres");

                this.nome = value;
            }
        }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                if (value != null && value.Length > 150)
                    throw new ArgumentException("A descrição do evento não pode conter mais que 150 caracteres");

                this.descricao = value;
            }
        }

        private DateTime data;
        public DateTime Data
        {
            get { return data; }
            set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentException("O campo 'Data' não pode estar vazio");

                this.data = value;
            }
        }

        private TimeSpan hora;
        public TimeSpan Hora
        {
            get { return hora; }
            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentException("O campo 'Hora' é inválido");

                this.hora = value;
            }
        }

        private decimal valor_entrada;
        public decimal ValorEntrada
        {
            get { return valor_entrada; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("O valor da entrada não pode ser negativo");

                this.valor_entrada = value;
            }
        }

        private int id_endereco;
        public int IdEndereco
        {
            get { return id_endereco; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("O Evento deve estar vinculado a um Endereço válido.");

                this.id_endereco = value;
            }
        }

        public Endereco Endereco { get; set; }
        public string EnderecoCompleto { get; set; }
    }
}