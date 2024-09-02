using AppCaixa.database;
using MySql.Data.MySqlClient;

namespace AppCaixa.DAO;

using Models;

public class ClienteDAO
{
    public void Insert(Cliente cliente)
    {
        try
        {
            string dataNasc = cliente.data_nasc.ToString("yyyy-MM-dd");
            string insertSql = "INSERT INTO clientes (nome, cpf, email, telefone, data_nasc)" +
                               "VALUES(@nome, @cpf, @email, @telefone, @dataNasc)";

            MySqlCommand command = new MySqlCommand(insertSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@nome", cliente.nome);
            command.Parameters.AddWithValue("@cpf", cliente.cpf);
            command.Parameters.AddWithValue("@email", cliente.email);
            command.Parameters.AddWithValue("@telefone", cliente.telefone);
            command.Parameters.AddWithValue("@dataNasc", dataNasc);
            command.ExecuteNonQuery();
            Console.WriteLine("salvo");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Delete(Cliente cliente)
    {
        try
        {
            string deleteSql = "DELETE FROM clientes WHERE id = @id_cliente";
            MySqlCommand command = new MySqlCommand(deleteSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@id_cliente", cliente.id);
            command.ExecuteNonQuery();
            Console.WriteLine("Cliente excluido com sucesso!");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

     public List<Cliente> Listar()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            try
            {
                string sqlList = "SELECT * FROM clientes";
                MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        id = reader.GetInt32("id"),
                        nome = reader.GetString("nome"),
                        cpf = reader.GetString("cpf"),
                        email = reader.GetString("email"),
                        telefone = reader.GetString("telefone"),
                        data_nasc = reader.GetDateTime("data_nasc")
                    };
                    listaClientes.Add(cliente);
                }
                reader.Close();
                Conexao.FecharConexao();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return listaClientes;
        }

        public void Update(Cliente cliente)
        {
            try
            {
                List<string> updates = new List<string>();
                MySqlCommand command = new MySqlCommand();
                command.Connection = Conexao.Conectar();

                if (!string.IsNullOrEmpty(cliente.nome))
                {
                    updates.Add("nome = @nome");
                    command.Parameters.AddWithValue("@nome", cliente.nome);
                }
                if (!string.IsNullOrEmpty(cliente.cpf))
                {
                    updates.Add("cpf = @cpf");
                    command.Parameters.AddWithValue("@cpf", cliente.cpf);
                }
                if (!string.IsNullOrEmpty(cliente.email))
                {
                    updates.Add("email = @email");
                    command.Parameters.AddWithValue("@email", cliente.email);
                }
                if (!string.IsNullOrEmpty(cliente.telefone))
                {
                    updates.Add("telefone = @telefone");
                    command.Parameters.AddWithValue("@telefone", cliente.telefone);
                }
                if (cliente.data_nasc != DateTime.MinValue)
                {
                    updates.Add("data_nasc = @dataNasc");
                    command.Parameters.AddWithValue("@dataNasc", cliente.data_nasc.ToString("yyyy-MM-dd"));
                }

                if (updates.Count > 0)
                {
                    string updateSql = $"UPDATE clientes SET {string.Join(", ", updates)} WHERE id = @id";
                    command.CommandText = updateSql;
                    command.Parameters.AddWithValue("@id", cliente.id);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Cliente atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Nenhuma atualização realizada.");
                }

                Conexao.FecharConexao();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

}
