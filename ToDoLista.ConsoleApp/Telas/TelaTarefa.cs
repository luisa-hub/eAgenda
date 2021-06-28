using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLista.ConsoleApp.Controladores;
using ToDoLista.ConsoleApp.Dominio;

namespace ToDoLista.ConsoleApp.Telas
{
    public class TelaTarefa: TelaBase
    {
        private readonly ControladorCadastroTarefa controladorTarefa;

        public TelaTarefa(ControladorCadastroTarefa controladorTarefa) : base("Tarefas...")
        {
            this.controladorTarefa = controladorTarefa;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando tarefas...");

            bool temRegistros = VisualizarRegistrosPendentes();

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da tarefa que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Tarefa tarefa = controladorTarefa.VisualizarTarefaPendente().Find(x => x.Id == id);

            if (tarefa == null)
            {
                ApresentarMensagem("Nenhuma tarefa foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            tarefa = ObterTarefa();
            tarefa.Id = id;

            string resultadoValidacao = controladorTarefa.AtualizarObjeto(tarefa);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo tarefa...");

            bool temRegistros = VisualizarRegistrosPendentes();

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do id da tarefa que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Tarefa tarefa= controladorTarefa.VisualizarTarefaPendente().Find(x => x.Id == id);

            if (tarefa == null)
            {
                ApresentarMensagem("Nenhuma tarefa foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }


            bool conseguiuExcluir = controladorTarefa.ExcluirObjeto(tarefa);

            if (conseguiuExcluir)
                ApresentarMensagem("tarefa excluída com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public void InserirNovoRegistro()
        {

            Tarefa tarefa = ObterTarefa();

            string resultadoValidacao = controladorTarefa.InserirNovoObjeto(tarefa);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Tarefa inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public bool VisualizarRegistros()
        {
            ConfigurarTela("Visualizando tarefa...");

            List<Tarefa> tarefas = controladorTarefa.VisualizarTarefaConcluida();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem("Nenhuma tarefa cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35} | {3, -35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Titulo", "Pocentagem", "Prioridade");

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(configuracaoColunasTabela, tarefa.Id, tarefa.Titulo, tarefa.PercentualConcluído, tarefa.Prioridade);
            }

            return true;
        }

        public bool VisualizarRegistrosPendentes()
        {
            ConfigurarTela("Visualizando tarefa...");

            List<Tarefa> tarefas = controladorTarefa.VisualizarTarefaPendente();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem("Nenhuma tarefa cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Titulo", "Pocentagem");

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(configuracaoColunasTabela, tarefa.Id, tarefa.Titulo, tarefa.PercentualConcluído);
            }

            Console.ReadLine();

            return true;

        }

        private Tarefa ObterTarefa()
        {
            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a data de criação: ");
           DateTime data = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite a data de conclusao: ");
            DateTime dataConclusao = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite o percentual da tarefa: ");
            int percentual = Int32.Parse(Console.ReadLine());

            Console.Write("Digite a prioridade (1 - Alta, 2 - Média, 3 - Baixa) ");
            int opcao = Int32.Parse(Console.ReadLine());

            return new Tarefa(titulo, data, dataConclusao, percentual, opcao);
        }

        override
        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo registro");
            Console.WriteLine("Digite 2 para visualizar registros concluídos");
            Console.WriteLine("Digite 3 para editar um registro");
            Console.WriteLine("Digite 4 para excluir um registro");
            Console.WriteLine("Digite 5 para visualizar registros pendentes");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }
    }
}
