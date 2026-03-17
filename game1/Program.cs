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
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----Добро пожаловать в сногсшибательную игру 'УльтраМегаГигаХроноРазлом: Эпоха Древних Богов и Великих Королевств----'!\n");
            Console.WriteLine("1.Игра");
            Console.WriteLine("2.Магазин");
            Console.WriteLine("3.Загрузить игру");
            Console.WriteLine("4.Сохранить и выйти");

                int firstChoise;
                if (int.TryParse(Console.ReadLine(), out firstChoise))
                {
                    Choise(firstChoise);
                }
        }
    }//9
}

public class Player
{
    // Свойства и характеристкики класса Player
    public string Name { get; set; } // Имя игрока
    public int Health { get; set; } // Текущее здоровье игрока
    public int MaxHealth { get; set; } // Максимальное здоровье игрока
    public int AttackPower { get; set; } // Сила атаки
    public int Gold { get; set; } // Количество золота
    public int Level { get; set; } // Текущий уровень
    public int Exp { get; set; } // Текущий опыт
    public int ExpToNextLevel { get; set; } // Опыта нужно для следующего уровня(для дальнейшей отрисовки прогрессбара лвла)

    // Конструктор, вызывается при создании нового игрока
    public Player(string name)
    {
        Name = name; // Присваивание имени
        MaxHealth = 100; // Устанавливаем максимально здоровье
        Health = MaxHealth; // Текущее здоровье приравниваем к максимальному
        AttackPower = 30;  // Базовая сила атаки
        Gold = 0; // Стартовое золото
        Level = 1; // Начальный уровень
        Exp = 0; // Начальное здоровье
        ExpToNextLevel = 100; // Для одного уровня нужно 100 опыта
    }
    // Метод атаки врага
    public void Attack(Enemy enemy)
    {


        //ДОБАВИЛ FRANIK747 70-85 & строка 93
        enemy.Name = "гоблин";
        enemy.drop = 100;
        enemy.drop_gold = 100;
        enemy.Damage= 10;
        enemy.HP= 100;
        enemy.replic = ["axaxахах","арг"];
        
        enemy.takedam(AttackPower); // Наносим урон противнику

        if ((enemy.HP < 0) || (enemy.HP == 0))
        {
            Console.WriteLine(enemy.replic[1]);
            Console.WriteLine("вы победили противника");
            enemy.Drop_exp_and_gold(Exp,Gold);
        }



    }
    // Метод получения урона от врага
    public void TakeDamage(int damage)
    {
        //хотелось бы и сюда реплики но строка 67 не разрешит, изменять пока не буду
        Health -= damage; // Уменьшаем здоровье на полученный урон
        if (Health < 0) Health = 0;  // Если здоровье ниже 0, присваиваем ему 0
    }

    // Метод смерти игрока
    public void die()
    {
        Health = 0; // Устанавливаем здоровье в 0
    }
    // Метод лечения игрока
    public void Heal(int amount)
    {
        Health += amount; // Увеличиваем здоровье на amount(на количество Hp) 
        if (Health > MaxHealth) // Если здоровье по итогу хилла будет больше чем максимальное
        {
            Health = MaxHealth; // То присваиваем здоровью максимально возможное
        }
    }
    public void LevelUp()
    {
        Level++;                               // Увеличиваем уровень на 1
        if (Level > 60) Level = 60;             // Максимальный уровень - 60

        MaxHealth += 10;                        // Увеличиваем максимальное здоровье на 10
        AttackPower += 2;                        // Увеличиваем силу атаки на 2
        Health = MaxHealth;                       // Восстанавливаем здоровье до максимума
        ExpToNextLevel = (int)(ExpToNextLevel * 1.5);  // Увеличиваем требуемый опыт в 1.5 раза

        // Выводим сообщение о повышении уровня
        Console.WriteLine($"УРОВЕНЬ ПОВЫШЕН! Теперь уровень {Level}!");
        Console.WriteLine($"Макс. здоровье: +10 | Атака: +2");
    }
    // Метод добавления опыта
    public void AddExp(int amount)
    {
        Exp += amount;                           // Добавляем полученный опыт
        Console.WriteLine($"+{amount} опыта!");   // Выводим сообщение

        // Проверяем, не повысился ли уровень несколько раз
        while (Exp >= ExpToNextLevel)              // Пока опыта достаточно для повышения
        {
            Exp -= ExpToNextLevel;                  // Вычитаем потраченный опыт
            LevelUp();                               // Повышаем уровень
        }
    }
    // Метод для отображения прогресс-бара опыта(потом допишу, пока что не придумал нормальную концепцию)
    public string GetExpBar()
    {

    }
}
public class Locations
{

}
public class Item
{
    public string Name;
    public int Price;
    public int Attack;
    public int Level;
    public float Weight;
    public Item(string name,int price,int attack,int level,float weight)
    {
        Name = name; // Название 
        Price = price; // Цена
        Attack= attack; // Сила Атаки
        Level = level; // Начальный Уровень
        Weight = weight; // вес
    }
}

public class Enemy
{
    //ДОБАВИЛ FRANIK747 151-168
    public string Name;
    public int HP;
    public int Damage;
    public int drop;
    public int drop_gold;
    public string[] replic;
    public int Drop_exp_and_gold(int a,int s)
    {
        s += drop_gold;
        a += drop;
        return a & s;
    }
    public int takedam(int a)
    {
        HP-=a;
        return HP;
    }
}

public class SaveSys
{
    private static string savePath = "savegame.txt";

    public static void SaveGame(Player player)
    {
        using (StreamWriter writer = new StreamWriter(savePath))
        {
            writer.WriteLine(player.Name);
            writer.WriteLine(player.Level);
            writer.WriteLine(player.Exp);
            writer.WriteLine(player.Gold);
            writer.WriteLine(player.Health);
            writer.WriteLine(player.MaxHealth);
            writer.WriteLine(player.AttackPower);
        }
        Console.WriteLine("Игра сохранена");
    }

    public static Player LoadGame()
    {
        if (!File.Exists(savePath))
            return null;

        using (StreamReader reader = new StreamReader(savePath))
        {
            string name = reader.ReadLine();
            int level = int.Parse(reader.ReadLine());
            int exp = int.Parse(reader.ReadLine());
            int gold = int.Parse(reader.ReadLine());
            int health = int.Parse(reader.ReadLine());
            int maxHealth = int.Parse(reader.ReadLine());
           int attackPower = int.Parse(reader.ReadLine());

            return new Player(name)
            {
                case 1:
                    StartNewGame();
                break;

                case 2:
                    if (currentPlayer != null);
                       // OpenShop();
                    else
                        Console.WriteLine("Сначала начните новую игру или загрузите сохранение!");
                break;
                
                case 3:
                   // LoadGame();
                break;

                case 4:
                    Console.WriteLine("Сохранить и выйти? y/n");
                    string exitChoise = Console.ReadLine();
                    if(exitChoise == "y")
                        {
                            SaveGame(); //дописать, какие переменные сохранять
                            Console.WriteLine("Твоя воля - закон, игра сохранена!");
                            Environment.Exit(0);
                        }
                    else
                    {
                        //написать, что будет при отказе выхода из игры
                    }
                break;
            }
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        static void StartNewGame()
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
