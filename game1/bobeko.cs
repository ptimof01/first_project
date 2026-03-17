using pwned;
using vano123123;
using Franik747;
using ptimof01;
using delta;

namespace bobeko;
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
{
    public class Weapon : Items
    {
    public int Damage;
    public Weapon(string name, int price, int effect, string description = "",int damage)
        {
        Name=name;
        Price = price;
        Effect = effect;
        Description = description;
        Damage=damage;
        }
    }
   public class HealingPotion : Items
    {
    public int Health;
    public Potion(string name, int price, int effect, string description = "",int health)
        {
        Name=name;
        Price = price;
        Effect = effect;
        Description = description;
        Health=health;
        }
    }
    public class PoisonPotion : Items
    {
    public int Poisoning;
    public PoisonPotion(string name, int price, int effect, string description = "",int poisoning)
        {
        Name=name;
        Price = price;
        Effect = effect;
        Description = description;
        Poisoning=poisoning;
        }
    } 
}