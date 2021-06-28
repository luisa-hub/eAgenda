﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLista.ConsoleApp.Controladores;
using ToDoLista.ConsoleApp.Dominio;

namespace ToDoLista.ConsoleApp.Telas
{
    public class TelaContato: TelaBase, ICadastravel
    {
        private readonly ControladorCadastroContato controladorContato;

        public TelaContato(ControladorCadastroContato controladorContato):base("Contatos...")
        {
            this.controladorContato = controladorContato;
        }

        public void InserirNovoRegistro()
        {            

            Contato contato = ObterContato();

            string resultadoValidacao = controladorContato.InserirNovoObjeto(contato);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Contato inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public bool VisualizarRegistros()
        {           
            ConfigurarTela("Visualizando contatos...");

            List<Contato> contatos = controladorContato.VisualizarContatoPorCargo();
           
            if (contatos.Count == 0)
            {
                ApresentarMensagem("Nenhum contato cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Nome", "Cargo");

            foreach (Contato contato in contatos)
            {
                Console.WriteLine(configuracaoColunasTabela, contato.Id, contato.Nome, contato.Cargo);
            }

            return true;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando contato...");

            bool temRegistros = VisualizarRegistros();

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do contato que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Contato contato = controladorContato.VisualizarContatoPorCargo().Find(x=> id == x.Id);

            if (contato == null)
            {
                ApresentarMensagem("Nenhum contato foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            contato = ObterContato();
            contato.Id = id;

            string resultadoValidacao = controladorContato.AtualizarObjeto(contato);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Contato editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo contato...");

            bool temRegistros = VisualizarRegistros();

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do id do contato que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Contato contato =  controladorContato.VisualizarContatoPorCargo().Find(x=> x.Id == id);

            if (contato == null)
            {
                ApresentarMensagem("Nenhum contato foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }
            

            bool conseguiuExcluir = controladorContato.ExcluirObjeto(contato);

            if (conseguiuExcluir)
                ApresentarMensagem("Contato excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o contato", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        private Contato ObterContato()
        {
            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email: ");
            string email = Console.ReadLine();

            Console.Write("Digite o nome da empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o telefone do contato: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();

            return new Contato(nome, email, empresa, telefone, cargo);
        }

        override
        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo registro");
            Console.WriteLine("Digite 2 para visualizar registros");
            Console.WriteLine("Digite 3 para editar um registro");
            Console.WriteLine("Digite 4 para excluir um registro");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }
    }
}
