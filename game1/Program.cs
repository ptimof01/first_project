using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.IO;

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
    
    public string Name { get; set; } 
    public int Health { get; set; } 
    public int MaxHealth { get; set; } 
    public int AttackPower { get; set; } 
    public int Gold { get; set; } 
    public int Level { get; set; } 
    public int Exp { get; set; } 
    public int ExpToNextLevel { get; set; } // Опыта нужно для следующего уровня(для дальнейшей отрисовки прогрессбара лвла)

  
    public Player(string name)
    {
        Name = name; 
        MaxHealth = 100; 
        Health = MaxHealth; 
        AttackPower = 30; 
        Gold = 0; 
        Level = 1; 
        Exp = 0; 
        ExpToNextLevel = 100; 
    }   
    
    public void Attack(Enemy enemy) 
    {
       enemy.TakeDamage(AttackPower); 
    }
    
    public void TakeDamage(int damage) 
    {
        Health -= damage; 
        if (Health < 0) Health = 0;  
    }
    
    public void die() 
    {
        Health = 0; 
    }
    
    public void Heal(int amount) 
    {
        Health += amount; 
        if (Health > MaxHealth) 
        {
            Health=MaxHealth; 
        }
    }
     public void LevelUp()
    {
                                       
        if (Level < 60)
        {
            Level++;                                       
            MaxHealth += 10;                              
            AttackPower += 2;                              
            Health = MaxHealth;                            
            ExpToNextLevel = (int)(ExpToNextLevel * 1.5);  

            
            Console.WriteLine($"УРОВЕНЬ ПОВЫШЕН! Теперь уровень {Level}!");
            Console.WriteLine($"Макс. здоровье: +10 | Атака: +2");
        }
        else  
        {
            Console.WriteLine($"У вас максимальный уровень!");
        }       
    }
    
    public void AddExp(int amount)
    {
        Exp += amount;                           
        Console.WriteLine($"+{amount} опыта!");   
        
        while (Exp >= ExpToNextLevel)              
        {
            Exp -= ExpToNextLevel;                 
            LevelUp();                             
        }
    }
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
    
    public Item(string name,int price,int attack,int level)
    {
        Name = name; 
        Price = price; 
        Attack= attack; 
        Level = level; 
        
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
    public int TakeDamage(int a)
    {
        HP-=a;
        return HP;
    }
}

public class SaveSys
{
    private static string savePath = "C:\\Users\\Academy\\AppData\\Local\\textRPG"; 

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