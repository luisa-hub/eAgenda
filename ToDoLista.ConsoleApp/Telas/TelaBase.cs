using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLista.ConsoleApp.Telas
{
    public class TelaBase
    {
        private string titulo;

        public string Titulo { get { return titulo; } }

        public TelaBase(string tit)
        {
            titulo = tit;
        }

        protected void ApresentarMensagem(string mensagem, TipoMensagem tm)
        {
            switch (tm)
            {
                case TipoMensagem.Sucesso:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case TipoMensagem.Atencao:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case TipoMensagem.Erro:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadLine();
        }

        protected void MontarCabecalhoTabela(string configuracaoColunasTabela, params object[] colunas)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(configuracaoColunasTabela, colunas);

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        protected void ConfigurarTela(string subtitulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();

            Console.WriteLine(subtitulo);

            Console.WriteLine();
        }


        public virtual string ObterOpcao()
        {
            return "";
        }

       

    }
}
