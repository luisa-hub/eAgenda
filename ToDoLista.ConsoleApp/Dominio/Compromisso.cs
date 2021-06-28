using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLista.ConsoleApp.Dominio
{
    public class Compromisso : EntidadeBase
    {
        private int id;
        private string assunto;
        private string local;
        private DateTime dataCompromisso;
        private DateTime horaInicio;
        private DateTime horaFim;
        private int id_contato;

        public Compromisso(string assunto, DateTime dataCompromisso, DateTime horaInicio, DateTime horaFim, int id_contato, string local)
        {
            this.assunto = assunto;
            this.dataCompromisso = dataCompromisso;
            this.horaInicio = horaInicio;
            this.horaFim = horaFim;
            this.id_contato = id_contato;
            this.local = local;
        }


        override
        public string Validar()
        {
            string resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;

        }

        public int Id { get => id; set => id = value; }
        public string Assunto { get => assunto; set => assunto = value; }
        public DateTime DataCompromisso { get => dataCompromisso; set => dataCompromisso = value; }
        public DateTime HoraInicio { get => horaInicio; set => horaInicio = value; }
        public DateTime HoraFim { get => horaFim; set => horaFim = value; }
        public int Id_contato { get => id_contato; set => id_contato = value; }
        public string Local { get => local; set => local = value; }
    }
}
