namespace AppCaixa.Models;

public class Recebimento
{
    public int id { get; set; }
    public decimal valor { get; set; }
    public DateTime data_vencimento { get; set; }
    public DateTime data_pagamento { get; set; }
    public string status_recebimento { get; set; }
    public int caixa_fk { get; set; }
    public int venda_fk { get; set; }
}