using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bailinho_senior_system.models
{
    public class Venda // CLASSE TORNADA PÚBLICA
    {
        public Venda() { }

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

        private string forma_pagamento;
        public string FormaPagamento
        {
            get { return forma_pagamento; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Forma de pagamento não pode ser vazia.");

                // Opcional: Adicionar validação de lista de valores permitidos (Pix, Crédito, etc.)

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

        // Propriedades de Lookup
        public string NomeCliente { get; set; }
        public string NomeEvento { get; set; }
    }
}