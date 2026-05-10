using Franik747;
using System;
using System.Runtime.InteropServices;
/*using pwned; брр брр патапим
using vano123123;
using ptimof01;
using delta;
using bobeko;
*/
namespace Franik747
{
   
    public class Location
    {
        public string name;
        public string discript;//описание (побольше воды)
        public List<string> PossibleActions;//лист действий в локации, типо при вводе или выборе объекта листа тебе выдают стишок,либо отправляют бить других
        public List<Enemy> enenmys; //он содержит противников в локации
        public Location(string _name,string _discript)
        {
            name = _name;
            discript = _discript;
        }
    }
    
    public class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Damage { get; set; }
        public int ExpDrop { get; set; }
        public int GoldDrop { get; set; }
        public string[] Replic { get; set; }

        public Enemy(string _name, int _hp, int _MaxHP, int _damage, int _expDrop, int _goldDrop)
        {
            Name = _name;
            HP = _hp;
            MaxHP = _MaxHP;
            Damage = _damage;
            ExpDrop = _expDrop;
            GoldDrop = _goldDrop;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0||HP == 0) 
            {
                HP = 0;
            }
        }
        public bool IsAlive()
        {               
            return HP > 0; 
        }
        public string GetRandomReplic()
        {
            // если массив реплик пустой или нулевой, возвращаем дефолтное сообщение
            if (Replic == null || Replic.Length == 0)
                return "...";

            // берем случайное число между 0 и длиной массива реплик
            Random rnd = new Random();
            return Replic[rnd.Next(Replic.Length)];
        }
    }
            class Program
        {
            static void Main()
            {
                Enemy goblin = new Enemy("гоблин", 50, 50, 10, 25, 10);
                Enemy wolf = new Enemy("волк", 65, 65, 17, 39, 15);
                Enemy knight = new Enemy("рыцарь", 100, 100, 15, 45, 20);
                Enemy Priests_of_the_Dark_Gods = new Enemy("жрецы темных богов,защищающие короля", 120, 120, 20, 50, 25);
                Enemy event_enemy = new Enemy("редкое существо при уничтожении которого - получаешь больше обычного", 1, 1, 1, 100, 50);
                Enemy franik = new Enemy("король королевства разбитого разума", 150, 150, 30, 75, 50);

            Location kingdom_franik = new Location("королевство франика", "вы пришли в рандомное место и что-то чувствуете");
            }
        }
    }
