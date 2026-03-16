using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.IO;
using TextRPG;
using pwned;
using vano123123;
using Franik747;
using ptimof01;
using delta;
using bobeko;
using System.Security.Cryptography.X509Certificates;



namespace TextRPG
{
 class Program
 {
    static void Main()
    {
        Console.WriteLine("----Добро пожаловать в сногсшибательную игру 'УльтраМегаГигаХроноРазлом: Эпоха Древних Богов и Великих Королевств----'!\n");
        Console.WriteLine("1.Игра");
        Console.WriteLine("2.Магазин");
        Console.WriteLine("3.Выйти из игры");
        int firstChoise = Convert.ToInt32(Console.ReadLine());
        Choise(firstChoise);

        static void Choise(int choise)
        {
            switch (choise)
            {
                case 1:
                
                break;

                case 2:

                break;

                case 3:
                    Console.WriteLine("Уверен? y/n");
                    string exitChoise = Console.ReadLine();
                    if(exitChoise == "y")
                        {
                            Console.WriteLine("Твоя воля - закон!");
                            Environment.Exit(0);
                        }
                    break;
            }
        }
        
    }
 }
}

