using AppCaixa.database;
using AppCaixa.Models;
using MySql.Data.MySqlClient;

namespace AppCaixa.DAO;

public class FornecedorDAO
{
    public void Insert(Fornecedor fornecedor)
    {
        try
        {
            string insertSql =
                "INSERT INTO fornecedores (razao_social, nome_fantasia, ativo, atividade_economica, telefone, email)" +
                "VALUES(@razaoSocial, @fantasia, @ativo, @atividadeEconomica, @telefone, @email)";

            MySqlCommand command = new MySqlCommand(insertSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@razaoSocial", fornecedor.razao_social);
            command.Parameters.AddWithValue("@fantasia", fornecedor.nome_fantasia);
            command.Parameters.AddWithValue("@email", fornecedor.email);
            command.Parameters.AddWithValue("@telefone", fornecedor.telefone);
            command.Parameters.AddWithValue("@atividadeEconomica", fornecedor.atividade_economica);
            command.Parameters.AddWithValue("@ativo", fornecedor.ativo);
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

    public void Delete(Fornecedor fornecedor)
    {
        try
        {
            string deleteSql = "DELETE FROM fornecedores WHERE id = @id";
            MySqlCommand command = new MySqlCommand(deleteSql, Conexao.Conectar());
            command.Parameters.AddWithValue("@id", fornecedor.id);
            command.ExecuteNonQuery();
            Console.WriteLine("Fornecedor excluido com sucesso!");
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Fornecedor> Listar()
    {
        List<Fornecedor> listaFornecedor = new List<Fornecedor>();

        try
        {
            string sqlList = "SELECT * FROM fornecedores";
            MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Fornecedor fornecedor = new Fornecedor
                {
                    id = reader.GetInt32("id"),
                    razao_social = reader.GetString("razao_social"),
                    nome_fantasia = reader.GetString("nome_fantasia"),
                    email = reader.GetString("email"),
                    telefone = reader.GetString("telefone"),
                    atividade_economica = reader.GetString("atividade_economica"),
                    ativo = reader.GetBoolean("ativo")
                };
                listaFornecedor.Add(fornecedor);
            }

            reader.Close();
            Conexao.FecharConexao();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return listaFornecedor;
    }

    public void Update(Fornecedor fornecedor)
    {
        try
        {
            List<string> updates = new List<string>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = Conexao.Conectar();

            if (!string.IsNullOrEmpty(fornecedor.razao_social))
            {
                updates.Add("razao_social = @razao_social");
                command.Parameters.AddWithValue("@razao_social", fornecedor.razao_social);
            }

            if (!string.IsNullOrEmpty(fornecedor.nome_fantasia))
            {
                updates.Add("nome_fantasia = @nome_fantasia");
                command.Parameters.AddWithValue("@nome_fantasia", fornecedor.nome_fantasia);
            }

            if (!string.IsNullOrEmpty(fornecedor.email))
            {
                updates.Add("email = @email");
                command.Parameters.AddWithValue("@email", fornecedor.email);
            }

            if (!string.IsNullOrEmpty(fornecedor.telefone))
            {
                updates.Add("telefone = @telefone");
                command.Parameters.AddWithValue("@telefone", fornecedor.telefone);
            }

            if (!string.IsNullOrEmpty(fornecedor.atividade_economica))
            {
                updates.Add("atividade_economica = @atividade_economica");
                command.Parameters.AddWithValue("@atividade_economica", fornecedor.atividade_economica);
            }

            if (fornecedor.ativo != null)
            {
                updates.Add("ativo = @ativo");
                command.Parameters.AddWithValue("@ativo", fornecedor.ativo);
            }

            if (updates.Count > 0)
            {
                string updateSql = $"UPDATE fornecedores SET {string.Join(", ", updates)} WHERE id = @id";
                command.CommandText = updateSql;
                command.Parameters.AddWithValue("@id", fornecedor.id);
                command.ExecuteNonQuery();
                Console.WriteLine("Fornecedor atualizado com sucesso!");
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