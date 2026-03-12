using System;
using System.Collections.Generic;

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
{
    Console.WriteLine("----Добро пожаловать в сногсшибательную игру 'УльтраМегаГигаХроносАгония: Эпоха Древних Богов и Великих Королевств----'!\n");

    Console.Write("Введите имя вашего героя: ");
    string? playerNameInput = Console.ReadLine();
    string playerName = string.IsNullOrWhiteSpace(playerNameInput) ? "Безымянный" : playerNameInput;

    Player player = new Player(playerName);
    Console.WriteLine($"\nДобро пожаловать, {player.Name}!");

    List<Location> allLocations = Locations.LocationsList;
    Location currentLocation = allLocations[0];

    bool gameRunning = true;
    while (gameRunning && player.Health > 0)
    {
        Console.Clear();
        Console.WriteLine($"\n╔═══════════════════════════════════════╗");
        Console.WriteLine($"║  {player.Name,-25} LVL: {player.Level,-5} ║");
        Console.WriteLine($"║  HP: {player.Health}/{player.MaxHealth,-5} Золото: {player.Gold,-5} ║");
        Console.WriteLine($"║  Опыт: {player.GetExpBar(18)} ║");  // Прогресс-бар опыта
        Console.WriteLine($"╚═══════════════════════════════════════╝");
        Console.WriteLine($"\nТекущая локация: {currentLocation.Name}");
        Console.WriteLine($"\n1. Исследовать локацию");
        
        if (currentLocation.Type == LocationType.City)
            Console.WriteLine("2. Посетить город");
        else
            Console.WriteLine("2. Вернуться в город");
            
        Console.WriteLine("3. Выбрать другую локацию");
        Console.WriteLine("4. Выйти из игры");
        Console.WriteLine("\n--- СЕКРЕТНЫЙ ЧИТ-КОД: введите 'iamgod' для максимального уровня и 10000 золота ---");
        Console.Write("\nВыберите действие: ");
        
        string? choiceInput = Console.ReadLine();
        string choice = choiceInput ?? "";
        
        // Проверка на чит-код
        if (choice.ToLower() == "iamgod")
        {
            // Максимальный уровень (60)
            while (player.Level < 60)
            {
                player.LevelUp();
            }
            player.Gold = 10000;
            player.Health = player.MaxHealth; // Полное восстановление здоровья
            
            Console.Clear();
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║         ЧИТ-КОД АКТИВИРОВАН!          ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine($"║  УРОВЕНЬ: {player.Level}/60                      ║");
            Console.WriteLine($"║  ЗОЛОТО: {player.Gold}                         ║");
            Console.WriteLine($"║  ЗДОРОВЬЕ: {player.Health}/{player.MaxHealth}                   ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
            continue; // Пропускаем остальную обработку и возвращаемся в меню
        }
        
        switch (choice)
        {
            case "1":
                currentLocation.Explore(player);
                break;
                
            case "2":
                if (currentLocation.Type == LocationType.City)
                    currentLocation.VisitCity(player);
                else
                {
                    currentLocation = allLocations[0];
                    Console.WriteLine("Вы вернулись в Штормград!");
                    Console.ReadKey();
                }
                break;
                
            case "3":
                ShowLocationsMenu(player, ref currentLocation, allLocations);
                break;
                
            case "4":
                gameRunning = false;
                break;
        }
    }
}

        static void ShowLocationsMenu(Player player, ref Location currentLocation, List<Location> allLocations)
        {
            Console.Clear();
            Console.WriteLine("\nДОСТУПНЫЕ ЛОКАЦИИ:\n");
            
            for (int i = 0; i < allLocations.Count; i++)
            {
                Location loc = allLocations[i];
                string levelInfo = loc.MinLevel > player.Level ? " (СЛИШКОМ ОПАСНО!)" : "";
                Console.WriteLine($"{i + 1}. {loc.Name} - {loc.Type}{levelInfo}");
            }
            
            Console.WriteLine("0. Отмена");
            Console.Write("\nВыберите локацию: ");
            
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= allLocations.Count)
            {
                Location selected = allLocations[choice - 1];
                
                if (player.Level >= selected.MinLevel)
                {
                    currentLocation = selected;
                    Console.WriteLine($"\nВы отправились в {selected.Name}!");
                }
                else
                {
                    Console.WriteLine($"\nЭта локация слишком опасна! Нужен уровень {selected.MinLevel}");
                }
            }
            
            Console.ReadKey();
        }

        public static void StartBattle(Player player, Enemy enemy)
        {
    Console.Clear();
    Console.WriteLine($"\n=== БОЙ С {enemy.Name.ToUpper()} ===\n");
    Thread.Sleep(1500);
    
    while (player.Health > 0 && enemy.IsAlive())
    {
        Console.WriteLine($"Ваше HP: {player.Health}/{player.MaxHealth}");
        Console.WriteLine($"HP врага: {enemy.Health}");
        Console.WriteLine($"\n{player.GetExpBar(15)}"); // Прогресс-бар во время боя
        Console.WriteLine("\n1. Атаковать");
        Console.WriteLine("2. Попытаться убежать");
        Console.Write("Выберите действие: ");
        
        string choice = Console.ReadLine() ?? "";
        
        if (choice == "1")
        {
            Console.Clear();
            Console.WriteLine($"\n=== БОЙ С {enemy.Name.ToUpper()} ===\n");
            player.Attack(enemy);
            Console.WriteLine($"Вы нанесли {player.AttackPower} урона!");
            Thread.Sleep(1500);
            
            if (enemy.IsAlive())
            {
                enemy.Attack(player);
                Thread.Sleep(1500);
            }
            
            Console.Clear();
            Console.WriteLine($"\n=== БОЙ С {enemy.Name.ToUpper()} ===\n");
        }
        else if (choice == "2")
        {
            Random rand = new Random();
            if (rand.Next(2) == 0)
            {
                Console.WriteLine("Вы убежали!");
                Thread.Sleep(1500);
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Не удалось убежать!");
                Thread.Sleep(1500);
                enemy.Attack(player);
                Thread.Sleep(1500);
                Console.Clear();
                Console.WriteLine($"\n=== БОЙ С {enemy.Name.ToUpper()} ===\n");
            }
        }
    }
    
    if (!enemy.IsAlive())
    {
        Console.Clear();
        Console.WriteLine($"\n╔═══════════════════════════════════════╗");
        Console.WriteLine($"║              ПОБЕДА!                  ║");
        Console.WriteLine($"╚═══════════════════════════════════════╝");
        Console.WriteLine($"\nВы победили {enemy.Name}!");
        Thread.Sleep(1000);
        
        // Показываем полученные награды
        Console.WriteLine($"\n╔═══════════════════════════════════════╗");
        Console.WriteLine($"║              НАГРАДА                  ║");
        Console.WriteLine($"╠═══════════════════════════════════════╣");
        Console.WriteLine($"║  Золото: +{enemy.ExpReward,-5}                         ║");
        Console.WriteLine($"║  Опыт:   +{enemy.ExpReward,-5}                         ║");
        
        int oldLevel = player.Level;
        int oldExp = player.Exp;
        
        // Добавляем опыт и золото
        player.Gold += enemy.ExpReward;
        player.AddExp(enemy.ExpReward);
        
        // Показываем изменение опыта
        Console.WriteLine($"║                                           ║");
        Console.WriteLine($"║  Прогресс опыта:                          ║");
        Console.WriteLine($"║  {player.GetExpBar(18)} ║");
        
        // Если уровень повысился
        if (player.Level > oldLevel)
        {
            Console.WriteLine($"║                                           ║");
            Console.WriteLine($"║  ⚡ УРОВЕНЬ ПОВЫШЕН! УРОВЕНЬ {player.Level}! ⚡  ║");
            Console.WriteLine($"║  ❤️ Здоровье +20 | ⚔️ Атака +5            ║");
        }
        
        Console.WriteLine($"╚═══════════════════════════════════════╝");
        
        Thread.Sleep(3000);
    }
    
    Console.WriteLine("\nНажмите любую клавишу...");
    Console.ReadKey();
}
    }

    public enum LocationType
    {
        City,
        Forest,
        Desert,
        Mountain,
        Swamp,
        Dungeon,
        Volcanic,
        Plague,
        Raid
    }

    public class Items
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Effect { get; set; }
        public string Description { get; set; }
        
        public Items(string name, int price, int effect, string description = "")
        {
            Name = name;
            Price = price;
            Effect = effect;
            Description = description;
        }
    }

    public class Shop
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public List<Items> Inventory { get; set; }
        
        public Shop(string name, string owner)
        {
            Name = name;
            Owner = owner;
            Inventory = new List<Items>();
            FillInventory();
        }
        
        private void FillInventory()
        {
            Inventory.Add(new Items("Малое зелье здоровья", 25, 30, "Восстанавливает 30 HP"));
            Inventory.Add(new Items("Среднее зелье здоровья", 45, 60, "Восстанавливает 60 HP"));
            Inventory.Add(new Items("Большое зелье здоровья", 80, 100, "Восстанавливает 100 HP"));
            Inventory.Add(new Items("Ржавый меч", 50, 5, "+5 к атаке"));
            Inventory.Add(new Items("Стальной меч", 120, 10, "+10 к атаке"));
            Inventory.Add(new Items("Двуручный меч", 250, 20, "+20 к атаке"));
            Inventory.Add(new Items("Кожаная броня", 80, 20, "+20 к макс. здоровью"));
            Inventory.Add(new Items("Кольчуга", 200, 40, "+40 к макс. здоровью"));
            Inventory.Add(new Items("Латы", 400, 70, "+70 к макс. здоровью"));
        }
        
        public void Open(Player player)
        {
            Console.Clear();
            Console.WriteLine($"\n╔═══════════════════════════════════════╗");
            Console.WriteLine($"║            {Name,-20}           ║");
            Console.WriteLine($"╚═══════════════════════════════════════╝");
            Console.WriteLine($"\nПродавец: {Owner}");
            Console.WriteLine($"\"Добро пожаловать, путник!\"");
            Console.WriteLine($"\nТвое золото: {player.Gold}\n");
            
            bool shopping = true;
            while (shopping)
            {
                Console.WriteLine("╔═══════════════════════════════════════╗");
                Console.WriteLine("║              ТОВАРЫ                   ║");
                Console.WriteLine("╠═══════════════════════════════════════╣");
                
                for (int i = 0; i < Inventory.Count; i++)
                {
                    Items item = Inventory[i];
                    Console.WriteLine($"║ {i + 1,-2}. {item.Name,-20} {item.Price,5} зол. ║");
                    Console.WriteLine($"║    {item.Description,-31} ║");
                }
                
                Console.WriteLine("╠═══════════════════════════════════════╣");
                Console.WriteLine("║ 0. Выйти из магазина                  ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.Write("Выберите товар: ");
                
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        Console.WriteLine($"\n{Owner}: \"Заходи ещё, всегда рад видеть!\"");
                        shopping = false;
                    }
                    else if (choice >= 1 && choice <= Inventory.Count)
                    {
                        Items selected = Inventory[choice - 1];
                        
                        if (player.Gold >= selected.Price)
                        {
                            player.Gold -= selected.Price;
                            
                            if (selected.Name.Contains("зелье"))
                            {
                                player.Heal(selected.Effect);
                                Console.WriteLine($"\nВы купили {selected.Name} и сразу выпили!");
                                Console.WriteLine($"Восстановлено {selected.Effect} HP!");
                            }
                            else if (selected.Name.Contains("броня") || selected.Name.Contains("кольчуга") || selected.Name.Contains("Латы"))
                            {
                                player.MaxHealth += selected.Effect;
                                player.Health += selected.Effect;
                                Console.WriteLine($"\nВы купили {selected.Name}!");
                                Console.WriteLine($"Макс. здоровье увеличено на {selected.Effect}!");
                            }
                            else
                            {
                                player.AttackPower += selected.Effect;
                                Console.WriteLine($"\nВы купили {selected.Name}!");
                                Console.WriteLine($"Атака увеличена на {selected.Effect}!");
                            }
                            
                            Console.WriteLine($"\n{Owner}: \"Спасибо за покупку!\"");
                        }
                        else
                        {
                            Console.WriteLine($"\n{Owner}: \"Эээ, друг, у тебя не хватает золота...\"");
                        }
                        
                        Console.WriteLine("\nНажмите любую клавишу...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }
    }

    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int ExpReward { get; set; }
        public bool IsBoss { get; set; }
        public string AgroPhrase { get; set; }
        public string AttackPhrase { get; set; }
        public string DeathPhrase { get; set; }

        public Enemy(string name, int health, int attackPower, int expReward, 
                     string agroPhrase = "АГРРР!", 
                     string attackPhrase = "ПОЛУЧАЙ!", 
                     string deathPhrase = "*умирает*", 
                     bool isBoss = false)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            ExpReward = expReward;
            IsBoss = isBoss;
            AgroPhrase = agroPhrase;
            AttackPhrase = attackPhrase;
            DeathPhrase = deathPhrase;
        }
        
        public void Attack(Player player)
        {
            Console.WriteLine($"{Name}: \"{AttackPhrase}\"");
            player.TakeDamage(AttackPower);
        }
        
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }
        
        public bool IsAlive()
        {
            return Health > 0;
        }
    }

    public class Player
{
    // Свойства
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int AttackPower { get; set; }
    public int Gold { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public int ExpToNextLevel { get; set; }
    
    // Конструктор
    public Player(string name)
    {
        Name = name;
        MaxHealth = 100;
        Health = MaxHealth;
        AttackPower = 30;
        Gold = 100;
        Level = 1; 
        Exp = 0;    
        ExpToNextLevel = 100;
    }
    
    // Методы атаки и получения урона
    public void Attack(Enemy enemy) 
    {
        enemy.TakeDamage(AttackPower);
    }
    
    public void TakeDamage(int damage) 
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
    
    public void Die() 
    {
        Health = 0;
    }
    
    public void Heal(int amount) 
    {
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
    
    // ЕДИНСТВЕННЫЙ метод LevelUp (объединил оба варианта)
    public void LevelUp()
    {
        Level++;
        if (Level > 60) Level = 60; // Максимальный уровень 60
        
        // Увеличиваем характеристики при повышении уровня
        MaxHealth += 10;  // Было +10 и +5, оставим +10 для баланса
        AttackPower += 2; // Было +2 и +1, оставим +2
        
        Health = MaxHealth; // Полное восстановление при повышении уровня
        
        // Увеличиваем требуемый опыт для следующего уровня
        ExpToNextLevel = (int)(ExpToNextLevel * 1.5);
        
        Console.WriteLine($"УРОВЕНЬ ПОВЫШЕН! Теперь уровень {Level}!");
        Console.WriteLine($"Макс. здоровье: +10 | Атака: +2");
    }
    
    // Метод добавления опыта
    public void AddExp(int amount)
    {
        Exp += amount;
        Console.WriteLine($"+{amount} опыта!");
        
        // Проверяем, не повысился ли уровень несколько раз
        while (Exp >= ExpToNextLevel)
        {
            Exp -= ExpToNextLevel;
            LevelUp();
        }
    }
    
    // Прогресс-бар опыта
    public string GetExpBar(int width = 20)
    {
        float percentage = (float)Exp / ExpToNextLevel;
        int filledWidth = (int)(percentage * width);
        
        string bar = "[";
        bar += new string('█', filledWidth);
        bar += new string('░', width - filledWidth);
        bar += $"] {Exp}/{ExpToNextLevel} ({percentage * 100:F1}%)";
        
        return bar;
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
        
        public void Die() 
        {
            Health = 0;
        }
        
        public void Heal(int amount) 
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LocationType Type { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public Shop? Shop { get; set; }
        public bool IsSafe { get; set; }
        public bool IsDungeon { get; set; }
        public bool IsRaid { get; set; }
        public List<Enemy> PossibleEnemies { get; set; }
        
        public Location(string name, string description, LocationType type)
        {
            Name = name;
            Description = description;
            Type = type;
            MinLevel = 1;
            MaxLevel = 60;
            IsSafe = type == LocationType.City;
            IsDungeon = type == LocationType.Dungeon || type == LocationType.Raid;
            IsRaid = type == LocationType.Raid;
            PossibleEnemies = new List<Enemy>();
            
            FillEnemiesByLocation();
        }
        
        private void FillEnemiesByLocation()
        {
            switch (Name)
            {
                case "Элвиннский лес":
                    PossibleEnemies.Add(new Enemy("Гоблин-разбойник", 25, 4, 15, "Хе-хе, золотишко принёс?", "Ща как дам!", "Мама..."));
                    PossibleEnemies.Add(new Enemy("Лесной паук", 20, 5, 12, "*шипит*", "*кусает*", "*скрючил лапки*"));
                    PossibleEnemies.Add(new Enemy("Койот", 30, 6, 18, "*рычит*", "*клацает зубами*", "*скулит*"));
                    PossibleEnemies.Add(new Enemy("Бандит", 35, 7, 22, "Кошелёк или жизнь!", "Попался!", "Проклятье..."));
                    break;
                    
                case "Западные Края":
                    PossibleEnemies.Add(new Enemy("Дефолт-мародёр", 40, 8, 30, "Всё будет моим!", "За Братство!", "Ван Клифф..."));
                    PossibleEnemies.Add(new Enemy("Койот-падальщик", 35, 7, 25, "*воет*", "*рвёт когтями*", "*издыхает*"));
                    PossibleEnemies.Add(new Enemy("Гоблин-инженер", 30, 9, 28, "Сейчас бабахнет!", "Взрываем!", "*недо взрыв*"));
                    break;
                    
                case "Тёмный берег":
                    PossibleEnemies.Add(new Enemy("Нага-воительница", 50, 12, 45, "Ш-ш-ш-ш!", "В пучину!", "*шипит*"));
                    PossibleEnemies.Add(new Enemy("Сатир", 55, 14, 50, "Тьма зовёт!", "Я слуга Легиона!", "Кил'джеден..."));
                    PossibleEnemies.Add(new Enemy("Мурлок-охотник", 40, 10, 35, "Мрглглгл!", "Мргл!", "*бульк*"));
                    break;
                    
                case "Ясеневый лес":
                    PossibleEnemies.Add(new Enemy("Орк-разведчик", 60, 15, 55, "За Орду!", "Лок'тар огар!", "Кровь..."));
                    PossibleEnemies.Add(new Enemy("Ночной эльф-часовой", 65, 16, 60, "За Альянс!", "Лес не простит!", "Элуна..."));
                    PossibleEnemies.Add(new Enemy("Древень", 80, 12, 70, "*шелестит листвой*", "*бьёт веткой*", "*падает*"));
                    break;
                    
                case "Фералас":
                    PossibleEnemies.Add(new Enemy("Сатир-скверноподданный", 70, 18, 75, "Тьма поглотит тебя!", "Жертва!", "*рассыпается*"));
                    PossibleEnemies.Add(new Enemy("Древний защитник", 100, 15, 90, "Не буди лес!", "*трясёт корнями*", "*затихает*"));
                    PossibleEnemies.Add(new Enemy("Харпия", 55, 20, 60, "*пронзительный крик*", "*клюёт*", "*падает*"));
                    break;
                    
                case "Степи":
                    PossibleEnemies.Add(new Enemy("Кентавр-воин", 50, 14, 45, "Убей чужака!", "Быстрее ветра!", "Мой народ..."));
                    PossibleEnemies.Add(new Enemy("Кентавр-шаман", 55, 12, 50, "Духи равнин!", "Земля дрожит!", "*падает*"));
                    PossibleEnemies.Add(new Enemy("Песчаный червь", 45, 15, 40, "*рычит из-под земли*", "*выпрыгивает*", "*затихает*"));
                    break;
                    
                case "Танарис":
                    PossibleEnemies.Add(new Enemy("Песчаный тролль", 70, 16, 70, "Тролль бить!", "Валить всех!", "Тролль умер..."));
                    PossibleEnemies.Add(new Enemy("Скорпид", 60, 18, 60, "*щёлкает клешнями*", "*жалит*", "*переворачивается*"));
                    PossibleEnemies.Add(new Enemy("Гоблин-пират", 55, 15, 55, "Йо-хо-хо!", "На абордаж!", "*тонет*"));
                    break;
                    
                case "Тысяча Игл":
                    PossibleEnemies.Add(new Enemy("Гарпия", 50, 16, 50, "*кричит*", "*пикирует*", "*падает в пропасть*"));
                    PossibleEnemies.Add(new Enemy("Ветрокрыл", 65, 18, 65, "*орлиный крик*", "*рвёт когтями*", "*падает*"));
                    PossibleEnemies.Add(new Enemy("Орк-скалолаз", 60, 15, 55, "За Орду!", "Не отступать!", "*срывается*"));
                    break;
                    
                case "Пещеры Стенаний":
                    PossibleEnemies.Add(new Enemy("Тюремщик-дефолт", 45, 12, 40, "Никто не сбежит!", "Сидеть!", "*падает*"));
                    PossibleEnemies.Add(new Enemy("Каменный великан", 80, 18, 80, "*рёв камня*", "*давит*", "*рассыпается*"));
                    PossibleEnemies.Add(new Enemy("Эдвин Ван Клифф", 150, 25, 200, "Ты не понимаешь масштабов!", "Братство не остановить!", "Ван Клифф... уходит...", true));
                    break;
                    
                case "Монастырь Алого Ордена":
                    PossibleEnemies.Add(new Enemy("Монах Алого Ордена", 60, 14, 60, "Очистись огнём!", "Еретик!", "*падает*"));
                    PossibleEnemies.Add(new Enemy("Паладин Алого Ордена", 80, 18, 90, "Свет покарает!", "Изыди!", "*падает*"));
                    PossibleEnemies.Add(new Enemy("Верховный инквизитор", 120, 22, 150, "Ты ответишь за свои грехи!", "Покайся!", "*умирает*", true));
                    break;
                    
                case "Ульдаман":
                    PossibleEnemies.Add(new Enemy("Каменный великан", 90, 20, 100, "*гул камней*", "*дробит*", "*оседает*"));
                    PossibleEnemies.Add(new Enemy("Тролль-берсерк", 100, 22, 110, "БЕРСЕРК!", "КРОВА-А-А-АВЬ!", "*падает*"));
                    PossibleEnemies.Add(new Enemy("Древний механизм", 150, 18, 150, "*скрежет*", "*искрит*", "*взрывается*", true));
                    break;
                    
                case "Затонувший храм":
                    PossibleEnemies.Add(new Enemy("Змеелюд", 80, 16, 90, "Ш-ш-ш-ш-ш!", "Во тьму!", "*шипит*"));
                    PossibleEnemies.Add(new Enemy("Драконоид", 120, 25, 150, "Ты потревожил храм!", "Огонь!", "*падает*"));
                    PossibleEnemies.Add(new Enemy("Пророк-змей", 180, 30, 250, "Древние проснутся!", "Жертва!", "*тонет*", true));
                    break;
                    
                case "Пылающие степи":
                    PossibleEnemies.Add(new Enemy("Огненный элементаль", 100, 125, 120, "*пылает*", "ГОРЮ!", "*гаснет*"));
                    PossibleEnemies.Add(new Enemy("Чёрный дракон", 200, 135, 250, "*драконий рёв*", "ОГОНЬ!", "*падает*", true));
                    PossibleEnemies.Add(new Enemy("Орк-берсерк", 120, 128, 150, "КРОВА-А-А-АВЬ!", "ЗА ОРДУ!", "*падает с честью*"));
                    break;
                    
                case "Чумные земли":
                    PossibleEnemies.Add(new Enemy("Вурдалак", 90, 118, 100, "*хрипит*", "МЯСО!", "*разлагается*"));
                    PossibleEnemies.Add(new Enemy("Абоминация", 180, 125, 200, "*жуткий рёв*", "*давит*", "*разваливается*", true));
                    PossibleEnemies.Add(new Enemy("Некромант", 110, 120, 150, "Смерть - это начало!", "Поднимитесь!", "*рассыпается*"));
                    break;
                    
                case "Наксрамас":
                    PossibleEnemies.Add(new Enemy("Паук-Ануб'рек", 300, 140, 500, "*шипение*", "Холод...", "*падает*", true));
                    PossibleEnemies.Add(new Enemy("Готик Жнец", 350, 145, 600, "ЖИВИ ВЕЧНО!", "Служи!", "*падает*", true));
                    PossibleEnemies.Add(new Enemy("Кел'Тузад", 500, 160, 1000, "ТЫ ПОСМЕЛ ПОТРЕВОЖИТЬ МЕНЯ?!", "Проклятие!", "Артас... ты обещал...", true));
                    break;
                    
                case "Чёрная Гора":
                    PossibleEnemies.Add(new Enemy("Ренд Блэкхэнд", 600, 150, 800, "Чёрная Гора не падёт!", "За клан!", "*падает*", true));
                    PossibleEnemies.Add(new Enemy("Нефариан", 700, 170, 1500, "*драконий смех*", "ГОРИ!", "*падает*", true));
                    break;
            }
        }
        
        public void VisitCity(Player player)
        {
            if (Type != LocationType.City)
            {
                Console.WriteLine("Это не город!");
                return;
            }
            
            Console.Clear();
            Console.WriteLine($"\n╔═══════════════════════════════════════╗");
            Console.WriteLine($"║            {Name,-20}           ║");
            Console.WriteLine($"╚═══════════════════════════════════════╝");
            Console.WriteLine($"\n{Description}");
            
            bool inCity = true;
            while (inCity)
            {
                Console.WriteLine($"\n╔═══════════════════════════════════════╗");
                Console.WriteLine($"║  Здоровье: {player.Health}/{player.MaxHealth,-5} Золото: {player.Gold,-5}       ║");
                Console.WriteLine($"╠═══════════════════════════════════════╣");
                Console.WriteLine($"║ 1. Пойти в магазин                    ║");
                Console.WriteLine($"║ 2. Отдохнуть в таверне (30 золота)    ║");
                Console.WriteLine($"║ 3. Покинуть город                     ║");
                Console.WriteLine($"╚═══════════════════════════════════════╝");
                Console.Write("Выберите действие: ");
                
                string? cityChoice = Console.ReadLine();
                
                switch (cityChoice)
                {
                    case "1":
                        if (Shop != null)
                        {
                            Shop.Open(player);
                        }
                        else
                        {
                            Console.WriteLine("В этом городе нет магазина...");
                            Console.WriteLine("Нажмите любую клавишу...");
                            Console.ReadKey();
                        }
                        break;
                        
                    case "2":
                        if (player.Gold >= 30)
                        {
                            player.Gold -= 30;
                            int healAmount = 50;
                            player.Heal(healAmount);
                            Console.WriteLine($"\nВы отдохнули в таверне. Восстановлено {healAmount} HP!");
                            Console.WriteLine($"Текущее здоровье: {player.Health}/{player.MaxHealth}");
                        }
                        else
                        {
                            Console.WriteLine("\nНедостаточно золота! Нужно 30 золота.");
                        }
                        Console.WriteLine("Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                        
                    case "3":
                        inCity = false;
                        Console.WriteLine($"\nВы покидаете {Name}...");
                        break;
                        
                    default:
                        Console.WriteLine("Неверный выбор!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        public void Explore(Player player)
        {
            Console.Clear();
            Console.WriteLine($"\n╔═══════════════════════════════════════╗");
            Console.WriteLine($"║            {Name,-20}           ║");
            Console.WriteLine($"╚═══════════════════════════════════════╝");
            Console.WriteLine($"\n{Description}");
            Console.WriteLine($"\nТребуемый уровень: {MinLevel}");
            
            if (player.Level < MinLevel)
            {
                Console.WriteLine($"\nЭта локация слишком опасна для тебя! Нужен уровень {MinLevel}");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            
            Random rand = new Random();
            int encounterChance = GetEncounterChance();
            
            Console.WriteLine($"\nИсследуем {Name}...");
            
            if (rand.Next(100) < encounterChance)
            {
                Enemy? enemy = GetRandomEnemy();
                if (enemy != null)
                {
                    Console.WriteLine($"\nВНИМАНИЕ! Вы встретили {enemy.Name}!");
                    Console.WriteLine($"{enemy.AgroPhrase}");
                    Console.WriteLine("Нажмите любую клавишу для начала боя...");
                    Console.ReadKey();
                    
                    Program.StartBattle(player, enemy);
                }
            }
            else
            {
                Console.WriteLine("\nВы исследовали местность, но никого не встретили.");
                
                if (rand.Next(100) < 15)
                {
                    int goldFound = rand.Next(5, 21);
                    player.Gold += goldFound;
                    Console.WriteLine($"Вы нашли {goldFound} золота!");
                }
                
                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }
        
        private int GetEncounterChance()
        {
            switch (Type)
            {
                case LocationType.Forest: return 40;
                case LocationType.Desert: return 35;
                case LocationType.Mountain: return 45;
                case LocationType.Dungeon: return 70;
                case LocationType.Volcanic: return 60;
                case LocationType.Plague: return 65;
                case LocationType.Raid: return 100;
                default: return 30;
            }
        }
        
        public Enemy? GetRandomEnemy()
        {
            if (PossibleEnemies.Count == 0)
                return null;
                
            Random rand = new Random();
            int index = rand.Next(PossibleEnemies.Count);
            Enemy template = PossibleEnemies[index];
            
            int healthVar = rand.Next(-5, 6);
            int attackVar = rand.Next(-1, 2);
            
            return new Enemy(
                template.Name,
                Math.Max(1, template.Health + healthVar),
                Math.Max(1, template.AttackPower + attackVar),
                template.ExpReward + rand.Next(-5, 6),
                template.AgroPhrase,
                template.AttackPhrase,
                template.DeathPhrase,
                template.IsBoss
            );
        }
    }

    public static class Locations
    {
        public static List<Location> LocationsList = new List<Location>
        {
            new Location("Штормград", "Столица Альянса, город величественных башен", LocationType.City)
            {
                Description = "Величественные башни Штормграда возвышаются над восточными королевствами. Статуя предков напоминает о былых героях.",
                Shop = new Shop("Торговый ряд", "Грегори Смотритель"),
                IsSafe = true
            },
            new Location("Стальгорн", "Гномья столица в недрах горы", LocationType.City)
            {
                Description = "Гигантские залы выточены в камне, кузницы пылают вечным огнём, а механизмы тикают в такт сердцу горы.",
                Shop = new Shop("Гильдия инженеров", "Торг Сыномолот"),
                IsSafe = true
            },
            new Location("Оргриммар", "Столица Орды", LocationType.City)
            {
                Description = "Суровые крепости из дерева и металла. В воздухе пахнет мясом и битвой. Слышны боевые кличи орков.",
                Shop = new Shop("Рынок сил", "Зевая Когтегрыз"),
                IsSafe = true
            },
            new Location("Громовой Утёс", "Столица тауренов на вершинах мес", LocationType.City)
            {
                Description = "Тотемные столбы уходят в небо. Шаманы общаются с духами предков. Ветер доносит мудрые речи вождя.", 
                Shop = new Shop("Тотем шамана", "Ворон Койот"),
                IsSafe = true
            },
            new Location("Элвиннский лес", "Зеленые холмы к югу от Штормграда", LocationType.Forest)
            {
                Description = "Солнечные лучи пробиваются сквозь листву. Мирные фермеры работают в полях... но разбойники прячутся в тенях.",
                MinLevel = 1,
                MaxLevel = 5
            },
            new Location("Западные Края", "Бескрайние поля и старые шахты", LocationType.Forest)
            {
                Description = "Пшеничные поля колышутся на ветру. Брошенные шахты таят опасности. Дефолты грабят караваны.",
                MinLevel = 5,
                MaxLevel = 10
            },
            new Location("Тёмный берег", "Таинственный лес у океана", LocationType.Forest)
            {
                Description = "Древние ночные эльфы охраняют эти земли. Туман скрывает опасности, а в воде слышен шёпот нагий.",
                MinLevel = 10,
                MaxLevel = 15
            },
            new Location("Ясеневый лес", "Земля вечной осени", LocationType.Forest)
            {
                Description = "Золотые листья падают с деревьев. Орда и Альянс ведут бесконечную битву за ресурсы.",
                MinLevel = 15,
                MaxLevel = 20
            },
            new Location("Степи", "Бескрайние равнины центрального Калимдора", LocationType.Desert) 
            {
                Description = "Сухой ветер несёт пыль. Кентавры охотятся на караваны. Койоты воют на луну.",
                MinLevel = 20,
                MaxLevel = 25
            },
            new Location("Тысяча Игл", "Каньоны с остроконечными скалами", LocationType.Mountain)
            {
                Description = "Гоблины построили зиплайны между скал. Внизу - Великая пропасть. Харпии атакуют путников.",
                MinLevel = 25,
                MaxLevel = 30
            },
            new Location("Танарис", "Пустыня на юге Калимдора", LocationType.Desert)
            {
                Description = "Палящее солнце и песчаные бури. Гоблинский порт Прибамбасск. Пираты и песчаные тролли.",
                MinLevel = 30,
                MaxLevel = 35
            },
            new Location("Фералас", "Древний лес с эльфийскими руинами", LocationType.Forest)
            {
                Description = "Заброшенные города ночных эльфов. Сатиры оскверняют землю. Древни охраняют тайны.",
                MinLevel = 35,
                MaxLevel = 40
            },
            new Location("Пещеры Стенаний", "Тюрьма под Штормградом", LocationType.Dungeon)
            {
                Description = "Сырые камеры и сточные воды. Бандиты и тюремщики сошли с ума. Дефолты захватили подземелье.",
                MinLevel = 15,
                MaxLevel = 20,
                IsDungeon = true
            },
            new Location("Монастырь Алого Ордена", "Оплот фанатиков", LocationType.Dungeon)
            {
                Description = "Религиозные фанатики сжигают 'неверных'. Четыре крыла - кладбище, библиотека, часовня, оружейная.",
                MinLevel = 30,
                MaxLevel = 40,
                IsDungeon = true
            },
            new Location("Ульдаман", "Древние гномьи залы", LocationType.Dungeon)
            {
                Description = "Титаны создали эти залы. Каменные великаны и троллы. Древние механизмы всё ещё работают.",
                MinLevel = 40,
                MaxLevel = 45,
                IsDungeon = true
            },
            new Location("Затонувший храм", "Храм ночных эльфов под водой", LocationType.Dungeon)
            {
                Description = "Древнее зло пробудилось. Змеелюды и драконоиды. Шесть алтарей нужно очистить.",
                MinLevel = 45,
                MaxLevel = 50,
                IsDungeon = true
            },
            new Location("Пылающие степи", "Земля, уничтоженная драконами", LocationType.Volcanic)
            {
                Description = "Земля трескается от жара. Чёрные драконы и огненные элементали. Рунные камни повсюду.",
                MinLevel = 50,
                MaxLevel = 55
            },
            new Location("Чумные земли", "Земля, проклятая чумой", LocationType.Plague)
            {
                Description = "Мёртвая земля. Плети и некроманты. Стратхольм пал. Артас ждёт в чаще.",
                MinLevel = 55,
                MaxLevel = 60
            },
            new Location("Наксрамас", "Летающая цитадель Лича", LocationType.Raid)
            {
                Description = "Некрополь парит над чумными землями. Четыре крыла - паучий, рыцарский, чумной, магический. Кел'Тузад ждёт.",
                MinLevel = 60,
                MaxLevel = 60,
                IsDungeon = true,
                IsRaid = true
            },
            new Location("Чёрная Гора", "Вулканическая твердыня", LocationType.Raid)
            {
                Description = "Две части - Верхний и Нижний пик. Ренд Блэкхэнд и Нефариан. Орки и чёрные драконы.",
                MinLevel = 50,
                MaxLevel = 60,
                IsDungeon = true,
                IsRaid = true
            }
        };
    }
}