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
        
        
                if (choice.ToLower() == "iamgod")
                {
            
                    while (player.Level < 60)
                    {
                        player.LevelUp();
                    }
                    player.Gold = 10000;
                    player.Health = player.MaxHealth; 
            
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
                    continue; 
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
        
        
                Console.WriteLine($"\n╔═══════════════════════════════════════╗");
                Console.WriteLine($"║              НАГРАДА                  ║");
                Console.WriteLine($"╠═══════════════════════════════════════╣");
                Console.WriteLine($"║  Золото: +{enemy.ExpReward,-5}                         ║");
                Console.WriteLine($"║  Опыт:   +{enemy.ExpReward,-5}                         ║");
        
                int oldLevel = player.Level;
                int oldExp = player.Exp;
        
        
                player.Gold += enemy.ExpReward;
                player.AddExp(enemy.ExpReward);
        
        
                Console.WriteLine($"║                                           ║");
                Console.WriteLine($"║  Прогресс опыта:                          ║");
                Console.WriteLine($"║  {player.GetExpBar(18)} ║");
        
        
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

    
}    