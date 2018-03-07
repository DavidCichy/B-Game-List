using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameListApp
{

    class GameList
    {
        public static void ListGames(Game[] list, int ll)
        {
            for (int i = 0; i < ll; i++)
            {
                Console.Write("{0}) \"{1}\"", list[i].GameNumber, list[i].GameName);
                if (list[i].Notes != "")
                {
                    Console.Write(" - {0}", list[i].Notes);
                }
                Console.Write("({0})\n", list[i].AdditionDate);
            }
        }
        public struct Game
        {
            public int GameNumber;
            public string GameName;
            public string Notes;
            public DateTime AdditionDate;
        }

        static void Main(string[] args)
        {
            string c = "";
            string r = "";
            int list_lenght = 0;
            string add;
            Game[] log = new Game[499];
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
                    /*
                    for (int i = 0; i < list_lenght; i++)
                    {
                        Console.Write("{0}) \"{1}\"", log[i].GameNumber, log[i].GameName);
                        if (log[i].Notes != "")
                        {
                            Console.Write(" - {0}", log[i].Notes);
                        }
                        Console.Write("({0})\n", log[i].AdditionDate);
                    }*/
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
                        log[list_lenght].AdditionDate = DateTime.Today;
                        Console.Write("Notes: ");
                        log[list_lenght].Notes = Console.ReadLine();
                        log[list_lenght].GameNumber = list_lenght + 1;
                        list_lenght++;
                    }

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
                        //dodać wyjątek jak nie ma numeru/podanej danej na liście
                    };
                    Console.WriteLine("\nGAME LIST:\n");
                    GameList.ListGames(log, list_lenght);
                    Console.ReadKey();
                    //if (r == "1" || c.ToLower() == "l")
                    //{
                    //}
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
