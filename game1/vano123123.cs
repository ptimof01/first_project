using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vano123123
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Attack { get; set; } 
        public int Level { get; set; }
        public string Type { get; set; }

        public Item(int id, string name, int price, int attack, int level, string type)
        {
            Id = id;
            Name = name;
            Price = price;
            Attack = attack;
            Level = level;
            Type = type;
        }
    }
    
    public class Armor : Item
    {
        public int Durabillty { get; set; } 
        public int Defense { get; set; } 
        public Armor(int id, string name, int price, int durabillty, int defense, string type) 
            : base(id, name, price, 0, 1, type) 
        {
            Defense = defense;
            Durabillty = durabillty;
        }
    }
}