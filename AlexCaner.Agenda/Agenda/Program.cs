using System;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Drawing;

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
                Thread.Sleep(4000); // Espera 4 segons avans de tornar al menú
                Console.Clear();
                break;
            case 2:
                Capçalera();

                Thread.Sleep(4000);
                Console.Clear();
                break;
            case 3:
                Capçalera();

                Thread.Sleep(4000);
                Console.Clear();
                break;
            case 4:
                Capçalera();

                Thread.Sleep(4000);
                Console.Clear();
                break;
            case 5:
                Capçalera();

                Thread.Sleep(4000);
                Console.Clear();
                break;
            case 6:
                Capçalera();

                Thread.Sleep(4000);
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
    static bool Error(error) // Es una capçalera per que quedi bonic =)
    {
        bool error = true;
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Has escrit malament la dada");
        return error;

        
    }

    // DONAR ALTA (POS1)
    static void Alta() 
    {
        bool error = false;

        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        string nom;
        while (error == false)
        {
            Console.Write("Nom: ");
            nom = IntroduirValor();
            if()
        }

       

    }

}