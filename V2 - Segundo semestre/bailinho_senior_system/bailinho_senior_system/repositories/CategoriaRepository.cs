using bailinho_senior_system.models;
using bailinho_senior_system.config;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace bailinho_senior_system.repositories
{
    internal class CategoriaRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public List<Categoria> GetCategorias()
        {
            var categorias = new List<Categoria>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT id, nome, descricao FROM Categoria;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categoria categoria = new Categoria(
                                    reader.GetInt32("id"),
                                    reader.GetString("nome"),
                                    reader.GetString("descricao")
                                );

                                categorias.Add(categoria);
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

            return categorias;
        }


        public Categoria GetCategoria(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT id, nome, descricao FROM Categoria WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Categoria categoria = new Categoria(
                                    reader.GetInt32("id"),
                                    reader.GetString("nome"),
                                    reader.GetString("descricao")
                                );

                                return categoria;
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

        public void CreateCategoria(Categoria categoria)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "INSERT INTO Categoria " +
                                 "(nome, descricao) " +
                                 "VALUES (@nome, @descricao); " +
                                 "SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", categoria.Nome);
                        command.Parameters.AddWithValue("@descricao", categoria.Descricao);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            categoria.Id = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;

            }
        }

        public void UpdateCategoria(Categoria categoria)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "UPDATE Categoria " +
                                 "set nome = @nome, descricao = @descricao " +
                                 "WHERE id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", categoria.Id);
                        command.Parameters.AddWithValue("@nome", categoria.Nome);
                        command.Parameters.AddWithValue("@descricao", categoria.Descricao);

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

        public void DeleteCategoria(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "DELETE FROM Categoria WHERE id = @id;";

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
