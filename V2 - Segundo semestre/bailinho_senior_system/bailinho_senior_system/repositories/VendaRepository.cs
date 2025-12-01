using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using bailinho_senior_system.models;
using bailinho_senior_system.config;

namespace bailinho_senior_system.repositories
{
    public class VendaRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public void CreateVenda(Venda venda, List<ProdutoVenda> itensVenda)
        {
            if (itensVenda == null || itensVenda.Count == 0)
            {
                throw new ArgumentException("A venda deve conter pelo menos um item.");
            }

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string sqlVenda =
                        "INSERT INTO Venda (valor_total, forma_pagamento, id_cliente, id_evento, data_venda) " +
                        "VALUES (@valor_total, @forma_pagamento, @id_cliente, @id_evento, @data_venda); " +
                        "SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmdVenda = new MySqlCommand(sqlVenda, connection, transaction))
                    {
                        cmdVenda.Parameters.AddWithValue("@valor_total", venda.ValorTotal);
                        cmdVenda.Parameters.AddWithValue("@forma_pagamento", venda.FormaPagamento);
                        cmdVenda.Parameters.AddWithValue("@id_cliente", venda.IdCliente);
                        cmdVenda.Parameters.AddWithValue("@data_venda", venda.DataVenda);
                        cmdVenda.Parameters.AddWithValue("@id_evento", venda.IdEvento);

                        object result = cmdVenda.ExecuteScalar();
                        if (result == null) 
                            throw new Exception("Falha ao obter ID da venda.");

                        venda.Id = Convert.ToInt32(result);
                    }

                    string sqlItem =
                        "INSERT INTO ProdutoVenda (id_produto, id_venda, quantidade, valor) " +
                        "VALUES (@id_produto, @id_venda, @quantidade, @valor);";

                    string sqlEstoque =
                        "UPDATE Produto SET qtd_estoque = qtd_estoque - @quantidade WHERE id = @id_produto;";

                    foreach (var item in itensVenda)
                    {
                        item.IdVenda = venda.Id;

                        using (MySqlCommand cmdItem = new MySqlCommand(sqlItem, connection, transaction))
                        {
                            cmdItem.Parameters.AddWithValue("@id_produto", item.IdProduto);
                            cmdItem.Parameters.AddWithValue("@id_venda", item.IdVenda);
                            cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                            cmdItem.Parameters.AddWithValue("@valor", item.Valor);
                            cmdItem.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmdEstoque = new MySqlCommand(sqlEstoque, connection, transaction))
                        {
                            cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                            cmdEstoque.Parameters.AddWithValue("@id_produto", item.IdProduto);

                            if (cmdEstoque.ExecuteNonQuery() == 0)
                            {
                                throw new Exception($"Falha ao subtrair estoque para o Produto ID {item.IdProduto}.");
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    throw new Exception("Erro durante a criação da venda. Transação desfeita.", ex);
                }
            }
        }

        public Venda GetVenda(int id)
        {
            Venda venda = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql =
                        "SELECT tv.id, tv.valor_total, tv.forma_pagamento, tv.id_cliente, tv.id_evento, " +
                        "tc.nome AS NomeCliente, te.nome AS NomeEvento " +
                        "FROM Venda tv " +
                        "INNER JOIN Cliente tc ON tv.id_cliente = tc.id " +
                        "INNER JOIN Evento te ON tv.id_evento = te.id " +
                        "WHERE tv.id = @id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                venda = new Venda(
                                    reader.GetInt32("id"),
                                    reader.GetDecimal("valor_total"),
                                    reader.GetString("forma_pagamento"),
                                    reader.GetInt32("id_cliente"),
                                    reader.GetInt32("id_evento"),
                                    DateTime.Now,
                                    reader.GetString("NomeCliente"),
                                    reader.GetString("NomeEvento")
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar venda.", ex);
            }

            return venda;
        }

        public List<Venda> GetVendas()
        {
            var vendas = new List<Venda>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql =
                        "SELECT tv.id, tv.valor_total, tv.forma_pagamento, tv.id_cliente, tv.id_evento, " +
                        "tc.nome AS NomeCliente, te.nome AS NomeEvento, tv.data_venda AS DataEvento " +
                        "FROM Venda tv " +
                        "INNER JOIN Cliente tc ON tv.id_cliente = tc.id " +
                        "INNER JOIN Evento te ON tv.id_evento = te.id " +
                        "ORDER BY tv.id DESC;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Venda venda = new Venda(
                                reader.GetInt32("id"),
                                reader.GetDecimal("valor_total"),
                                reader.GetString("forma_pagamento"),
                                reader.GetInt32("id_cliente"),
                                reader.GetInt32("id_evento"),
                                reader.GetDateTime("DataEvento"),
                                reader.GetString("NomeCliente"),
                                reader.GetString("NomeEvento")
                            );

