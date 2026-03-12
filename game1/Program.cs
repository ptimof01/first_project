using System;
using System.Collections.Generic;
using System.Threading;
//переписать main с использованием сохранений, просто перетыкать сверху меню загрузки. 1-новое приключение, 2-загрузка персонажа.  // player = SaveSys.LoadGame(); \\
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
       //enemy.TakeDamage(AttackPower); // Наносим урон противнику
    }
    // Метод получения урона от врага
    public void TakeDamage(int damage) 
    {
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
            Health=MaxHealth; // То присваиваем здоровью максимально возможное
        }
    }
     public void LevelUp()
    {
                                       
        if (Level < 60)
        {
            Level++;                                       // Увеличиваем уровень на 1
            MaxHealth += 10;                               // Увеличиваем максимальное здоровье на 10
            AttackPower += 2;                              // Увеличиваем силу атаки на 2
            Health = MaxHealth;                            // Восстанавливаем здоровье до максимума
            ExpToNextLevel = (int)(ExpToNextLevel * 1.5);  // Увеличиваем требуемый опыт в 1.5 раза

            // Выводим сообщение о повышении уровня
            Console.WriteLine($"УРОВЕНЬ ПОВЫШЕН! Теперь уровень {Level}!");
            Console.WriteLine($"Макс. здоровье: +10 | Атака: +2");
        }
        else  // Максимальный уровень - 60
        {
            Console.WriteLine($"У вас максимальный уровень!");
        }       

        

        
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
    //public string GetExpBar()
    //{
       
    //}
}
public class Locations
{
    
}
public class Item
{
    
}

public class Enemy
{
    
}

public class SaveSys
{
    private static string savePath = "savegame.txt"; 

    public static void SaveGame(Player player)
    {
        using(StreamWriter writer = new StreamWriter(savePath))
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
        
        using(StreamReader reader = new StreamReader(savePath)) 
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
                Level = level,
                Exp = exp,
                Gold = gold,
                Health = health,
                MaxHealth = maxHealth,
                AttackPower = attackPower
            };
        }
    }
}