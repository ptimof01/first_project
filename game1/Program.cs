using Microsoft.VisualBasic;


Console.WriteLine("Добро пожаловать в сногсшибательную игру 'УльтраМегаГигаХроносАгония: Эпоха Древних Богов и Великих Королевств'!\n");
Console.WriteLine("1.игра");
Console.WriteLine("2.магазин");
Console.WriteLine("3.выйти из игры");
int choise = Convert.ToInt32(Console.ReadLine());

class Game
{
    
}

public class Enemy
{
    public string Name {get; set;}
    public int Health{get; set;}
    public int AttackPower{get; set;}
    public int ExpReward{get; set;}

            public Enemy(string name, int health, int attaclpower, int expreward)
            {
                
                
            }
public class Player
{
    public string Name{get; set;}
    public int Health {get; set;}
    public int MaxHealth{get; set;}
    public int AttackPower{get; set;}
    public int Exp;
        public Player(string name)
    {
        Health=MaxHealth;
        AttackPower=30;
        Exp=0;
        Name=name;
    }   
    public void Attack(Enemy enemy)
    {
       
    }
}
}
//TAKEDMG DIE HEAL 