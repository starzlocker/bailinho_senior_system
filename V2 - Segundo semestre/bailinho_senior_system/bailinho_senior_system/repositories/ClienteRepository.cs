using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using bailinho_senior_system.models;
using bailinho_senior_system.config;

namespace bailinho_senior_system.repositories
{
    public class ClienteRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public int createCliente(Cliente cliente)
        {
            int newId = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO Cliente (
                            nome,
                            telefone,
                            cpf,
                            genero,
                            dt_nascimento
                        )
                        VALUES (
                            @nome,
                            @telefone,
                            @cpf,
                            @genero,
                            @dt_nascimento
                        );

                        SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", cliente.Nome);
                        command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                        command.Parameters.AddWithValue("@cpf", cliente.Cpf);
                        command.Parameters.AddWithValue("@genero", cliente.Genero);
                        command.Parameters.AddWithValue("@dt_nascimento", cliente.DtNascimento);
                        newId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar cliente: " + e.Message);
            }

            return newId;
        }

        public List<Cliente> GetClientes()
        {
            var clientes = new List<Cliente>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"SELECT
                            id,
                            nome,
                            telefone,
                            cpf,
                            genero,
                            dt_nascimento
                        FROM Cliente;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Telefone = reader.GetString("telefone"),
                                Cpf = reader.GetString("cpf"),
                                Genero = reader.GetString("genero"),
                                DtNascimento = reader.GetDateTime("dt_nascimento")
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter clientes: " + e.Message);
            }

            return clientes;
        }

        public Cliente GetCliente(int id)
        {
            Cliente cliente = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"SELECT
                            id,
                            nome,
                            telefone,
                            cpf,
                            genero,
                            dt_nascimento
                        FROM Cliente
                        WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente = new Cliente
                                {
                                    Id = reader.GetInt32("id"),
                                    Nome = reader.GetString("nome"),
                                    Telefone = reader.GetString("telefone"),
                                    Cpf = reader.GetString("cpf"),
                                    Genero = reader.GetString("genero"),
                                    DtNascimento = reader.GetDateTime("dt_nascimento")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter cliente: " + e.Message);
            }

            return cliente;
        }

        public void updateCliente(Cliente cliente)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"UPDATE Cliente
                        SET
                            nome=@nome,
                            telefone=@telefone,
                            cpf=@cpf,
                            genero=@genero,
                            dt_nascimento=@dt_nascimento
                        WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", cliente.Nome);
                        command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                        command.Parameters.AddWithValue("@cpf", cliente.Cpf);
                        command.Parameters.AddWithValue("@genero", cliente.Genero);
                        command.Parameters.AddWithValue("@dt_nascimento", cliente.DtNascimento);
                        command.Parameters.AddWithValue("@id", cliente.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar cliente: " + e.Message);
            }
        }

        public void deleteCliente(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM Cliente WHERE id=@id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao deletar cliente: " + e.Message);
            }
        }
    }
}