                            vendas.Add(venda);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar todas as vendas. " + ex, ex);
            }

            return vendas;
        }

        public void UpdateVenda(Venda venda, List<ProdutoVenda> novosItens)
        {
            if (venda.Id <= 0) throw new ArgumentException("ID da venda inválido.");

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    ReverterEstoque(venda.Id, connection, transaction, true);

                    string sqlDeleteItens = "DELETE FROM ProdutoVenda WHERE id_venda = @id_venda;";
                    using (MySqlCommand cmdDeleteItens = new MySqlCommand(sqlDeleteItens, connection, transaction))
                    {
                        cmdDeleteItens.Parameters.AddWithValue("@id_venda", venda.Id);
                        cmdDeleteItens.ExecuteNonQuery();
                    }

                    string sqlUpdateVenda = @"
                        UPDATE Venda 
                        SET valor_total = @valor_total, forma_pagamento = @forma_pagamento, 
                            id_cliente = @id_cliente, id_evento = @id_evento
                        WHERE id = @id;";

                    using (MySqlCommand cmdUpdateVenda = new MySqlCommand(sqlUpdateVenda, connection, transaction))
                    {
                        cmdUpdateVenda.Parameters.AddWithValue("@id", venda.Id);
                        cmdUpdateVenda.Parameters.AddWithValue("@valor_total", venda.ValorTotal);
                        cmdUpdateVenda.Parameters.AddWithValue("@forma_pagamento", venda.FormaPagamento);
                        cmdUpdateVenda.Parameters.AddWithValue("@id_cliente", venda.IdCliente);
                        cmdUpdateVenda.Parameters.AddWithValue("@id_evento", venda.IdEvento);
                        cmdUpdateVenda.ExecuteNonQuery();
                    }

                    string sqlItem = "INSERT INTO ProdutoVenda (id_produto, id_venda, quantidade, valor) VALUES (@id_produto, @id_venda, @quantidade, @valor);";
                    string sqlEstoque = "UPDATE Produto SET qtd_estoque = qtd_estoque - @quantidade WHERE id = @id_produto;";

                    foreach (var item in novosItens)
                    {
                        item.IdVenda = venda.Id;

                        using (MySqlCommand cmdItem = new MySqlCommand(sqlItem, connection, transaction))
                        {
                            cmdItem.Parameters.AddWithValue("@id_produto", item.IdProduto);
                            cmdItem.Parameters.AddWithValue("@id_venda", item.IdVenda);
                            cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                            cmdItem.Parameters.AddWithValue("@valor", item.Valor);
                            cmdItem.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmdEstoque = new MySqlCommand(sqlEstoque, connection, transaction))
                        {
                            cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                            cmdEstoque.Parameters.AddWithValue("@id_produto", item.IdProduto);
                            if (cmdEstoque.ExecuteNonQuery() == 0)
                            {
                                throw new Exception($"Falha ao subtrair novo estoque para o Produto ID {item.IdProduto}.");
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    throw new Exception("Erro durante a atualização da venda. Transação desfeita.", ex);
                }
            }
        }

        public void DeleteVenda(int vendaId)
        {
            if (vendaId <= 0) return;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    ReverterEstoque(vendaId, connection, transaction, true);

                    string sqlDeleteItens = "DELETE FROM ProdutoVenda WHERE id_venda = @id_venda;";
                    using (MySqlCommand cmdDeleteItens = new MySqlCommand(sqlDeleteItens, connection, transaction))
                    {
                        cmdDeleteItens.Parameters.AddWithValue("@id_venda", vendaId);
                        cmdDeleteItens.ExecuteNonQuery();
                    }

                    using (MySqlCommand cmdDeleteVenda = new MySqlCommand("DELETE FROM Venda WHERE id=@id;", connection, transaction))
                    {
                        cmdDeleteVenda.Parameters.AddWithValue("@id", vendaId);
                        cmdDeleteVenda.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    throw new Exception("Erro durante a exclusão da venda. Transação desfeita.", ex);
                }
            }
        }

        private void ReverterEstoque(int vendaId, MySqlConnection connection, MySqlTransaction transaction, bool adicionar)
        {
            string sqlItens = "SELECT id_produto, quantidade FROM ProdutoVenda WHERE id_venda = @id_venda;";

            List<ProdutoVenda> itensAntigos = new List<ProdutoVenda>();
            using (MySqlCommand cmdItens = new MySqlCommand(sqlItens, connection, transaction))
            {
                cmdItens.Parameters.AddWithValue("@id_venda", vendaId);
                using (MySqlDataReader reader = cmdItens.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itensAntigos.Add(new ProdutoVenda
                        {
                            IdProduto = reader.GetInt32("id_produto"),
                            Quantidade = reader.GetInt32("quantidade")
                        });
                    }
                }
            }

            if (adicionar)
            {
                string sqlEstoque = "UPDATE Produto SET qtd_estoque = qtd_estoque + @quantidade WHERE id = @id_produto;";

                foreach (var item in itensAntigos)
                {
                    using (MySqlCommand cmdEstoque = new MySqlCommand(sqlEstoque, connection, transaction))
                    {
                        cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                        cmdEstoque.Parameters.AddWithValue("@id_produto", item.IdProduto);

                        if (cmdEstoque.ExecuteNonQuery() == 0)
                        {
                            throw new Exception($"Falha ao reverter estoque para o Produto ID {item.IdProduto}.");
                        }
                    }
                }
            }
        }
    }
}
