using System;
using System.Collections.Generic;
using bailinho_senior_system.models;
using bailinho_senior_system.config;
using MySql.Data.MySqlClient;

namespace bailinho_senior_system.repositories
{
    public class ProdutoRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public List<Produto> GetProdutos()
        {
            var products = new List<Produto>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql =
                        $"SELECT tp.id, tp.nome, tp.descricao, tp.qtd_estoque, tp.preco, tp.id_categoria, tc.nome AS categoria " +
                        $"FROM Produto tp " +
                        $"LEFT JOIN Categoria tc ON tp.id_categoria = tc.id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto(
                                reader.GetString("nome"),
                                reader.GetString("descricao"),
                                reader.GetInt32("qtd_estoque"),
                                reader.GetDecimal("preco")
                            );

                            produto.Id = reader.GetInt32("id");
                            produto.IdCategoria = reader.GetInt32("id_categoria");
                            produto.Categoria = reader.GetString("categoria");

                            products.Add(produto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            return products;
        }

        public Produto GetProduto(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql =
                        $"SELECT tp.id, tp.nome, tp.descricao, tp.qtd_estoque, tp.preco, tp.id_categoria, tc.nome AS categoria " +
                        $"FROM Produto tp " +
                        $"LEFT JOIN Categoria tc ON tp.id_categoria = tc.id " +
                        $"WHERE tp.id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var produto = new Produto(
                                    reader.GetString("nome"),
                                    reader.GetString("descricao"),
                                    reader.GetInt32("qtd_estoque"),
                                    reader.GetDecimal("preco")
                                );

                                produto.Id = reader.GetInt32("id");
                                produto.IdCategoria = reader.GetInt32("id_categoria");
                                produto.Categoria = reader.GetString("categoria");

                                return produto;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            return null;
        }

        public void CreateProduto(Produto produto)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql =
                        "INSERT INTO Produto (nome, descricao, qtd_estoque, preco, id_categoria) " +
                        "VALUES (@nome, @descricao, @qtd_estoque, @preco, @id_categoria); " +
                        "SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", produto.Nome);
                        command.Parameters.AddWithValue("@descricao", produto.Descricao);
                        command.Parameters.AddWithValue("@qtd_estoque", produto.QtdEstoque);
                        command.Parameters.AddWithValue("@preco", produto.Preco);
                        command.Parameters.AddWithValue("@id_categoria", produto.IdCategoria);

                        object result = command.ExecuteScalar();
                        if (result != null)
                            produto.Id = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
        }

        public void UpdateProduto(Produto produto)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql =
                        "UPDATE Produto SET nome=@nome, descricao=@descricao, qtd_estoque=@qtd_estoque, preco=@preco, id_categoria=@id_categoria " +
                        "WHERE id=@id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", produto.Id);
                        command.Parameters.AddWithValue("@nome", produto.Nome);
                        command.Parameters.AddWithValue("@descricao", produto.Descricao);
                        command.Parameters.AddWithValue("@qtd_estoque", produto.QtdEstoque);
                        command.Parameters.AddWithValue("@preco", produto.Preco);
                        command.Parameters.AddWithValue("@id_categoria", produto.IdCategoria);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
        }

        public void DeleteProduto(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM Produto WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
        }
    }
}
