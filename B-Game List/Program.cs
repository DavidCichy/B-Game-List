using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGameList
{

    class GameList
    {
        public static void SaveGameList(Game[] log, int ll)
        {
            StreamWriter f = new StreamWriter("gamelist.dat");
            f.WriteLine("{0}", ll);
            for (int i = 0; i < ll; i++)
            {
                f.WriteLine("{0}", log[i].GameNumber);
                f.WriteLine("{0}", log[i].GameName);
                f.WriteLine("{0}", log[i].Notes);
                f.WriteLine("{0}", log[i].AdditionDate);
                // zmienić oddzielanie danych tabeli w pliku - średniki zamiast linii
            }
            f.Close();
        }
        public static int LoadGameListLenght()
        {
            StreamReader sr = new StreamReader("gamelist.dat");
            int a = Convert.ToInt32(sr.ReadLine());
            sr.Close();
            return a;
        }
        public static Game[] LoadGameList()
        {
            StreamReader sr = new StreamReader("gamelist.dat");
            Game[] log = new Game[499];
            int ll = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < ll; i++)
            {
                log[i].GameNumber = Convert.ToInt32(sr.ReadLine());
                log[i].GameName = sr.ReadLine(); 
                log[i].Notes = sr.ReadLine();
                log[i].AdditionDate = sr.ReadLine();
            }
            sr.Close();
            return log;
        }
        public static void ListGames(Game[] list, int ll)
        {
            for (int i = 0; i < ll; i++)
            {
                Console.Write("{0}) \"{1}\"", list[i].GameNumber, list[i].GameName);
                if (list[i].Notes != "")
                {
                    Console.Write(" - {0}", list[i].Notes);
                }
                Console.Write(" ({0})\n", list[i].AdditionDate);
            }
        }
        public struct Game
        {
            public int GameNumber;
            public string GameName;
            public string Notes;
            public string AdditionDate;
    }

        static void Main(string[] args)
        {
            string c = "";
            string r = "";
            int list_lenght = 0;
            string add;
            Game[] log = new Game[499]; //debug i optymalizacja - robić coraz większe tabele
            if (File.Exists("gamelist.dat"))
                { 
                list_lenght = LoadGameListLenght();
                Console.WriteLine("Games imported: {0}", list_lenght);
                log = LoadGameList();
                Console.ReadKey();}
            bool newGame = true;


            while (c != "0" && c.ToLower() != "e")
            {
                Console.Clear();
                Console.WriteLine("1. (L)ist of games\n2. (A)dd New game\n3. (R)emove Game\n0. (E)xit\n\n");
                Console.Write("Command: ");
                c = Console.ReadLine();
                Console.Clear();
                if (c == "1" || c.ToLower() == "l")
                {
                    Console.WriteLine("GAME LIST:\n");
                    GameList.ListGames(log, list_lenght);
                    Console.WriteLine("\nEnd of list. Press any key to continue.");
                    Console.ReadKey();

                }
                else if (c == "2" || c.ToLower() == "a")
                {
                    newGame = true;
                    Console.Write("ADDING GAME\nTitle: ");
                    add = Console.ReadLine();
                    for (int i = 0; i <= list_lenght; i++)
                    {
                        if (add == log[i].GameName)
                        {
                            Console.WriteLine("This title is already on the list. Press any key to continue.");
                            Console.ReadLine();
                            newGame = false;
                        }

                    }
                    if (newGame == true)
                    {
                        log[list_lenght].GameName = add;
                        log[list_lenght].AdditionDate = Convert.ToString(DateTime.Today); //poprawić formatowanie daty
                        Console.Write("Notes: ");
                        log[list_lenght].Notes = Console.ReadLine();
                        log[list_lenght].GameNumber = list_lenght + 1;
                        list_lenght++;
                    }   
                    Array.Sort<Game>(log, (a, b) => a.GameName == null ? 1 : b.GameName == null ? -1 : a.GameName.CompareTo(b.GameName));
                    for (int i = 0; i <= list_lenght; i++)
                    {
                        log[i].GameNumber = i+1;
                    }
                    SaveGameList(log, list_lenght);

                }
                else if (c == "3" || c.ToLower() == "r")
                {
                    Console.WriteLine("GAME LIST:\n");
                    GameList.ListGames(log, list_lenght);
                    Console.Write("\nREMOVING GAME. Select a number or type 0 to exit. ");
                    r = Console.ReadLine();

                    for (int i = 0; i < list_lenght; i++)
                    {
                        if (r == log[i].GameNumber.ToString())
                        {
                            for (int j = i; j < list_lenght; j++)
                            {
                                if (log[j + 1].GameName != "")
                                {
                                    log[j] = log[j + 1];
                                    log[j].GameNumber--;
                                }
                            }
                            list_lenght--;
                        }
                    };
                    Console.WriteLine("\nUPDATED GAME LIST:\n");
                    GameList.ListGames(log, list_lenght);
                    SaveGameList(log, list_lenght);
                    Console.ReadKey();
                }
                else if (c != "0" && c.ToLower() != "e")
                {
                    Console.WriteLine("Incorrect command.");
                }
            }
            Console.Write("Thank you for using B-Game List. Application is closing.");
            Console.ReadKey();
        }
    }
}
// nieograniczony array