using System;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Quic;
using System.IO;
using System.ComponentModel.DataAnnotations;

class Program
{

    // MENU 
    static void Main()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        int opcio = 1;
        bool seguir = true; // Si no es la opció sortir, torna a executar el menú
        while (seguir)
        {
            Menu(opcio); // Cada cop que cliques una tecla, torna a imprimir el menú, pero amb els canvis dins de el metode
            ConsoleKeyInfo tecla = Console.ReadKey(); // Detecta les feltxetes
            Thread.Sleep(1);
            switch (tecla.Key)
            {
                case ConsoleKey.UpArrow: // Si clica la fletxa de adalt li resta 1 a la ubicació
                    if (opcio > 1)
                    {
                        opcio = opcio - 1;
                    }
                    else opcio = 7;

                    break;
                case ConsoleKey.DownArrow: // El mateix al reves
                    if (opcio < 7)
                    {
                        opcio = opcio + 1;
                    }
                    else opcio = 1;

                    break;
                case ConsoleKey.LeftArrow:
                    if (opcio > 1)
                    {
                        opcio = opcio - 1;
                    }
                    else opcio = 7;

                    break;
                case ConsoleKey.RightArrow:
                    if (opcio < 7)
                    {
                        opcio = opcio + 1;
                    }
                    else opcio = 1;

                    break;
                case ConsoleKey.Enter: // Quan li dona a la tecla enter, executa el switch que segons la ubicació on estaba executa la opcio.
                    Switch(opcio);
                    if (opcio == 7) seguir = false; // En cas de que sigui 9 la opcio, es a dir, sortir, la variable seguir es converteix en fals, així que ja sortira del programa.
                    break;

            }
        }
    }

    // INTERFÍCIE MENU
    static void Menu(int opcio)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("┌───────────────────────────────────┐");
        Console.WriteLine("│               AGENDA              │");
        Console.WriteLine("│┌─────────────────────────────────┐│");
        for (int i = 0; i < 8; i++) // Imprimeix un a un les opcions.
        {
            if (i == opcio) // En cas de que la posició de on va a executar es la mateixa que el WriteLine que esta imprimin, canvia el fons y color de lletra per així que sembli que es el que esta seleccionant.
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
            }

            switch (i) // Imprimeix un a un
            {
                case 1:
                    Console.WriteLine("││        Donar Alta Usuari        ││");
                    break;
                case 2:
                    Console.WriteLine("││        Recuperar Usuari         ││");
                    break;
                case 3:
                    Console.WriteLine("││        Modificar Usuari         ││");
                    break;
                case 4:
                    Console.WriteLine("││         Eliminar Usuari         ││");
                    break;
                case 5:
                    Console.WriteLine("││          Mostra Agenda          ││");
                    break;
                case 6:
                    Console.WriteLine("││         Ordenar Agenda          ││");
                    break;
                case 7:
                    Console.ResetColor(); // Hi ha un espai de mes per un tema estetic y reinicio el color al normal per així que al seleccionar la opció de sortir deixi en vuit la linea de entre mig
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("││                                 ││");
                    if (opcio == 7)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("││          Sortir Menú            ││");
                    break;
            }

            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        Console.WriteLine("│└─────────────────────────────────┘│");
        Console.WriteLine("└───────────────────────────────────┘");
    }

    // BUSCA EL METODE SEGONS EL QUE LI HAGIS DONAT
    static void Switch(int opcio)
    {
        Console.Clear();
        switch (opcio)
        {
            case 7:
                Console.ResetColor();
                break;
            case 1:
                Capçalera();
                Alta();
                Thread.Sleep(500); // Espera 4 segons avans de tornar al menú
                Console.Clear();
                break;
            case 2:
                Capçalera();
                Recuperar();
                Thread.Sleep(5000);
                Console.Clear();
                break;
            case 3:
                Capçalera();
                Modificar();
                Thread.Sleep(500);
                Console.Clear();
                break;
            case 4:
                Capçalera();
                Eliminar();
                Thread.Sleep(500);
                Console.Clear();
                break;
            case 5:
                Capçalera();
                MostrarAgenda();
                Thread.Sleep(5000);
                Console.Clear();
                break;
            case 6:
                Capçalera();
                OrdenarAgenda();
                Console.Clear();
                break;
        }
    }

    // METODE PER ENTRAR DADA 
    static string IntroduirValor() // Aixo es un métode el cual demana un valor y el retorna al switch
    {
        string valor = Console.ReadLine();
        return valor;
    }
    // METODE PER ENTRAR DADA
    static int IntroduirInt() // Aixo es un métode el cual demana un valor y el retorna al switch
    {
        int valor = Convert.ToInt32(Console.ReadLine());
        return valor;
    }

    // CAPÇALERA
    static void Capçalera() // Es una capçalera per que quedi bonic =)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black; 
        Console.WriteLine("\n┌────────────────────────┐");
        Console.WriteLine("│         AGENDA         │");
        Console.WriteLine("└────────────────────────┘\n");
        Console.ResetColor();
    }
    // DONAR ALTA (POS1)
    static void Alta() 
    {
        using (StreamWriter SW = new StreamWriter("agenda.txt",true))
        {

            string nom, cognom1, cognom2, DNI, telefon, data, correu;
        bool error = false;

        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        do
        {
            error = false;
            Console.Write("Nom: ");
            nom = IntroduirValor();
            error = PrimeraMaj(nom, error);
        } while (error);
        do
        {
            error = false;
            Console.Write("Primer cognom: ");
            cognom1 = IntroduirValor();
            error = PrimeraMaj(cognom1, error);
        } while (error); 
        do
        {
            error = false;
            Console.Write("Segon cognom: ");
            cognom2 = IntroduirValor();
            error = PrimeraMaj(cognom2, error);
        } while (error);
        do
        {
            error = false;
            Console.Write("DNI: ");
            DNI = IntroduirValor();
            error = DNIvalidar(DNI, error);

        } while (error);
        do
        {
            error = false;
            Console.Write("Teléfon: ");
            telefon = IntroduirValor();
            error = TelValidar(telefon, error);
        } while (error);
        do
        {
            error = false;
            Console.Write("Data Naixement: ");
            data = IntroduirValor();
            data = DataValidar(data);
        } while (error);
        do
        {
            error = false;
            Console.Write("Correu Electrónic: ");
            correu = IntroduirValor();
            error = CorreuValidar(correu, error);
        } while (error) ;

        //DateTime datanaix = new DateTime(Convert.ToInt32(data));
        SW.WriteLine(nom + ";" + cognom1 + ";" + cognom2 + ";" + DNI + ";" + telefon + ";" + data + ";" + correu);
        }
    }

    static void Recuperar()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        string nom, linea;
        bool error = false, trobat = false;

        do
        {
            do
            {
                Console.Write("Insereix Usuari: ");
                nom = IntroduirValor();
                error = PrimeraMaj(nom, error);
            } while (error);
            linea = TrobarUsuari(nom);
            if (linea != "")
            {
                 Console.WriteLine($"Nom: {linea.Substring(0,linea.IndexOf(";"))}");
                 linea = Retallar(linea);
                 Console.WriteLine($"Primer Cognom: {linea.Substring(0, linea.IndexOf(";"))}");
                 linea = Retallar(linea);
                 Console.WriteLine($"Segon Cognom: {linea.Substring(0, linea.IndexOf(";"))}");
                 linea = Retallar(linea);
                 Console.WriteLine($"DNI: {linea.Substring(0, linea.IndexOf(";") )}");
                 linea = Retallar(linea);
                 Console.WriteLine($"Telefon: {linea.Substring(0, linea.IndexOf(";"))}");
                 linea = Retallar(linea);
                 Console.WriteLine($"Data Naixement: {linea.Substring(0, linea.IndexOf(";"))}");
                 linea = Retallar(linea);
                 Console.WriteLine($"Correu: {linea}");;
                 trobat = true;
             }

            if (!trobat)
            {
                Console.WriteLine("No s'ha trobat, vols provar un altre cop?");
                string tornar = Console.ReadLine();
                if (tornar == "Si" || tornar == "si")
                {
                    trobat = false;
                }
                else
                {
                    trobat = true;
                }
            }
        } while (!trobat);
    }
    static void Modificar()
    {
        string agenda = "", linea, nom, dada, lineaAux, lineaAux1="", Aux1linea = "";
        int cas, i = 0;
        bool error;
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        do
        {
            do
            {
                error = false;
                Console.Write("Insereix Usuari: ");
                nom = IntroduirValor();
                error = PrimeraMaj(nom, error);
            } while (error);

            linea = TrobarUsuari(nom);
            Console.WriteLine(linea.Replace(";", "   "));
        } while (linea == "");
        
        lineaAux = linea;
        Aux1linea = linea;
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("  1. Nom               ");
        Console.WriteLine("  2. Primer Cognom     ");
        Console.WriteLine("  3. Segon Cognom      ");
        Console.WriteLine("  4. DNI               ");
        Console.WriteLine("  5. Telefon           ");
        Console.WriteLine("  6. Data Naixement    ");
        Console.WriteLine("  7. Correu Electrónic ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write("\n Quina vols modificar:");
        cas = IntroduirInt();
        switch (cas)
        {
            case 1:
                do
                {
                    error = false;
                    Console.Write("Nom: ");
                    dada = IntroduirValor();
                    error = PrimeraMaj(dada, error);
                    lineaAux1 = linea;
                } while (error);
                linea = dada + linea.Substring(linea.IndexOf(";"));
                break;
            case 2:
                do
                {
                    error = false;
                    Console.Write("Primer Cognom: ");
                    dada = IntroduirValor();
                    error = PrimeraMaj(dada, error);
                    lineaAux1 = linea;
                } while (error);
                linea = "";
                while (i < 1)
                {

                    linea += Aux1linea.Substring(0, Aux1linea.IndexOf(";") + 1);
                    Aux1linea = Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                    i++;
                }
                linea += dada + ";";
                linea += Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                break;
            case 3:
                do
                {
                    error = false;
                    Console.Write("Segon Cognom: ");
                    dada = IntroduirValor();
                    error = PrimeraMaj(dada, error);
                    lineaAux1 = linea;
                } while (error);
                linea = "";
                while (i < 2)
                {
                    
                    linea += Aux1linea.Substring(0, Aux1linea.IndexOf(";") + 1);
                    Aux1linea = Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                    i++;
                }
                linea += dada + ";";
                linea += Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                break;
            case 4:
                do
                {
                    error = false;
                    Console.Write("DNI: ");
                    dada = IntroduirValor();
                    error = DNIvalidar(dada, error);
                    lineaAux1 = linea;
                } while (error);
                linea = "";
                while (i < 3)
                {

                    linea += Aux1linea.Substring(0, Aux1linea.IndexOf(";") + 1);
                    Aux1linea = Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                    i++;
                }
                linea += dada + ";";
                linea += Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                break;
            case 5:
                do
                {
                    error = false;
                    Console.Write("Telefon: ");
                    dada = IntroduirValor();
                    error = TelValidar(dada, error);
                    lineaAux1 = linea;
                } while (error);
                linea = "";
                while (i < 4)
                {

                    linea += Aux1linea.Substring(0, Aux1linea.IndexOf(";") + 1);
                    Aux1linea = Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                    i++;
                }
                linea += dada + ";";
                linea += Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                break;
            case 6:
                do
                {
                    error = false;
                    Console.Write("Data Naixement: ");
                    dada = IntroduirValor();
                    dada = DataValidar(dada);
                    lineaAux1 = linea;
                } while (error);
                linea = "";
                while (i < 5)
                {

                    linea += Aux1linea.Substring(0, Aux1linea.IndexOf(";") + 1);
                    Aux1linea = Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                    i++;
                }
                linea += dada + ";";
                linea += Aux1linea.Substring(Aux1linea.IndexOf(";") + 1);
                break;
            case 7:
                do
                {
                    error = false;
                    Console.Write("Correu Electronic: ");
                    dada = IntroduirValor();
                    error = CorreuValidar(dada, error);
                    lineaAux1 = linea;
                } while (error);
                linea = linea.Substring(0, linea.LastIndexOf(";") +1 ) + dada;
                break;
        }

        using (StreamReader SR = new StreamReader("agenda.txt", true))
        {
            while (!SR.EndOfStream)
                agenda = SR.ReadLine();
        }
        ModLinea(linea, lineaAux1);
    } 
    static void Eliminar()
    {
        string agenda = "", linea, nom = "";
        bool error;
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        do
        {
            do
            {
                error = false;
                Console.Write("Insereix Usuari: ");
                nom = IntroduirValor();
                error = PrimeraMaj(nom, error);
            } while (error);

            linea = TrobarUsuari(nom);
        } while (linea == "");

        using (StreamReader SR = new StreamReader("agenda.txt", true))
        {
            while (!SR.EndOfStream)
                agenda = SR.ReadLine();
        }
        EliminarLinea(linea);
    }
    //static void MostrarAgenda();
    
    static void MostrarAgenda()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        List <string>llista = Llista(); //Ho faré amb List, que serveix per guardar una sequencia de cadenes,
                      //i es més fàcil de gestionar, aqui truco al métode que afegira els valors a la llista.
        // Ordena la agenda
        llista.Sort();

        // Mostro linea a linea la llista
        Console.WriteLine("Llista ordenada dels usuaris:\n");
        foreach (var usuari in llista) // Foreach ho que fa es mostrar totes les cadenes de la llista
        {
            Console.WriteLine(usuari.Replace(";", " "));
        }

    }
    static void OrdenarAgenda()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        List<string> llista = Llista(); //Ho faré amb List, que serveix per guardar una sequencia de cadenes,
                                        //i es més fàcil de gestionar, aqui truco al métode que afegira els valors.
                                        // Ordena la agenda
        llista.Sort();
        OrdenarEsc(llista); //Truco al métode que escriura la llista a la agenda
    }

    static List<string> Llista()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;

        List<string> llista = new List<string>();

        //  Afegeixo totes les líneas de la agenda a la llista.
        using (StreamReader SR = new StreamReader("agenda.txt"))
        {
            while (!SR.EndOfStream)
            {
                llista.Add(SR.ReadLine()); // Es fa amb la comanda .add, el qual
            }                              // afegeix ho que llegeix com a una
        }                                  // cadena.
        return llista;
    }

    static void OrdenarEsc(List<string> llistaUsuaris)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        // Escric la llista ordenada al arxiu.
        using (StreamWriter SW = new StreamWriter("agenda.txt"))
        {
            foreach (var usuari in llistaUsuaris) // Per cada cadena fa el que hi ha dins,
            {                                     // que es escriura la cadena al arxiu.
                SW.WriteLine(usuari);
            }
        }
    }

     
    static void EliminarLinea(string linea)
    {
        string lineAux = "", agenda = "";
        using (StreamReader SR = new StreamReader("agenda.txt", true))
        {
            lineAux = SR.ReadLine();
            while (!SR.EndOfStream)
            {
                lineAux = SR.ReadLine();
                if (linea.Substring(0, linea.IndexOf(";")) != lineAux.Substring(0, lineAux.IndexOf(";")))
                {
                    agenda += lineAux + '\r';
                }
            }
        }
        File.WriteAllText(@"agenda.txt", string.Empty);
        using (StreamWriter SW = new StreamWriter("agenda.txt", true))
        {
            SW.Write(agenda);
        }
    }

    static void ModLinea(string linea, string lineaAux1)
    {
        string lineAux = "", agenda = "";
        using (StreamReader SR = new StreamReader("agenda.txt", true))
        {
            while (!SR.EndOfStream)
            {
                lineAux = SR.ReadLine();
                if (lineaAux1.Substring(0,lineaAux1.IndexOf(";")) != lineAux.Substring(0, lineAux.IndexOf(";")))
                {
                    agenda += lineAux + '\r';
                }
            }
            agenda += linea;

        }
        File.WriteAllText(@"agenda.txt", string.Empty);
        using (StreamWriter SW = new StreamWriter("agenda.txt", true))
        {
            SW.Write(agenda);
        }
    }

    static string Retallar(string linea)
    {
        linea = linea.Substring(linea.IndexOf(";") + 1);
        return linea;
    }
    static bool PrimeraMaj(string nom, bool error)
    {
        ;
        if (nom.Substring(0,1) != nom.Substring(0, 1).ToUpper())
        {
            error = true;
        }
        if (error) Console.WriteLine("HAS INTRODUÏT INCORRECTAMENT LES DADES");
        return error;
    }
    static bool DNIvalidar(string DNI, bool error)
    {
        if (DNI.Length != 9)
        {
            error = true;
        }
        else if(!Regex.IsMatch(DNI.Substring(0,7), @"^\d+$") && !Regex.IsMatch(DNI.Substring(8), @"[^@\s]"))
        {
            error = true;
        }
        if (error) Console.WriteLine("HAS INTRODUÏT INCORRECTAMENT LES DADES");
        return error;
    }
    static bool TelValidar(string telefon, bool error)
    {
        if (telefon.Length != 9)
        {
            error = true;
        }
        else if (!Regex.IsMatch(telefon,@"^\d+$"))
        {
            error = true;
        }
        if (error) Console.WriteLine("HAS INTRODUÏT INCORRECTAMENT LES DADES");
        return error;
    }
      static string DataValidar(string data)
      {
        DateTime dataAct = DateTime.Now;
        data = data.Replace("/", "");
        if (DateTime.TryParseExact(data, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataType))
        {
            int edat = dataAct.Year - dataType.Year;
            if (dataAct.Month < dataType.Month || (dataAct.Month == dataType.Month && dataAct.Day < dataType.Day))
                edat--;
            Console.WriteLine($"Tens {edat} anys");
        }

        return dataType.ToString("dd/MM/yyyy");
      }
    static bool CorreuValidar(string correu, bool error)
    {
        ;
        if (!Regex.IsMatch(Convert.ToString(correu), @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"))
        {
            error = true;
        }
        if (error) Console.WriteLine("HAS INTRODUÏT INCORRECTAMENT LES DADES");
        return error;
    }
    static string TrobarUsuari(string nom)
    {
        string linea, lineaAux = "";
        bool trobat = false;
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        
        using (StreamReader SR = new StreamReader("agenda.txt"))
        {
            nom = nom.Replace(" ", ";");
            while (!SR.EndOfStream)
            {
                linea = SR.ReadLine();
                if (linea.Contains(nom))
                {
                    trobat = true;
                    lineaAux = linea;
                }
            }
            if(!trobat) Console.WriteLine("No hi ha cap usuari amb aquest nom");
        }
        return lineaAux;
    }
}