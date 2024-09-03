namespace AppCaixa.view;

using AppCaixa.DAO;
using AppCaixa.Models;

public static class CaixaView
{
    public static void menu()
    {
        CaixaDAO caixaDao = new CaixaDAO();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir Caixa");
            Console.WriteLine("2. Listar Caixa");
            Console.WriteLine("3. Atualizar Caixa");
            Console.WriteLine("4. Deletar Caixa");
            Console.WriteLine("5. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Inserir(caixaDao);
                    break;
                case "2":
                    Listar(caixaDao);
                    break;
                case "3":
                    Atualizar(caixaDao);
                    break;
                case "4":
                    Deletar(caixaDao);
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }
        }
    }

    public static void Inserir(CaixaDAO caixaDao)
    {
        Caixa caixa = new Caixa();
        FuncionarioDAO Funcionario = new FuncionarioDAO();

        Console.Write("Saldo Inicial: ");
        caixa.saldo_inicial = decimal.Parse(Console.ReadLine());

        Console.Write("Total Entradas: ");
        caixa.total_entradas = int.Parse(Console.ReadLine());

        Console.Write("Total Saídas: ");
        caixa.total_saidas = int.Parse(Console.ReadLine());

        Console.Write("Status do Caixa: ");
        caixa.status_caixa = Console.ReadLine();

        FuncionarioView.Listar(Funcionario);
        Console.Write("ID do Funcionário: ");
        caixa.func_fk = int.Parse(Console.ReadLine());

        Console.Write("Saldo Total: ");
        caixa.saldo_total = decimal.Parse(Console.ReadLine());

        caixaDao.Insert(caixa);
    }

    public static void Listar(CaixaDAO caixaDao)
    {
        List<Caixa> caixas = caixaDao.Listar();

        foreach (var caixa in caixas)
        {
            Console.WriteLine(
                $"ID: {caixa.id}, Saldo Inicial: {caixa.saldo_inicial}, Total Entradas: {caixa.total_entradas}, Total Saídas: {caixa.total_saidas}, Status: {caixa.status_caixa}, Funcionário: {caixa.funcionario_nome}, Saldo Total: {caixa.saldo_total}");
        }
    }

    public static void Atualizar(CaixaDAO caixaDao)
    {
        Caixa caixa = new Caixa();

        Console.Write("ID do Caixa a ser atualizado: ");
        caixa.id = int.Parse(Console.ReadLine());

        Console.WriteLine("Deseja atualizar o Saldo Inicial? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Saldo Inicial: ");
            caixa.saldo_inicial = decimal.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o Total Entradas? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Total Entradas: ");
            caixa.total_entradas = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o Total Saídas? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Total Saídas: ");
            caixa.total_saidas = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o Status do Caixa? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Status do Caixa: ");
            caixa.status_caixa = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o ID do Funcionário? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo ID do Funcionário: ");
            caixa.func_fk = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o Saldo Total? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Saldo Total: ");
            caixa.saldo_total = decimal.Parse(Console.ReadLine());
        }

        caixaDao.Update(caixa);
    }

    public static void Deletar(CaixaDAO caixaDao)
    {
        Console.Write("ID do Caixa a ser deletado: ");
        int id = int.Parse(Console.ReadLine());

        Caixa caixa = new Caixa { id = id };

        caixaDao.Delete(caixa);
    }
}