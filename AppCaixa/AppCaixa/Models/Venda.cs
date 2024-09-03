namespace AppCaixa.Models;

public class Venda
{
    public int id { get; set; }
    public DateTime data_venda { get; set; }
    public TimeOnly hora { get; set; }
    public decimal valor_total { get; set; }
    public decimal desconto { get; set; }
    public decimal valor_final { get; set; }
    public string tipo { get; set; }
    public int? id_cliente_fk  { get; set; }
}