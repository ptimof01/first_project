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
    static Player currentPlayer = null;
    static void Main()
    { while (true)
        {
            Console.Clear();
            Console.WriteLine("----Добро пожаловать в сногсшибательную игру 'УльтраМегаГигаХроноРазлом: Эпоха Древних Богов и Великих Королевств----'!\n");
            Console.WriteLine("1.Игра");
            Console.WriteLine("2.Магазин");
            Console.WriteLine("3.Загрузить игру");
            Console.WriteLine("4.Выйти из игры");

                int firstChoise;
                if (int.TryParse(Console.ReadLine(), out firstChoise))
                {
                    Choise(firstChoise);
                }
        }
    }
        
        static void Choise(int choise)
        {
            switch (choise)
            {
                case 1:     StartNewGame();
                break;

                case 2:if (currentPlayer != null);
                       // OpenShop();
                    else
                        Console.WriteLine("Сначала начните новую игру или загрузите сохранение!");
                break;
                

                case 3:
                   // LoadGame();
                break;

                case 4:
                    Console.WriteLine("Уверен? y/n");
                    string exitChoise = Console.ReadLine();
                    if(exitChoise == "y")
                        {
                            Console.WriteLine("Твоя воля - закон!");
                            Environment.Exit(0);
                        }break;
            }
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }   static void StartNewGame()
        {
            Console.Clear();
            Console.Write("Введите имя:");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
                name = "OPK";

            currentPlayer = new Player(name);
            Console.WriteLine($"\nДобро пожаловать, {currentPlayer.Name}!");
            Console.WriteLine($"Здоровье: {currentPlayer.Health}/{currentPlayer.MaxHealth}");
            Console.WriteLine($"Атака: {currentPlayer.AttackPower}");
            Console.WriteLine($"Уровень: {currentPlayer.Level}");
        }
    }   
}      