using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Polizia
{
    //Implemetazione della classe che si interfaccia al database Polizia
    //per la manipolazione dei dati inerenti agli Agenti
    public class GestioneAgente
    {
        //Definizione della Connection String per connettermi al database Polizia che contiene la tabella degli Agenti
        const string connectionString = @"Persist Security Info = False; Integrated Security = true; Initial Catalog = Polizia; Server = .\SQLEXPRESS";

        //METODI

        //metodo in modalità connessa GetAllAgents 
        //per visualizzare l'elenco di tutti gli Agenti presenti nel Database
        public static void GetAllAgents()
        {
            //CREO LA CONNESSIONE
            //creo oggetto connection

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //APRO LA CONNESSIONE
                connection.Open();

                //Creo il command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Agenti";

                //Eseguo il comando con DataReader
                SqlDataReader reader = command.ExecuteReader();

                //LETTURA DEI DATI
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["CF"]} - {reader["Nome"]} {reader["Cognome"]} - {reader["AnniServizio"]} anni di servizio");

                    //Con il metodo di estensione andrebbe chiamato così
                    //Console.WriteLine(reader.ConvertToAgente().ToString());
                }

                //Chiudo la connessione
                reader.Close();
                connection.Close();
            }
        }

        //metodo in modalità connessa GetAgentsByArea
        //per mostrare gli agenti assegnati ad una determinata area data da input dell’utente.
        public static void GetAgentsByArea(string Area)
        {
            //Creo la connessione con l'oggetto connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Apro la connessione
                connection.Open();

                //Creo il command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Agenti INNER JOIN Pattuglie" +
                                      " ON (Agenti.ID = Pattuglie.AgenteID)" +
                                      " INNER JOIN AreeMetropolitane" +
                                      " ON (Pattuglie.AreaID = AreeMetropolitane.ID)" +
                                      " WHERE AreeMetropolitane.Codice = @Area";

                //CREO IL PARAMETRO
                command.Parameters.AddWithValue("@Area", Area);

                //Eseguo il command con DataReader
                SqlDataReader reader = command.ExecuteReader();

                //Lettura dei dati
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["CF"]} - {reader["Nome"]} {reader["Cognome"]} - {reader["AnniServizio"]} anni di servizio");

                    //Con il metodo di estensione andrebbe chiamato così
                    //Console.WriteLine(reader.ConvertToAgente().ToString());
                }

                //Chiudo la connessione
                reader.Close();
                connection.Close();
            }
        }

        //metodo in modalità connessa GetAgentsByServizio
        //per mostrare gli agenti con anni di servizio maggiori o uguali rispetto ad un input dato dall’utente
        public static void GetAgentsByServizio(int anni)
        {
            //Creo la connessione con l'oggetto connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Apro la connessione
                connection.Open();

                //Creo il command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Agenti" +
                                      " WHERE Agenti.AnniServizio >= @anni";

                //CREO IL PARAMETRO
                command.Parameters.AddWithValue("@anni", anni);

                //Eseguo il command con DataReader
                SqlDataReader reader = command.ExecuteReader();

                //Lettura dei dati
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["CF"]} - {reader["Nome"]} {reader["Cognome"]} - {reader["AnniServizio"]} anni di servizio");

                    //Con il metodo di estensione andrebbe chiamato così
                    //Console.WriteLine(reader.ConvertToAgente().ToString());
                }

                //Chiudo la connessione
                reader.Close();
                connection.Close();
            }
        }

        //metodo in modalità disconnessa NewAgent
        //per inserire un nuovo agente nel database Polizia.
        public static void NewAgent(Agente newAgente)
        {
            //Creo la connessione
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Creo l'ADAPTER per mediare locale e database
                SqlDataAdapter adapter = new SqlDataAdapter();

                //Creo i comandi da associare all'adapter

                //Comando per selezionare tutti gli agenti presenti nel database
                SqlCommand SelectCommand = new SqlCommand();
                SelectCommand.Connection = connection;
                SelectCommand.CommandType = System.Data.CommandType.Text;
                SelectCommand.CommandText = "SELECT * FROM Agenti";

                //Comando per inserire un nuovo agente
                SqlCommand InsertCommand = new SqlCommand();
                InsertCommand.Connection = connection;
                InsertCommand.CommandType = System.Data.CommandType.Text;
                InsertCommand.CommandText = "INSERT INTO Agenti VALUES(@Nome, @Cognome, @CF, @DataNascita, @AnniServizio) ";

                //istruisco l'adapter per eseguire l'insert con parametri legandoli alle colonne corrispondenti
                InsertCommand.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 255, "Nome");
                InsertCommand.Parameters.Add("@Cognome", System.Data.SqlDbType.NVarChar, 255, "Cognome");
                InsertCommand.Parameters.Add("@CF", System.Data.SqlDbType.NVarChar, 16, "CF");
                InsertCommand.Parameters.Add("@DataNascita", System.Data.SqlDbType.DateTime, 200, "DataNascita");
                InsertCommand.Parameters.Add("@AnniServizio", System.Data.SqlDbType.Int, 200, "AnniServizio");

                //Associo i comandi all'adapter
                adapter.SelectCommand = SelectCommand;
                adapter.InsertCommand = InsertCommand;

                //creo il DATASET che conterrà in locale i dati prelevati dal database
                DataSet dataSet = new DataSet();

                try
                {
                    //Apro la connessione
                    connection.Open();

                    //popolo il dataset tramite l'adapter con la tabella Agenti
                    adapter.Fill(dataSet, "Agenti");

                    //Utilizzo i dati IN LOCALE senza sfruttare la connessione

                    //creare una nuova riga DataRow
                    DataRow agente = dataSet.Tables["Agenti"].NewRow();
                    agente["Nome"] = newAgente.Nome;
                    agente["Cognome"] = newAgente.Cognome;
                    agente["CF"] = newAgente.CodiceFiscale;
                    agente["DataNascita"] = newAgente.DataNascita;
                    agente["AnniServizio"] = newAgente.AnniServizio;

                    //insert della riga nella tabella Agenti creata in LOCALE
                    dataSet.Tables["Agenti"].Rows.Add(agente);

                    //Update delle modifiche sul database sfruttando la connessione tramite l'adapter
                    adapter.Update(dataSet, "Agenti");
                    Console.WriteLine("INSERIMENTO AVVENUTO CON SUCCESSO");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    //chiusura connessione
                    connection.Close();
                }
            }
        }
    }
}