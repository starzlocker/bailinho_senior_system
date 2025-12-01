using bailinho_senior_system.models;
using bailinho_senior_system.config;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace bailinho_senior_system.repositories
{
    internal class FornecedorRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public List<Fornecedor> GetFornecedores()
        {
            var fornecedores = new List<Fornecedor>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT id, nome, cnpj, email, telefone FROM Fornecedor;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Fornecedor fornecedor = new Fornecedor(
                                    reader.GetString("cnpj"),
                                    reader.GetString("nome"),
                                    reader.GetString("email"),
                                    reader.GetString("telefone"),
                                    reader.GetInt32("id")
                                );

                                fornecedores.Add(fornecedor);
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

            return fornecedores;
        }


        public Fornecedor GetFornecedor(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "SELECT id, nome, cnpj, email, telefone FROM Fornecedor WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Fornecedor fornecedor = new Fornecedor(
                                    reader.GetString("cnpj"),
                                    reader.GetString("nome"),
                                    reader.GetString("email"),
                                    reader.GetString("telefone"),
                                    reader.GetInt32("id")
                                );

                                return fornecedor;
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

        public void CreateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "INSERT INTO Fornecedor " +
                                 "(nome, cnpj, email, telefone) " +
                                 "VALUES (@nome, @cnpj, @email, @telefone); " +
                                 "SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", fornecedor.Nome);
                        command.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                        command.Parameters.AddWithValue("@email", fornecedor.Email);
                        command.Parameters.AddWithValue("@telefone", fornecedor.Telefone);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            fornecedor.Id = Convert.ToInt32(result);
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

        public void UpdateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "UPDATE Fornecedor " +
                                 "SET nome = @nome, cnpj = @cnpj, email = @email, telefone = @telefone " +
                                 "WHERE id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", fornecedor.Id);
                        command.Parameters.AddWithValue("@nome", fornecedor.Nome);
                        command.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                        command.Parameters.AddWithValue("@email", fornecedor.Email);
                        command.Parameters.AddWithValue("@telefone", fornecedor.Telefone);

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

        public void DeleteFornecedor(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();


                    string sql = "DELETE FROM Fornecedor WHERE id = @id;";

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
