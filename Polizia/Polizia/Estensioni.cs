using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Polizia
{
    public static class Estensioni
    {
        //Implementazione di un metodo per convertire l'oggetto letto da database in agente 
        //per poterlo visualizzare in console

        //ma ho problemi con il costruttore

        //public static Agente ConvertToAgente(this SqlDataReader reader)
        //{
        //    return new Agente(Nome, cognome, codiceFiscale)
        //    {
        //        CodiceFiscale = reader["CF"].ToString(),
        //        Nome = reader["Nome"].ToString(),
        //        Cognome = reader["Cognome"].ToString(),
        //        AnniServizio = (int)reader["AnniServizio"]
        //    };
        //}
    }
}
