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
        try
        {
            var sqlList = "SELECT * FROM clientes";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Listas;
    }
}
