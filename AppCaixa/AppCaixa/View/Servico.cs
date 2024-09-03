using AppCaixa.DAO;
using AppCaixa.Models;

namespace AppCaixa.view;

public static class ServicoView
{
    public static void menu()
    {
        ServicoDAO servicoDao = new ServicoDAO();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir Serviço");
            Console.WriteLine("2. Listar Serviços");
            Console.WriteLine("3. Atualizar Serviço");
            Console.WriteLine("4. Deletar Serviço");
            Console.WriteLine("5. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Inserir(servicoDao);
                    break;
                case "2":
                    Listar(servicoDao);
                    break;
                case "3":
                    Atualizar(servicoDao);
                    break;
                case "4":
                    Deletar(servicoDao);
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

    public static void Inserir(ServicoDAO servicoDao)
    {
        Servico servico = new Servico();

        Console.Write("Valor: ");
        servico.valor = decimal.Parse(Console.ReadLine());

        Console.Write("Descrição: ");
        servico.descricao = Console.ReadLine();

        Console.Write("Tempo (HH:mm): ");
        servico.tempo = TimeOnly.Parse(Console.ReadLine());

        servicoDao.Insert(servico);
    }

    public static void Listar(ServicoDAO servicoDao)
    {
        List<Servico> servicos = servicoDao.Listar();

        foreach (var s in servicos)
        {
            string tempoFormatado = s.tempo.ToString("HH:mm");
            Console.WriteLine(
                $"ID: {s.id}, Valor: {s.valor}, Descrição: {s.descricao}, Tempo: {tempoFormatado}");
        }
    }

    public static void Atualizar(ServicoDAO servicoDao)
    {
        Servico servico = new Servico();

        Console.Write("ID do Serviço a ser atualizado: ");
        servico.id = int.Parse(Console.ReadLine());

        Console.WriteLine("Deseja atualizar o Valor? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Valor: ");
            servico.valor = decimal.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar a Descrição? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Nova Descrição: ");
            servico.descricao = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o Tempo? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Tempo (HH:mm): ");
            servico.tempo = TimeOnly.Parse(Console.ReadLine());
        }

        servicoDao.Update(servico);
    }

    public static void Deletar(ServicoDAO servicoDao)
    {
        Console.Write("ID do Serviço a ser deletado: ");
        int id = int.Parse(Console.ReadLine());

        Servico servico = new Servico { id = id };

        servicoDao.Delete(servico);
    }
}
