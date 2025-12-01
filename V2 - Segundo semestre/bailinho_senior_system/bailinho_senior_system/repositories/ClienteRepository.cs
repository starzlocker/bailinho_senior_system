using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using bailinho_senior_system.models;
using bailinho_senior_system.config;
using System.Windows.Forms;

namespace bailinho_senior_system.repositories
{
    public class ClienteRepository
    {
        private string ConnectionString => DatabaseConfig.ConnectionString;

        public List<Cliente> GetClientes()
        {
            var clientes = new List<Cliente>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT id, cpf, genero, dt_nascimento, nome, telefone FROM Cliente;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Cliente(
                                reader.GetInt32("id"),
                                reader.GetString("nome"),
                                reader.GetDateTime("dt_nascimento"),
                                reader.GetChar("genero"),
                                reader.GetString("cpf"),
                                reader.GetString("telefone")
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar todos os clientes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("Erro ao buscar todos os clientes.", ex);
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
                    string sql = "SELECT id, cpf, genero, dt_nascimento, nome, telefone FROM Cliente WHERE id = @id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente = new Cliente(
                                    reader.GetInt32("id"),
                                    reader.GetString("nome"),
                                    reader.GetDateTime("dt_nascimento"),
                                    reader.GetChar("genero"),
                                    reader.GetString("cpf"),
                                    reader.GetString("telefone")
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar cliente por ID.", ex);
            }
            return cliente;
        }


        public void CreateCliente(Cliente cliente)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"
                        INSERT INTO Cliente (cpf, genero, dt_nascimento, nome, telefone) 
                        VALUES (@cpf, @genero, @dt_nascimento, @nome, @telefone); 
                        SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cpf", cliente.Cpf);
                        command.Parameters.AddWithValue("@genero", cliente.Genero);
                        command.Parameters.AddWithValue("@dt_nascimento", cliente.DtNascimento.Date);
                        command.Parameters.AddWithValue("@nome", cliente.Nome);
                        command.Parameters.AddWithValue("@telefone", cliente.Telefone);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            cliente.Id = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar novo cliente.", ex);
            }
        }

        public void UpdateCliente(Cliente cliente)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = @"
                        UPDATE Cliente 
                        SET cpf = @cpf, genero = @genero, dt_nascimento = @dt_nascimento, 
                            nome = @nome, telefone = @telefone 
                        WHERE id = @id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", cliente.Id);
                        command.Parameters.AddWithValue("@cpf", cliente.Cpf);
                        command.Parameters.AddWithValue("@genero", cliente.Genero);
                        command.Parameters.AddWithValue("@dt_nascimento", cliente.DtNascimento.Date);
                        command.Parameters.AddWithValue("@nome", cliente.Nome);
                        command.Parameters.AddWithValue("@telefone", cliente.Telefone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar cliente.", ex);
            }
        }

        public void DeleteCliente(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Cliente WHERE id = @id;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir cliente. Verifique se há vendas associadas.", ex);
            }
        }
    }
}