using System;
using System.IO;
using vano123123;
using ptimof01;
using delta;
using bobeko;
using System.Security.Cryptography.X509Certificates;
using Franik747;

namespace pwned
{
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
    
        public void Attack(Franik747.Enemy enemy) 
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
            //public string GetExpBar()
            //{
       
            //}
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
    
        public static Player? LoadGame() 
{
    if (!File.Exists(savePath))
    {
        return null;  
    }
    
    using (StreamReader reader = new StreamReader(savePath))
    {
       
        string? name = reader.ReadLine();
        string? levelStr = reader.ReadLine();
        string? expStr = reader.ReadLine();
        string? goldStr = reader.ReadLine();
        string? healthStr = reader.ReadLine();
        string? maxHealthStr = reader.ReadLine();
        string? attackPowerStr = reader.ReadLine();
        
       
        if (name == null || levelStr == null || expStr == null || goldStr == null || 
            healthStr == null || maxHealthStr == null || attackPowerStr == null)
        {
            Console.WriteLine("Ошибка: файл сохранения поврежден");
            return null;
        }
        
        
        if (!int.TryParse(levelStr, out int level) ||
            !int.TryParse(expStr, out int exp) ||
            !int.TryParse(goldStr, out int gold) ||
            !int.TryParse(healthStr, out int health) ||
            !int.TryParse(maxHealthStr, out int maxHealth) ||
            !int.TryParse(attackPowerStr, out int attackPower))
        {
            Console.WriteLine("Ошибка: неверный формат данных в файле сохранения");
            return null;
        }
        
        
        if (string.IsNullOrEmpty(name))
        {
            name = "Безымянный";
        }
        
        Player player = new Player(name);
        player.Level = level;
        player.Exp = exp;
        player.Gold = gold;
        player.Health = health;
        player.MaxHealth = maxHealth;
        player.AttackPower = attackPower;
        
        return player;
    }
}
    }
}