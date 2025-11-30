using bailinho_senior_system.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace bailinho_senior_system.repositories
{
    internal class RelatorioRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public int GetTotalVendas()
        {
            int total = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM venda;", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int count))
                        total = count;
                }
            }
            return total;
        }

        public float getTotalVendido()
        {
            float total = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT SUM(valor_total) FROM venda;", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && float.TryParse(result.ToString(), out float sum))
                        total = sum;
                }
            }
            return total;
        }

        public float getTicketMedio()
        {
            float ticketMedio = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT AVG(valor_total) FROM venda;", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && float.TryParse(result.ToString(), out float avg))
                        ticketMedio = avg;
                }
            }
            return ticketMedio;
        }

        public int getEventosRealizados()
        {
            int total = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM evento WHERE  `data` < CURDATE();", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int count))
                        total = count;
                }
            }
            return total;
        }

        public int getFornecedoresCadastrados()
        {
            int total = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM fornecedor;", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int count))
                        total = count;
                }
            }
            return total;
        }

        public int getClientesCadastrados()
        {
            int total = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM cliente;", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int count))
                        total = count;
                }
            }
            return total;
        }

        public int getEstoqueTotalProdutos()
        {
            int total = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT SUM(qtd_estoque) FROM produto;", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int sum))
                        total = sum;
                }
            }
            return total;
        }

        public int getProdutosCadastrados()
        {
            int total = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM produto;", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int count))
                        total = count;
                }
            }
            return total;
        }

        public int getProdutosVendidos()
        {
            int total = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT SUM(quantidade) FROM produtovenda;", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int sum))
                        total = sum;
                }
            }
            return total;
        }
    }
}
