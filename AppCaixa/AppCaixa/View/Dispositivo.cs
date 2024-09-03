using System;
using AppCaixa.DAO;
using AppCaixa.Models;

namespace AppCaixa.view
{
    public static class DispositivoView
    {
        public static void Menu()
        {
            DispositivoDAO dispositivoDao = new DispositivoDAO();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Inserir Dispositivo");
                Console.WriteLine("2. Listar Dispositivos");
                Console.WriteLine("3. Atualizar Dispositivo");
                Console.WriteLine("4. Deletar Dispositivo");
                Console.WriteLine("5. Sair");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Inserir(dispositivoDao);
                        break;
                    case "2":
                        Listar(dispositivoDao);
                        break;
                    case "3":
                        Atualizar(dispositivoDao);
                        break;
                    case "4":
                        Deletar(dispositivoDao);
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

        public static void Inserir(DispositivoDAO dispositivoDao)
        {
            Dispositivo dispositivo = new Dispositivo();

            Console.Write("Aliquota: ");
            dispositivo.aliquota = double.Parse(Console.ReadLine());

            Console.Write("Descrição: ");
            dispositivo.descricao = Console.ReadLine();

            Console.Write("Status (ativo/inativo): ");
            dispositivo.status_dispo = Console.ReadLine().Trim().ToLower() == "ativo";

            dispositivoDao.Insert(dispositivo);
        }

        public static void Listar(DispositivoDAO dispositivoDao)
        {
            List<Dispositivo> dispositivos = dispositivoDao.Listar();

            foreach (var d in dispositivos)
            {
                Console.WriteLine(
                    $"ID: {d.id}, Aliquota: {d.aliquota}, Descrição: {d.descricao}, Status: {(d.status_dispo ? "Ativo" : "Inativo")}");
            }
        }

        public static void Atualizar(DispositivoDAO dispositivoDao)
        {
            Dispositivo dispositivo = new Dispositivo();

            Console.Write("ID do Dispositivo a ser atualizado: ");
            dispositivo.id = int.Parse(Console.ReadLine());

            Console.WriteLine("Deseja atualizar a Aliquota? (S/N)");
            if (Console.ReadLine().Trim().ToUpper() == "S")
            {
                Console.Write("Nova Aliquota: ");
                dispositivo.aliquota = double.Parse(Console.ReadLine());
            }

            Console.WriteLine("Deseja atualizar a Descrição? (S/N)");
            if (Console.ReadLine().Trim().ToUpper() == "S")
            {
                Console.Write("Nova Descrição: ");
                dispositivo.descricao = Console.ReadLine();
            }

            Console.WriteLine("Deseja atualizar o Status? (S/N)");
            if (Console.ReadLine().Trim().ToUpper() == "S")
            {
                Console.Write("Status (ativo/inativo): ");
                dispositivo.status_dispo = Console.ReadLine().Trim().ToLower() == "ativo";
            }

            dispositivoDao.Update(dispositivo);
        }

        public static void Deletar(DispositivoDAO dispositivoDao)
        {
            Console.Write("ID do Dispositivo a ser deletado: ");
            int id = int.Parse(Console.ReadLine());

            Dispositivo dispositivo = new Dispositivo { id = id };

            dispositivoDao.Delete(dispositivo);
        }
    }
}
