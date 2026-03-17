using System;
using pwned;
using vano123123;
using ptimof01;
using delta;
using bobeko;

namespace Franik747
{
    public class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; } 
        public int Damage { get; set; }
        public int ExpDrop { get; set; } 
        public int GoldDrop { get; set; }

        public Enemy(string name, int hp, int damage, int expDrop, int goldDrop)
        {
            Name = name;
            HP = hp;
            MaxHP = hp;
            Damage = damage;
            ExpDrop = expDrop;
            GoldDrop = goldDrop;
           
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0) HP = 0;
        }
 
        public bool IsAlive()
        {
            return HP > 0;
        }
    }
}