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
        public string[] Replic { get; set; } 

        public Enemy(string name, int hp, int damage, int expDrop, int goldDrop, string[] replic = null)
        {
            Name = name;
            HP = hp;
            MaxHP = hp;
            Damage = damage;
            ExpDrop = expDrop;
            GoldDrop = goldDrop;
            Replic = replic ?? new string[] { "..." }; 
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

        public string GetRandomReplic()
        {
            if (Replic == null || Replic.Length == 0)
                return "...";
            Random rnd = new Random();
            return Replic[rnd.Next(Replic.Length)];
        }
    }
}