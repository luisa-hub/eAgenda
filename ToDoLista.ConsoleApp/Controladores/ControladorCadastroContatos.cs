using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using ToDoLista.ConsoleApp.Dominio;
namespace ToDoLista.ConsoleApp.Controladores
{
    public class ControladorCadastroContato: ControladorBase<Contato>
    {
        
        SqlConnection conexaoComBanco = new SqlConnection();

        public ControladorCadastroContato() {
           
        }

        override
        public string ComandoInsercao()
        {
            string sqlInsercao =
                    @"INSERT INTO TBCONTATOS
	        (
	             NOME,
                 EMAIL,
                EMPRESA,
                TELEFONE,
                CARGO

	        ) 
	        VALUES 
	        (
	            @NOME, 
	            @EMAIL, 
	            @EMPRESA, 
	            @TELEFONE, 
	           @CARGO
	            );";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            return sqlInsercao;
        }

        public string ComandoSelecao() {

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [NOME], 
		                [EMAIL], 
		                [EMPRESA] ,
	                    [TELEFONE],
                        [CARGO] 
                    FROM 
                        TBCONTATOS
                    ORDER BY [CARGO]";

            return sqlSelecao;
        }

        override
        public string ComandoAtualizacao() {

            string sqlAtualizacao =
                        @"UPDATE TBCONTATOS
	        SET	
            
		    [NOME] = @NOME, 
		    [EMAIL]= @EMAIL, 
		    [EMPRESA] = @EMPRESA,
	        [TELEFONE] = @TELEFONE,
            [CARGO] = @CARGO
            

	        WHERE 
		    [ID] = @ID";

            return sqlAtualizacao;

        }

        override
        public string ComandoExclusao() {

            string sqlExclusao = @"DELETE FROM TBCONTATOS 	                
	                WHERE 
		                [ID] = @ID";

            return sqlExclusao;

        }        

        override
        public  SqlCommand InserirNoBanco(Contato contato, ref SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", contato.Id);
            comando.Parameters.AddWithValue("NOME", contato.Nome);
            comando.Parameters.AddWithValue("EMAIL", contato.Email);
            comando.Parameters.AddWithValue("EMPRESA", contato.Empresa);
            comando.Parameters.AddWithValue("TELEFONE", contato.Telefone);
            comando.Parameters.AddWithValue("CARGO", contato.Cargo);

            return comando;
        }

        public List<Contato> VisualizarContatoPorCargo()
        {
            AbrirConexao();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [NOME], 
		                [EMAIL], 
		                [EMPRESA] ,
	                    [TELEFONE],
                        [CARGO] 
                    FROM 
                        TBCONTATOS
                    ORDER BY [CARGO]";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            List<Contato> contatos = AdicionarContatosNaLista(leitorContatos);           

            FecharConexao();

            return contatos;

        }

        private static List<Contato> AdicionarContatosNaLista(SqlDataReader leitorContatos)
        {
            List<Contato> contatos = new List<Contato>();

            while (leitorContatos.Read())
            {
                int id = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
                string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);

                Contato c = new Contato(nome, email, empresa, telefone, cargo);
                c.Id = id;

                contatos.Add(c);
            }

            return contatos;
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
