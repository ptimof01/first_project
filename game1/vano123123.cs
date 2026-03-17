using pwned;
using Franik747;
using ptimof01;
using delta;
using bobeko;
using System.Data.Common;


namespace vano123123
{
    public class Item
    {
        public int Id;
        public string Name { get; set; }
        public int Price { get; set; }
        public int Attack { get; set; } 
        public int Level { get; set; } 
        public float Weight{ get; set; }
        public string Type{ get; set; }

        public Item(int id, string name, int price, int attack, int level,float weight,string type)
        {
            Id = id;
            Name = name;
            Price = price;
            Attack = attack;
            Level = level;
            Weight = weight;
            Type = type;
        }

        
    }
   public class Armor : Item
    {
        public int Durabillty { get; set; } 
        public int Defense{ get; set; } 
         

        public Armor(int id, string name, int price,float weight,int durabillty,int defense,string type)
        {
            Defense = defense;  // бонус к защите
            Durabillty = durabillty;  // прочность

        }
    }
}


