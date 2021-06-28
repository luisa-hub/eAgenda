using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLista.ConsoleApp.Controladores;
using ToDoLista.ConsoleApp.Dominio;

namespace ToDoLista.ConsoleApp.Telas
{
    public class TelaCompromisso: TelaBase, ICadastravel
    {
        private readonly ControladorCadastroCompromisso controladorCompromisso;
        private readonly TelaContato telaContato;
        public TelaCompromisso(ControladorCadastroCompromisso controladorCompromisso, TelaContato telaContato) : base("Compromissos...")
        {
            this.controladorCompromisso = controladorCompromisso;
            this.telaContato = telaContato;
        }

        public void InserirNovoRegistro()
        {

            Compromisso compromisso = ObterCompromisso();

            string resultadoValidacao = controladorCompromisso.InserirNovoObjeto(compromisso);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Compromisso inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public bool VisualizarRegistros()
        {
            ConfigurarTela("Visualizando compromissos...");

            List<Compromisso> compromissos = controladorCompromisso.VisualizarCompromissos();

            if (compromissos.Count == 0)
            {
                ApresentarMensagem("Nenhum contato cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaoColunasTabela = "{0,-10} | {1,-35} | {2,-35} | {3, -35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Assunto", "Data Compromisso", "Id do Contato");

           
            
            foreach (Compromisso compromisso in compromissos)
            {
                Console.WriteLine(configuracaoColunasTabela, compromisso.Id, compromisso.Assunto, compromisso.DataCompromisso, compromisso.Id_contato);
            }

            return true;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando compromissos...");

            bool temRegistros = VisualizarRegistros();

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do contato que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Compromisso compromisso = controladorCompromisso.VisualizarCompromissos().Find(x => id == x.Id);

            if (compromisso == null)
            {
                ApresentarMensagem("Nenhum compromisso foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            compromisso = ObterCompromisso();
            compromisso.Id = id;

            string resultadoValidacao = controladorCompromisso.AtualizarObjeto(compromisso);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Compromisso editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        private Compromisso ObterCompromisso()
        {
            Console.Write("Digite o assunto: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite a data do compromisso: ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite o local do compromisso: ");
            string local = Console.ReadLine();

            Console.Write("Digite o horário de início: ");
            DateTime horarioInicio = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite o horário final: ");
            DateTime horarioFim = Convert.ToDateTime(Console.ReadLine());

            int numeroContato = ObterNumeroContato();

            return new Compromisso(assunto, data, horarioInicio, horarioFim, numeroContato, local);
        }

        public int ObterNumeroContato() {

            int id = 0;

            Console.WriteLine("Deseja vincular ao seu compromisso o número de um contato existente?");
            Console.WriteLine(" 1 - Sim \n 2 - Não");
            int opcao = Convert.ToInt32(Console.ReadLine());

            if (opcao == 1) {
                telaContato.VisualizarRegistros();

                Console.WriteLine("Digite o número do id que deseja vincular:");
                id = Convert.ToInt32(Console.ReadLine()); 
            }            

            if (opcao == 2) {

                return default;
            }


            return id;
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo compromisso...");

            bool temRegistros = VisualizarRegistros();

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do id do contato que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Compromisso compromisso = controladorCompromisso.VisualizarCompromissos().Find(x => x.Id == id);

            if (compromisso == null)
            {
                ApresentarMensagem("Nenhum compromisso foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }


            bool conseguiuExcluir = controladorCompromisso.ExcluirObjeto(compromisso);

            if (conseguiuExcluir)
                ApresentarMensagem("Compromisso excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o compromisso", TipoMensagem.Erro);
                ExcluirRegistro();
            }
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
