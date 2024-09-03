using AppCaixa.database;
using AppCaixa.Models;
using MySql.Data.MySqlClient;

namespace AppCaixa.DAO;

public class FuncionarioDAO
{
    public void Insert(Funcionario funcionario)
    {
        try
        {
            string insertSql = "INSERT INTO funcionarios (nome, cpf)" +
                               "VALUES(@nome, @cpf)";

            MySqlCommand command = new MySqlCommand(insertSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@nome", funcionario.nome);
            command.Parameters.AddWithValue("@cpf", funcionario.cpf);
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

    public void Delete(Funcionario funcionario)
    {
        try
        {
            string deleteSql = "DELETE FROM funcionarios WHERE id = @id";
            MySqlCommand command = new MySqlCommand(deleteSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@id", funcionario.id);
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

     public List<Funcionario> Listar()
        {
            List<Funcionario> listaFuncionario= new List<Funcionario>();

            try
            {
                string sqlList = "SELECT * FROM funcionarios";
                MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Funcionario funcionario = new Funcionario
                    {
                        id = reader.GetInt32("id"),
                        nome = reader.GetString("nome"),
                        cpf = reader.GetString("cpf"),
                    };
                    listaFuncionario.Add(funcionario);
                }
                reader.Close();
                Conexao.FecharConexao();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return listaFuncionario;
        }

        public void Update(Funcionario funcionario)
        {
            try
            {
                List<string> updates = new List<string>();
                MySqlCommand command = new MySqlCommand();
                command.Connection = Conexao.Conectar();

                if (!string.IsNullOrEmpty(funcionario.nome))
                {
                    updates.Add("nome = @nome");
                    command.Parameters.AddWithValue("@nome", funcionario.nome);
                }
                if (!string.IsNullOrEmpty(funcionario.cpf))
                {
                    updates.Add("cpf = @cpf");
                    command.Parameters.AddWithValue("@cpf", funcionario.cpf);
                }
                
                if (updates.Count > 0)
                {
                    string updateSql = $"UPDATE funcionarios SET {string.Join(", ", updates)} WHERE id = @id";
                    command.CommandText = updateSql;
                    command.Parameters.AddWithValue("@id", funcionario.id);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Funcionário atualizado com sucesso!");
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
