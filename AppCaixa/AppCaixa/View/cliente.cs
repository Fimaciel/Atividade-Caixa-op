namespace AppCaixa.view;

using System;
using AppCaixa.DAO;
using AppCaixa.Models;
using System.Collections.Generic;
using AppCaixa.Helpers;

class ClienteView()
{
    public static void principal()
    {
        ClienteDAO clienteDAO = new ClienteDAO();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir Cliente");
            Console.WriteLine("2. Listar Clientes");
            Console.WriteLine("3. Atualizar Cliente");
            Console.WriteLine("4. Deletar Cliente");
            Console.WriteLine("5. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    InserirCliente(clienteDAO);
                    break;
                case "2":
                    ListarClientes(clienteDAO);
                    break;
                case "3":
                    AtualizarCliente(clienteDAO);
                    break;
                case "4":
                    DeletarCliente(clienteDAO);
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

    public static void InserirCliente(ClienteDAO clienteDAO)
    {
        Cliente cliente = new Cliente();

        Console.Write("Nome: ");
        cliente.nome = Console.ReadLine();

        Console.Write("CPF: ");
        cliente.cpf = Console.ReadLine();

        Console.Write("Email: ");
        cliente.email = Console.ReadLine();

        Console.Write("Telefone: ");
        cliente.telefone = Console.ReadLine();

        Console.Write("Data de Nascimento (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
        {
            cliente.data_nasc = dataNasc;
        }

        clienteDAO.Insert(cliente);
    }

    public static void ListarClientes(ClienteDAO clienteDAO)
    {
        List<Cliente> clientes = clienteDAO.Listar();

        foreach (var cliente in clientes)
        {
            Console.WriteLine(
                $"ID: {cliente.id}, Nome: {cliente.nome}, CPF: {cliente.cpf}, Email: {cliente.email}, Telefone: {cliente.telefone}, Data de Nascimento: {cliente.data_nasc.ToString("yyyy-MM-dd")}");
        }
    }

    public static void AtualizarCliente(ClienteDAO clienteDAO)
    {
        Cliente cliente = new Cliente();

        Console.Write("ID do Cliente a ser atualizado: ");
        cliente.id = int.Parse(Console.ReadLine());

        Console.WriteLine("Deseja atualizar o Nome? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Nome: ");
            cliente.nome = Console.ReadLine();

            Console.WriteLine(cliente.nome);
        }

        Console.WriteLine("Deseja atualizar o CPF? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            bool cpfValido = true;
            while (cpfValido)
            {
                Console.Write("Novo CPF: ");
                cliente.cpf = Console.ReadLine();

                if (!Helpers.ValidarCpf(cliente.cpf))
                {
                    Console.WriteLine("Informe um cpf Válido!");
                }
                else
                {
                    cpfValido = false;
                }
            }
        }

        Console.WriteLine("Deseja atualizar o Email? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Email: ");
            cliente.email = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar o Telefone? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Novo Telefone: ");
            cliente.telefone = Console.ReadLine();
        }

        Console.WriteLine("Deseja atualizar a Data de Nascimento? (S/N)");
        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            Console.Write("Nova Data de Nascimento (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNasc))
            {
                cliente.data_nasc = dataNasc;
            }
            else
            {
                Console.WriteLine("Data de nascimento inválida.");
            }
        }

        clienteDAO.Update(cliente);
    }


    public static void DeletarCliente(ClienteDAO clienteDAO)
    {
        Console.Write("ID do Cliente a ser deletado: ");
        int id = int.Parse(Console.ReadLine());

        Cliente cliente = new Cliente { id = id };

        clienteDAO.Delete(cliente);
    }
}