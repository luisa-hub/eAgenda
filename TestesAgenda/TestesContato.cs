using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToDoLista.ConsoleApp.Controladores;
using ToDoLista.ConsoleApp.Dominio;

namespace TestesAgenda
{
    [TestClass]
    public class TestesContato
    {
        [TestMethod]
        public void TesteInsercao()
        {

            ControladorCadastroContato controladorContato = new ControladorCadastroContato();
            Contato contato = new Contato("luisa", "luisa@", "9090990", "empresa", "cargo");


            Assert.AreEqual("ESTA_VALIDO", controladorContato.InserirNovoObjeto(contato));
        }

        [TestMethod]
        public void TesteInsercaoSemArroba()
        {

            ControladorCadastroContato controladorContato = new ControladorCadastroContato();
            Contato contato = new Contato("luisa", "luisa", "9090990", "empresa", "cargo");


            Assert.AreEqual("Email inválido", controladorContato.InserirNovoObjeto(contato));
        }
    }
}
