using System;

namespace Polizia
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            char key;

            Console.WriteLine("Benvenuto nel Database Agenti di Polizia!");
            Console.WriteLine();

            do
            {
                //Menù delle funzionalità disponibili per l'utente
                Console.WriteLine("Premi 1 - Visualizzare tutti gli Agenti");
                Console.WriteLine("Premi 2 - Agenti assegnati ad una determinata Area Metropolitana");
                Console.WriteLine("Premi 3 - Agenti con anni di servizio maggiori o uguali a quelli da voi indicati");
                Console.WriteLine("Premi 4 - Inserire un nuovo Agente");
                Console.WriteLine("Premi q - Uscire");

                //leggo la chiave della funzionalità selezionata dall'utente
                key = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (key)
                {
                    case '1':
                        //Visualizzo tutta la lista degli agenti
                        GestioneAgente.GetAllAgents();

                        Console.WriteLine("Premi un tasto per continuare");
                        Console.ReadKey();
                        break;
                    case '2':
                        //richiedo paramentro area in input all'utente
                        Console.WriteLine("Inserire Codice Area Metropolitana di interesse: ");
                        string area = Console.ReadLine();

                        //visualizzo gli agenti filtrati per area
                        GestioneAgente.GetAgentsByArea(area);

                        Console.WriteLine("Premi un tasto per continuare");
                        Console.ReadKey();
                        break;
                    case '3':
                        //richiedo paramentro anni in input all'utente
                        Console.WriteLine("Inserire il numero minimo di anni di servizio: ");
                        int anni = Int32.Parse(Console.ReadLine());

                        //visualizzo gli agenti filtrati per anni di servizio
                        GestioneAgente.GetAgentsByServizio(anni);

                        Console.WriteLine("Premi un tasto per continuare");
                        Console.ReadKey();
                        break;
                    case '4':
                        //Richiesta Parametri in input da console
                        Console.WriteLine("Nome nuovo Agente: ");
                        string Nome = Console.ReadLine();
                        Console.WriteLine("Cognome nuovo Agente: ");
                        string Cognome = Console.ReadLine();
                        Console.WriteLine("Codice fiscale nuovo Agente: ");
                        string CF = Console.ReadLine();
                        Console.WriteLine("Data di nascita nuovo Agente: (yyyy-mm-dd)");
                        DateTime DataNascita = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Numero anni di servizio nuovo Agente: ");
                        int AnniServizio = Int32.Parse(Console.ReadLine());

                        //Creo oggetti nuovo agente da inserire nel database
                        Agente newAgente = new Agente(Nome, Cognome, CF, DataNascita, AnniServizio);

                        //Chiamo la funzione che esegue l'insert dell'agente fornito
                        GestioneAgente.NewAgent(newAgente);

                        Console.WriteLine("Premi un tasto per continuare");
                        Console.ReadKey();
                        break;
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Riprovare.");
                        break;
                }
                Console.Clear();
            } while (!quit);
        }
    }
}