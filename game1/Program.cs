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



public class Item
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
       enemy.TakeDamage(AttackPower); // Наносим урон противнику
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
