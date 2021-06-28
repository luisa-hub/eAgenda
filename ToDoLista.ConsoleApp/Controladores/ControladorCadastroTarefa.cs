using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLista.ConsoleApp.Dominio;
namespace ToDoLista.ConsoleApp.Controladores
{
    public class ControladorCadastroTarefa : ControladorBase<Tarefa>
    {
        
       
        SqlConnection conexaoComBanco = new SqlConnection();

        public ControladorCadastroTarefa()
        {
            
        }

        override
        public string ComandoInsercao()
        {

            string sqlInsercao =
                   @"INSERT INTO TBTAREFAS
	        (
	             TITULO, 
	             DATAINICIO, 
	             DATACONCLUSAO, 
	             PERCENTUAL, 
	             PRIORIDADE
	        ) 
	        VALUES 
	        (
	            @TITULO, 
	            @DATAINICIO, 
	            @DATACONCLUSAO, 
	            @PERCENTUAL, 
	            @PRIORIDADE
	            );";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            return sqlInsercao;
        }

        override
        public string ComandoExclusao() {
            string sqlExclusao =
                @"DELETE FROM TBTAREFAS 	                
	                WHERE 
		                [ID] = @ID";
            return sqlExclusao;
        }

        override
        public string ComandoAtualizacao() {
            string sqlAtualizacao =
                        @"UPDATE TBTAREFAS 
	        SET	
		    [TITULO] = @TITULO, 
		    [DATAINICIO]= @DATAINICIO, 
		    [DATACONCLUSAO] = @DATACONCLUSAO,
	        [PERCENTUAL] = @PERCENTUAL,
            [PRIORIDADE] = @PRIORIDADE

	        WHERE 
		    [ID] = @ID";

            return sqlAtualizacao;

        }
        

        override
        public SqlCommand InserirNoBanco(Tarefa tarefa, ref SqlCommand comandoInsercao)
        {
            comandoInsercao.Parameters.AddWithValue("Id", tarefa.Id);
            comandoInsercao.Parameters.AddWithValue("Titulo", tarefa.Titulo);
            comandoInsercao.Parameters.AddWithValue("DataInicio", tarefa.DataCriação);
            
            comandoInsercao.Parameters.AddWithValue("Percentual", tarefa.PercentualConcluído);
            comandoInsercao.Parameters.AddWithValue("Prioridade", tarefa.Prioridade);

            if(tarefa.DataConclusão == null)
                comandoInsercao.Parameters.AddWithValue("DataConclusao", DBNull.Value);
            else
                comandoInsercao.Parameters.AddWithValue("DataConclusao", tarefa.DataConclusão);

            return comandoInsercao;
        }


        public List<Tarefa> VisualizarTarefaConcluida()
        {
            AbrirConexao();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [TITULO], 
		                [DATAINICIO], 
		                [DATACONCLUSAO] ,
	                    [PERCENTUAL],
                        [PRIORIDADE] 
                    FROM 
                        TBTAREFAS
                    WHERE
                        [PERCENTUAL] = 100";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = AdicionarTarefas(leitorTarefas);
                        
            FecharConexao();

            return tarefas;
        }


        public List<Tarefa> VisualizarTarefaPendente()
        {

            AbrirConexao();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                    [ID],
                    [TITULO], 
		            [DATAINICIO], 
		            [DATACONCLUSAO] ,
	                [PERCENTUAL],
                    [PRIORIDADE] 
                    FROM 
                        TBTAREFAS
                    WHERE
                        [PERCENTUAL] < 100";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = AdicionarTarefas(leitorTarefas);

           
            FecharConexao();

            return tarefas;
        }

        private static List<Tarefa> AdicionarTarefas(SqlDataReader leitorTarefas)
        {
            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATAINICIO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                int percentualConclusao = Convert.ToInt32(leitorTarefas["PERCENTUAL"]);
                string prioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);

                Tarefa t = new Tarefa(titulo, dataCriacao, dataConclusao, percentualConclusao, prioridade);
                t.Id = id;

                tarefas.Add(t);
            }

            return tarefas;
        }

              

        private void FecharConexao()
        {
            conexaoComBanco.Close();
        }

        private void AbrirConexao()
        {
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();
        }
    }
}
