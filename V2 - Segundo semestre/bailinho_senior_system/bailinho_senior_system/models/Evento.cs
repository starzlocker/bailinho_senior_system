using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class Evento
    {
        public Evento() { }

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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome não pode estar vazio.");

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

        public string EnderecoLogradouro { get; set; }
        public string EnderecoNumero { get; set; }
        public string EnderecoBairro { get; set; }
        public string EnderecoCidade { get; set; }
        public string EnderecoEstado { get; set; }
        public string EnderecoCep { get; set; }
        public string EnderecoComplemento { get; set; }

        public string EnderecoCompleto
        {
            get
            {
                if (string.IsNullOrEmpty(EnderecoLogradouro))
                {
                    return "Endereço Não Informado";
                }

                // Padrão de endereço brasileiro: Rua, Número - Bairro, Cidade/Estado, CEP
                string complementoStr = string.IsNullOrWhiteSpace(EnderecoComplemento) ? "" : $" ({EnderecoComplemento})";

                return $"{EnderecoLogradouro}, {EnderecoNumero}{complementoStr} - {EnderecoBairro}, {EnderecoCidade}/{EnderecoEstado} - CEP: {EnderecoCep}";
            }
        }
    }
}