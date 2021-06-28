using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLista.ConsoleApp.Dominio;

namespace ToDoLista.ConsoleApp.Controladores
{
    public class ControladorBase<T> where T: EntidadeBase
    {
        public string enderecoDBTarefas =
              @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";
        SqlConnection conexaoComBanco = new SqlConnection();


        
        public string InserirNovoObjeto(T objeto)
        {
            string resultadoValidacao = objeto.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {

                AbrirConexao();

                SqlCommand comandoInsercao = new SqlCommand();
                comandoInsercao.Connection = conexaoComBanco;

                string sqlInsercao = ComandoInsercao();

                comandoInsercao.CommandText = sqlInsercao;

                InserirNoBanco(objeto, ref comandoInsercao);

                object id = comandoInsercao.ExecuteScalar();

                objeto.id = Convert.ToInt32(id);

                FecharConexao();
            }

            return resultadoValidacao;
        }

        public bool ExcluirObjeto(T objeto)
        {

            AbrirConexao();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao = ComandoExclusao();

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", objeto.id);

            comandoExclusao.ExecuteNonQuery();


            FecharConexao();

            return true;
        }

        public string AtualizarObjeto(T objeto)
        {
            string resultadoValidacao = objeto.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                AbrirConexao();

                SqlCommand comandoAtualizacao = new SqlCommand();
                comandoAtualizacao.Connection = conexaoComBanco;

                string sqlAtualizacao =
                    ComandoAtualizacao();

                comandoAtualizacao.CommandText = sqlAtualizacao;

                comandoAtualizacao = InserirNoBanco(objeto, ref comandoAtualizacao);

                comandoAtualizacao.ExecuteNonQuery();



                FecharConexao();
            }

            return resultadoValidacao;
        }



        public virtual string ComandoExclusao()
        {
            return "";
        }

        public virtual string ComandoInsercao()
        {
            return "";
        }

        public virtual string ComandoAtualizacao()
        {
            return "";

        }

        public virtual  SqlCommand InserirNoBanco(T objeto, ref SqlCommand comando)
        {
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
