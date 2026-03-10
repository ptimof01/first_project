using Microsoft.VisualBasic;
using System;

static void Main()
{

    Console.WriteLine("----Добро пожаловать в сногсшибательную игру 'УльтраМегаГигаХроносАгония: Эпоха Древних Богов и Великих Королевств----'!\n");
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



public class Game
{
    
}

public class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public int ExpReward { get; set; }

            public Enemy(string name, int health, int attackPower, int expReward)
            {
                Name = name;
                Health = health;
                AttackPower = attackPower;
                ExpReward = expReward;
            }
             public void Attack(Player player) //бьёт игрок
            {
                player.TakeDamage(AttackPower);
            }
            public void TakeDamage(int damage) //моб получает урон
            {
                Health -= damage;
            }
            public bool IsAlive() //проверка жив ли моб
            {
                return Health<0;
            }
}
public class Player
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int AttackPower { get; set; }
    public int Gold { get; set; }
        public Player(string name)
    {
        Name = name;
        MaxHealth = 100;
        Health = MaxHealth;
        AttackPower = 30;
        Gold = 0;
    }   
    public void Attack(Enemy enemy) //атака игрока
    {
       enemy.TakeDamage(AttackPower);
    }
    public void TakeDamage(int damage) //получение урона игроком
    {
        Health -= damage;
    }
    public void die() //смэрть игрока
    {
        Health = 0;
    }
    public void Heal(int amount) //отхил игрока
    {
        Health += amount;
        if (Health > MaxHealth)
        {
            Health=MaxHealth;
        }
    }
}
public class Item
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int Effect { get; set; }
    
        public Item(string name, int price, int effect)
    {
        Name = name;
        Price = price;
        Effect = effect;
    }
}
