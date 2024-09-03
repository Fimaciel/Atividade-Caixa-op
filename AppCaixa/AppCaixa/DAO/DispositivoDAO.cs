using AppCaixa.database;
using AppCaixa.Models;
using MySql.Data.MySqlClient;

namespace AppCaixa.DAO
{
    public class DispositivoDAO
    {
        public void Insert(Dispositivo dispositivo)
        {
            try
            {
                string insertSql = "INSERT INTO dispositivos (aliquota, descricao, status_dispo)" +
                                   "VALUES(@aliquota, @descricao, @status_dispo)";

                MySqlCommand command = new MySqlCommand(insertSql, Conexao.Conectar());
                command.Parameters.AddWithValue("@aliquota", dispositivo.aliquota);
                command.Parameters.AddWithValue("@descricao", dispositivo.descricao);
                command.Parameters.AddWithValue("@status_dispo", dispositivo.status_dispo);
                command.ExecuteNonQuery();
                Console.WriteLine("Dispositivo salvo com sucesso!");
                Conexao.FecharConexao();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Delete(Dispositivo dispositivo)
        {
            try
            {
                string deleteSql = "DELETE FROM dispositivos WHERE id = @id";
                MySqlCommand command = new MySqlCommand(deleteSql, Conexao.Conectar());
                command.Parameters.AddWithValue("@id", dispositivo.id);
                command.ExecuteNonQuery();
                Console.WriteLine("Dispositivo excluído com sucesso!");
                Conexao.FecharConexao();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Dispositivo> Listar()
        {
            List<Dispositivo> listaDispositivos = new List<Dispositivo>();

            try
            {
                string sqlList = "SELECT * FROM dispositivos";
                MySqlCommand command = new MySqlCommand(sqlList, Conexao.Conectar());
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Dispositivo dispositivo = new Dispositivo
                    {
                        id = reader.GetInt32("id"),
                        aliquota = reader.GetDouble("aliquota"),
                        descricao = reader.GetString("descricao"),
                        status_dispo = reader.GetBoolean("status_dispo")
                    };
                    listaDispositivos.Add(dispositivo);
                }
                reader.Close();
                Conexao.FecharConexao();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return listaDispositivos;
        }

        public void Update(Dispositivo dispositivo)
        {
            try
            {
                List<string> updates = new List<string>();
                MySqlCommand command = new MySqlCommand();
                command.Connection = Conexao.Conectar();

                if (dispositivo.aliquota != 0)
                {
                    updates.Add("aliquota = @aliquota");
                    command.Parameters.AddWithValue("@aliquota", dispositivo.aliquota);
                }

                if (!string.IsNullOrEmpty(dispositivo.descricao))
                {
                    updates.Add("descricao = @descricao");
                    command.Parameters.AddWithValue("@descricao", dispositivo.descricao);
                }

                updates.Add("status_dispo = @status_dispo");
                command.Parameters.AddWithValue("@status_dispo", dispositivo.status_dispo);

                if (updates.Count > 0)
                {
                    string updateSql = $"UPDATE dispositivos SET {string.Join(", ", updates)} WHERE id = @id";
                    command.CommandText = updateSql;
                    command.Parameters.AddWithValue("@id", dispositivo.id);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Dispositivo atualizado com sucesso!");
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
}
