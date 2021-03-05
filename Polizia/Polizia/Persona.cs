using System;
using System.Collections.Generic;
using System.Text;

namespace Polizia
{
    //Definizione della classe astratta Persona
    public abstract class Persona
    {
        //Proprietà
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }

        //Costruttore della classe astratta con parametri
        public Persona(string nome, string cognome, string codiceFiscale)
        {
            Nome = nome;
            Cognome = cognome;
            CodiceFiscale = codiceFiscale;
        }

        //metodi override di Equals della Classe Object
        public override bool Equals(object obj)
        {
            return obj is Persona persona &&
                CodiceFiscale == persona.CodiceFiscale;
        }

        public static bool operator ==(Persona left, Persona right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Persona left, Persona right)
        {
            return !(left == right);
        }           
    }
}
