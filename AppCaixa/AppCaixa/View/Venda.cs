using AppCaixa.DAO;
using AppCaixa.Models;

namespace AppCaixa.view;

public static class VendaView
{
    public static void Menu()
    {
        VendaDAO vendaDao = new VendaDAO();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir Venda");
            Console.WriteLine("2. Listar Vendas");
            Console.WriteLine("3. Atualizar Venda");
            Console.WriteLine("4. Deletar Venda");
            Console.WriteLine("5. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Inserir(vendaDao);
                    break;
                case "2":
                    Listar(vendaDao);
                    break;
                case "3":
                    Atualizar(vendaDao);
                    break;
                case "4":
                    Deletar(vendaDao);
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

    public static void Inserir(VendaDAO vendaDao)
    {
        Venda venda = new Venda();

        Console.Write("Data da Venda (yyyy-MM-dd): ");
        venda.data_venda = DateTime.Parse(Console.ReadLine());

        Console.Write("Hora da Venda (HH:mm): ");
        venda.hora = TimeOnly.Parse(Console.ReadLine());

        Console.Write("Valor Total: ");
        venda.valor_total = decimal.Parse(Console.ReadLine());

        Console.Write("Desconto: ");
        venda.desconto = decimal.Parse(Console.ReadLine());

        Console.Write("Valor Final: ");
        venda.valor_final = decimal.Parse(Console.ReadLine());

        Console.Write("Tipo: ");
        venda.tipo = Console.ReadLine();

        Console.Write("ID do Cliente (ou deixe em branco para NULL): ");
        string idClienteInput = Console.ReadLine();
        venda.id_cliente_fk = string.IsNullOrEmpty(idClienteInput) ? (int?)null : int.Parse(idClienteInput);

        List<VendaServico> vendaServicos = new List<VendaServico>();

        bool addMoreServices = true;
        while (addMoreServices)
        {
            VendaServico vendaServico = new VendaServico();

            Console.Write("ID do Serviço: ");
            vendaServico.servico_fk = int.Parse(Console.ReadLine());

            Console.Write("Valor Unitário: ");
            vendaServico.valor_unitario = decimal.Parse(Console.ReadLine());

            Console.Write("Quantidade: ");
            vendaServico.quantidade = int.Parse(Console.ReadLine());

            vendaServicos.Add(vendaServico);

            Console.WriteLine("Deseja adicionar outro serviço? (S/N)");
            addMoreServices = Console.ReadLine().Trim().ToUpper() == "S";
        }

        vendaDao.Insert(venda, vendaServicos);
    }

    public static void Listar(VendaDAO vendaDao)
    {
        List<Venda> vendas = vendaDao.Listar();

        foreach (var v in vendas)
        {
            Console.WriteLine(
                $"ID: {v.id}, Data: {v.data_venda:yyyy-MM-dd}, Hora: {v.hora:HH:mm}, Valor Total: {v.valor_total}, " +
                $"Desconto: {v.desconto}, Valor Final: {v.valor_final}, Tipo: {v.tipo}, ID Cliente: {v.id_cliente_fk}");
        }
    }

    public static void Atualizar(VendaDAO vendaDao)
    {
        Venda venda = new Venda();

        Console.Write("ID da Venda a ser atualizada: ");
        venda.id = int.Parse(Console.ReadLine());

        Console.WriteLine("Deseja atualizar a Data da Venda? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Nova Data da Venda (yyyy-MM-dd): ");
            venda.data_venda = DateTime.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar a Hora da Venda? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Nova Hora da Venda (HH:mm): ");
            venda.hora = TimeOnly.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o Valor Total? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Valor Total: ");
            venda.valor_total = decimal.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o Desconto? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Desconto: ");
            venda.desconto = decimal.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o Valor Final? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Valor Final: ");
            venda.valor_final = decimal.Parse(Console.ReadLine());
        }

        Console.WriteLine("Deseja atualizar o Tipo? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Tipo: ");
            venda.tipo = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o ID do Cliente? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo ID do Cliente (ou deixe em branco para NULL): ");
            string idClienteInput = Console.ReadLine();
            venda.id_cliente_fk = string.IsNullOrEmpty(idClienteInput) ? (int?)null : int.Parse(idClienteInput);
        }

        vendaDao.Update(venda);
    }

    public static void Deletar(VendaDAO vendaDao)
    {
        Console.Write("ID da Venda a ser deletada: ");
        int id = int.Parse(Console.ReadLine());

        Venda venda = new Venda { id = id };

        vendaDao.Delete(venda);
    }
}
