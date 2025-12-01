using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class Venda
    {
        public Venda() { }
        public Venda(int id, decimal valor_total, string forma_pagamento,
                     int id_cliente, int id_evento, DateTime data_venda, string nome_cliente, string nome_evento)
        {
            Id = id;
            ValorTotal = valor_total;
            FormaPagamento = forma_pagamento;
            IdCliente = id_cliente;
            IdEvento = id_evento;
            DataVenda = data_venda;
            NomeCliente = nome_cliente;
            NomeEvento = nome_evento;
        }

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

        private decimal valor_total;
        public decimal ValorTotal
        {
            get { return valor_total; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Valor total não pode ser negativo.");
                valor_total = value;
            }
        }

        private List<string> pagamentosAceitos = new List<string> { "Pix", "Crédito", "Débito", "Dinheiro" };

        private string forma_pagamento;
        public string FormaPagamento
        {
            get { return forma_pagamento; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Forma de pagamento não pode ser vazia.");

                if (!pagamentosAceitos.Contains(value))
                    throw new ArgumentException("Forma de pagamento inválida.");        
          
                forma_pagamento = value;
            }
        }

        private int id_cliente;
        public int IdCliente
        {
            get { return id_cliente; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Cliente deve ser válido.");
                id_cliente = value;
            }
        }

        private int id_evento;
        public int IdEvento
        {
            get { return id_evento; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Evento deve ser válido.");
                id_evento = value;
            }
        }

        private DateTime data_venda;
        public DateTime DataVenda
        {
            get { return data_venda; }
            set
            {
                data_venda = value;
            }
        }

        public string NomeCliente { get; set; }
        public string NomeEvento { get; set; }
    }
}