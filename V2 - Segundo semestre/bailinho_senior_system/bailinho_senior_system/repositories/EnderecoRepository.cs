using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using bailinho_senior_system.models;
using bailinho_senior_system.config;

namespace bailinho_senior_system.repositories
{
    public class EnderecoRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public List<Endereco> GetEnderecos()
        {
            var enderecos = new List<Endereco>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = "SELECT id, cep, logradouro, bairro, cidade, numero, estado, complemento FROM Endereco;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var endereco = new Endereco
                            {
                                Id = reader.GetInt32("id"),
                                Cep = reader.GetString("cep"),
                                Logradouro = reader.GetString("logradouro"),
                                Bairro = reader.GetString("bairro"),
                                Cidade = reader.GetString("cidade"),
                                Numero = reader.GetString("numero"),
                                Estado = reader.GetString("estado"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento")
                            };
                            enderecos.Add(endereco);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            return enderecos;
        }

        public Endereco GetEndereco(int id)
        {
            Endereco endereco = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = "SELECT id, cep, logradouro, bairro, cidade, numero, estado, complemento FROM Endereco WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                endereco = new Endereco
                                {
                                    Id = reader.GetInt32("id"),
                                    Cep = reader.GetString("cep"),
                                    Logradouro = reader.GetString("logradouro"),
                                    Bairro = reader.GetString("bairro"),
                                    Cidade = reader.GetString("cidade"),
                                    Numero = reader.GetString("numero"),
                                    Estado = reader.GetString("estado"),
                                    Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento")
                                };
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
            return endereco;
        }

        public void CreateEndereco(Endereco endereco)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"
                        INSERT INTO Endereco (cep, logradouro, bairro, cidade, numero, estado, complemento) 
                        VALUES (@cep, @logradouro, @bairro, @cidade, @numero, @estado, @complemento); 
                        SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cep", endereco.Cep);
                        command.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                        command.Parameters.AddWithValue("@bairro", endereco.Bairro);
                        command.Parameters.AddWithValue("@cidade", endereco.Cidade);
                        command.Parameters.AddWithValue("@numero", endereco.Numero);
                        command.Parameters.AddWithValue("@estado", endereco.Estado);
                        command.Parameters.AddWithValue("@complemento", string.IsNullOrEmpty(endereco.Complemento) ? (object)DBNull.Value : endereco.Complemento);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            endereco.Id = Convert.ToInt32(result);
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

        public void UpdateEndereco(Endereco endereco)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"
                        UPDATE Endereco 
                        SET cep = @cep, logradouro = @logradouro, bairro = @bairro, cidade = @cidade, 
                            numero = @numero, estado = @estado, complemento = @complemento 
                        WHERE id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", endereco.Id);
                        command.Parameters.AddWithValue("@cep", endereco.Cep);
                        command.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                        command.Parameters.AddWithValue("@bairro", endereco.Bairro);
                        command.Parameters.AddWithValue("@cidade", endereco.Cidade);
                        command.Parameters.AddWithValue("@numero", endereco.Numero);
                        command.Parameters.AddWithValue("@estado", endereco.Estado);
                        command.Parameters.AddWithValue("@complemento", string.IsNullOrEmpty(endereco.Complemento) ? (object)DBNull.Value : endereco.Complemento);

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

        public void DeleteEndereco(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM Endereco WHERE id = @id;";

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