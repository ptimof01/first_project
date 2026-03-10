using Microsoft.VisualBasic;
static void Main()
{

    Console.WriteLine("----Добро пожаловать в сногсшибательную игру 'УльтраМегаГигаХроносАгония: Эпоха Древних Богов и Великих Королевств----'!\n");
    Console.WriteLine("1.игра");
    Console.WriteLine("2.магазин");
    Console.WriteLine("3.выйти из игры");
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