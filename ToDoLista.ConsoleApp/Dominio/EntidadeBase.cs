using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLista.ConsoleApp.Dominio
{
    public abstract class EntidadeBase
    {
        public int id;
        public abstract string Validar();
    }
}
