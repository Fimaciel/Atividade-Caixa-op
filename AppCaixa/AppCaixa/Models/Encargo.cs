namespace AppCaixa.Models;

public class Encargo
{
    public int id { get; set; }
    public string descricao { get; set; }
    public decimal valor { get; set; }
    public int id_dispositivo_fk { get; set; }
    public int recebimento_fk { get; set; }
}
