using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLista.ConsoleApp.Dominio;

namespace ToDoLista.ConsoleApp.Controladores
{
    public class ControladorCadastroCompromisso : ControladorBase<Compromisso>
    {
       
        SqlConnection conexaoComBanco = new SqlConnection();
       

        public ControladorCadastroCompromisso()
        {
           

        }

        override
        public string ComandoInsercao()
        {

            string sqlInsercao =
                   @"INSERT INTO TBCOMPROMISSOS
            (
                 [ASSUNTO],
                 [LOCAL],
                [DATACOMPROMISSO],
                [HORAINICIO],
                [HORAFIM],
                [ID_CONTATO]


            )


            VALUES
            (
                @ASSUNTO,
                 @LOCAL,
                @DATACOMPROMISSO,
                @HORAINICIO,
                @HORAFIM,
                @ID_CONTATO
             )";

            sqlInsercao +=
                @"SELECT SCOPE_IDENTITY();";

            return sqlInsercao;

        }

        override
        public string ComandoExclusao() {

            string sqlExclusao =
                @"DELETE FROM TBCOMPROMISSOS 	                
	                WHERE 
		                [ID] = @ID";
            return sqlExclusao;
        }

        override 
        public string ComandoAtualizacao()
        {
            string sqlAtualizacao =
    @"UPDATE TBCOMPROMISSOS
	        SET	
            
		     [ASSUNTO] = @ASSUNTO,
             [LOCAL] = @LOCAL,
             [DATACOMPROMISSO] = @DATACOMPROMISSO,
             [HORAINICIO] = @HORAINICIO,
             [HORAFIM] = @HORAFIM,
             [ID_CONTATO] = @ID_CONTATO
            

	        WHERE 
		    [ID] = @ID";

            return sqlAtualizacao;


        }
       

        public List<Compromisso> VisualizarCompromissos()
        {
            AbrirConexao();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
            
                 [ID],
                 [ASSUNTO],
                 [LOCAL],
                [DATACOMPROMISSO],
                [HORAINICIO],
                [HORAFIM],
                [ID_CONTATO]

             FROM TBCompromissos  ";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorCompromissos = comandoSelecao.ExecuteReader();

            List<Compromisso> compromissos = AdicionarCompromissosNaLista(leitorCompromissos);

            FecharConexao();

            return compromissos;
            throw new NotImplementedException();
        }
              
        
        private List<Compromisso> AdicionarCompromissosNaLista(SqlDataReader leitorCompromissos)
        {
            List<Compromisso> compromissos = new List<Compromisso>();

            while (leitorCompromissos.Read())
            {
                int id = Convert.ToInt32(leitorCompromissos["ID"]);
                string assunto = Convert.ToString(leitorCompromissos["ASSUNTO"]);
                string local = Convert.ToString(leitorCompromissos["LOCAL"]);
                DateTime dataCompromisso = Convert.ToDateTime(leitorCompromissos["DATACOMPROMISSO"]);
                DateTime horaInicio = Convert.ToDateTime(leitorCompromissos["HORAINICIO"]);
                DateTime horaFim = Convert.ToDateTime(leitorCompromissos["HORAFIM"]);
                int id_contato;

                if (leitorCompromissos["ID_CONTATO"] == DBNull.Value)
                    id_contato = default;
                else
                    id_contato = Convert.ToInt32(leitorCompromissos["ID_CONTATO"]);

                Compromisso c = new Compromisso( assunto,  dataCompromisso,  horaInicio,  horaFim,  id_contato,  local);
                c.Id = id;

                compromissos.Add(c);
            }

            return compromissos;
        }

        override
        public SqlCommand InserirNoBanco(Compromisso compromisso, ref SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", compromisso.Id);
            comando.Parameters.AddWithValue("ASSUNTO", compromisso.Assunto);
            comando.Parameters.AddWithValue("LOCAL", compromisso.Local);
            comando.Parameters.AddWithValue("DATACOMPROMISSO", compromisso.DataCompromisso);
            comando.Parameters.AddWithValue("HORAINICIO", compromisso.HoraInicio);
            comando.Parameters.AddWithValue("HORAFIM", compromisso.HoraFim);

            if (compromisso.Id_contato == default)
                comando.Parameters.AddWithValue("ID_CONTATO", DBNull.Value);
            else
                comando.Parameters.AddWithValue("ID_CONTATO", compromisso.Id_contato);

            return comando;
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
