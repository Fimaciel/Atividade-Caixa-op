using AppCaixa.DAO;
using AppCaixa.database;
using AppCaixa.Models;
try
{
    Cliente c = new Cliente();

    c.nome = "nome";
    c.data_nasc = new DateOnly(2003, 09, 20);
    c.cpf = "1234542013";
    c.email = "teste@gmail.com";
    c.telefone = "1234123454";

    ClienteDAO cd = new ClienteDAO();
    cd.Insert(c);
    cd.Delete(c);
}
catch (Exception e)
{
    Console.WriteLine(e);
}