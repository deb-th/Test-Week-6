using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Polizia
{
    //Definizione della classe Agente come classe derivata dalla classe Persona
    public class Agente : Persona
    {
        private object nome;
        private object cognome;
        private object codiceFiscale;

        //proprietà
        public DateTime DataNascita { get; set; }
        public int AnniServizio { get; set; }

        //Costruttore di Agente derivato parzialmente dalla classe base Persona
        public Agente(string nome, string cognome, string codiceFiscale, DateTime dataNascita, int anniServizio) : base(nome, cognome, codiceFiscale)
        {
            DataNascita = dataNascita;
            AnniServizio = anniServizio;
        }

        //public Agente(object nome, object cognome, object codiceFiscale, object anniServizio)
        //{
        //    this.CodiceFiscale = codiceFiscale;
        //    this.Nome = nome;
        //    this.Cognome = cognome;
        //    this.AnniServizio = anniServizio;
        //}

        //metodo override della classe object per visualizzare le informazioni dell'agente
        public override string ToString()
        {
            return $"{CodiceFiscale} - {Nome} {Cognome} - {AnniServizio} anni di servizio";
        }
    }
}