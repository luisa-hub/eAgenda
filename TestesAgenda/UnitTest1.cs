using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToDoLista.ConsoleApp.Controladores;
using ToDoLista.ConsoleApp.Dominio;

namespace TestesAgenda
{
    [TestClass]
    public class TestesTarefa
    {
        [TestMethod]
        public void TesteInsercao()
        {

            ControladorCadastroTarefa controladorTarefa = new ControladorCadastroTarefa();
            Tarefa tarefa = new Tarefa("programar", new DateTime(2021, 06, 06), new DateTime(2021, 06, 07), 90, 2 );
            

            Assert.AreEqual("ESTA_VALIDO", controladorTarefa.InserirNovoObjeto(tarefa));
        }

        [TestMethod]
        public void TesteInsercaoDataConclusaoNula()
        {

            ControladorCadastroTarefa controladorTarefa = new ControladorCadastroTarefa();
            Tarefa tarefa = new Tarefa("programar", new DateTime(2021, 06, 06), new Nullable<DateTime>(), 90, 2);
            Assert.AreEqual("ESTA_VALIDO", controladorTarefa.InserirNovoObjeto(tarefa));
        }
    }
}
