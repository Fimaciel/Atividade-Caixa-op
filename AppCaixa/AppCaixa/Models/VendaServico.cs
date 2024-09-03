namespace AppCaixa.Models;

public class VendaServico
{
    public int id { get; set; }
    public int venda_fk { get; set; }
    public int servico_fk { get; set; }
    public decimal valor_unitario { get; set; }
    public int quantidade { get; set; }
}