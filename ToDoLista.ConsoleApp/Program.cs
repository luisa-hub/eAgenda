using System;
using ToDoLista.ConsoleApp.Telas;

namespace ToDoLista.ConsoleApp
{
    class Program
    {
        static TelaPrincipal telaPrincipal = new TelaPrincipal();

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = telaPrincipal.ObterTela();

                if (telaSelecionada == null)
                    break;

                Console.Clear();

                if (telaSelecionada is TelaBase)
                    Console.WriteLine(telaSelecionada.Titulo); Console.WriteLine();


                if (telaSelecionada is ICadastravel)
                {
                    ICadastravel tela = (ICadastravel)telaSelecionada;

                    string opcao = telaSelecionada.ObterOpcao();
                    if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (opcao == "1")
                        tela.InserirNovoRegistro();

                    else if (opcao == "2")
                    {
                        bool temRegistros = tela.VisualizarRegistros();
                        if (temRegistros)
                            Console.ReadLine();
                    }

                    else if (opcao == "3")
                        tela.EditarRegistro();

                    else if (opcao == "4")
                        tela.ExcluirRegistro();
                }

                if (telaSelecionada is TelaTarefa)
                {
                    TelaTarefa tela = (TelaTarefa)telaSelecionada;
                    string opcao = telaSelecionada.ObterOpcao();
                    if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (opcao == "1")
                        tela.InserirNovoRegistro();

                    else if (opcao == "2")
                    {
                        bool temRegistros = tela.VisualizarRegistros();
                        if (temRegistros)
                            Console.ReadLine();
                    }

                    else if (opcao == "3")
                        tela.EditarRegistro();

                    else if (opcao == "4")
                        tela.ExcluirRegistro();

                    else if (opcao == "5")
                        tela.VisualizarRegistrosPendentes();
                }
            }






        }
    }
}
