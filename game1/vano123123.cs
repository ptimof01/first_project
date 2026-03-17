using pwned;
using Franik747;
using ptimof01;
using delta;
using bobeko;


namespace vano123123
{
    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Attack { get; set; } 
        public int Level { get; set; } 

        public Item(string name, int price, int attack, int level)
        {
            Name = name;
            Price = price;
            Attack = attack;
            Level = level;
        }
    }
}