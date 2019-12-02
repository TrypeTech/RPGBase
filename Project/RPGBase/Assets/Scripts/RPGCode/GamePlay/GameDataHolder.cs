using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameDataHolder : MonoBehaviour {


    // Add this script to the GameManager
    // This Script contain all the base stats of items and attack, players, and enamies
    // Set the game contents of items and attack and stats in this script

    public List<Player> Players = new List<Player>();
    public List<Enemy> Enemies = new List<Enemy>();
    public List<Item> Items = new List<Item>();
    public List<Attack> Attacks = new List<Attack>();

    public enum ElementType
    {
        Normal,
        Water,
        Fire,
        Earth,
        Air,
        Electric,
        Sound
    }

    public ElementType EType;
	// Use this for initialization
	void Awake () {
        ConstructDataLists();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //PlayerClass................................................................................................................
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float HP { get; set; }
        public float CurrentHealth { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Level { get; set; }
        public int Power { get; set; }

        // ablity types
        public enum AblityTypes
        {
            Attacker,
            Defender,
            Speedster,
            PowerUp,
            Healer
        }
        public AblityTypes Type { get; set; }

        public ElementType Etype { get; set; }

        public Player (int id,string name,string desc,float hp,int speed,int attack,int defence,int level,int power,AblityTypes type,ElementType etype,float currentHealth)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.HP = hp;
            this.Speed = speed;
            this.Attack = attack;
            this.Defence = defence;
            this.Level = level;
            this.Power = power;
            this.Type = type;
            this.Etype = etype;
        }


    }

    //Monster Class...................................................................................................
    public class Enemy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float HP { get; set; }
        public float CurrentHP { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Level { get; set; }
        public int Power { get; set; }

        // ablity types
        public enum AblityTypes
        {
            Attacker,
            Defender,
            Speedster,
            PowerUp,
            Healer
        }
        public AblityTypes Type { get; set; }
        public int ItemDroped;

        public ElementType Etype;

        public Enemy (int id, string name, string desc, float hp, int speed, int attack, int defence, int level, int power, AblityTypes type,int itemDroped,ElementType etype,float currentHp)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.HP = hp;
            this.Speed = speed;
            this.Attack = attack;
            this.Defence = defence;
            this.Level = level;
            this.Power = power;
            this.Type = type;
            this.ItemDroped = itemDroped;
            this.Etype = etype;
            this.CurrentHP = currentHp;
        }
    }


    // Item Class....................................................................................................
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HealthPlus { get; set; }
        public int HPPlus { get; set; }
        public int AttackPlus { get; set; }
        public int DefencePlus { get; set; }
        public int SpeedPlus { get; set; }
        public int LevelPlus { get; set; }
        public int PowerPlus { get; set; }
        
        public enum ItemType
        {
            Usable,
            Equiptable,
            Quest,
            Craftable,
            Empty

        }

        public enum EquiptType
        {
            MainAttacker,
            SubAttacker,
            MinAttacker,
            MainShield,
            SubShield,
            MinShield,
            MainEmblem,
            SubEmblem,
            MinEmblem,
            none

        }
        public EquiptType Etype;
        public ItemType Type { get; set; }
        public int Cost { get; set; }
        public int EquiptItem { get; set; }
        public bool Equipted { get; set; }
        public int Count { get; set; }
        public Sprite Icon { get; set; }
        public string IconLoc { get; set; }

        public Item(int id, string name, string desc,int health,int hp,int attack,int defence,int speed,int level,int power,ItemType type,int cost,int item,bool equipted,int count,string iconLocation,EquiptType etype)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.HealthPlus = health;
            this.HPPlus = hp;
            this.AttackPlus = attack;
            this.DefencePlus = defence;
            this.SpeedPlus = speed;
            this.LevelPlus = level;
            this.PowerPlus = power;
            this.Type = type;
            this.Cost = cost;
            this.EquiptItem = item;
            this.Equipted = equipted;
            this.Count = count;
            this.Icon =  Resources.Load(iconLocation) as Sprite;
            this.Etype = etype;
        }

        
    }

    // Attack Class .............................................................................................................
    public class Attack
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AttackPower { get; set; }
        public int DefencePower { get; set; }
        public int StatusLevel { get; set; }
        public int Count { get; set; }



        public enum EffectType
        {
            plusAttack,
            plusDefence,
            plusSpeed,
            plusAccuracy,
            plusEvasion,
            plusHp,
            DoubleAttack,
            DoubleDefence,
            DoubleSpAttack,
            DoubleSpDefence,
            DoubleSpeed,
            BuffFire,
            BuffPoison,
            BuffParalysis,
            BuffSleep,
            none
        }
        public EffectType effectType { get; set; }

        public bool isItemAttack { get; set; }
        public Item ItemForAttack { get; set; }
        public ElementType Etype;
        public int LevelRequirment { get; set; }
        public Attack(int id, string name, string desc, int attackPower,int defencePower,int statusLevel,EffectType effect,ElementType etype,int levelRec,int count ,bool IsAnItemAttack,Item item)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.AttackPower = attackPower;
            this.Description = desc;
            this.StatusLevel = statusLevel;
            this.effectType = effect;
            this.Etype = etype;
            this.LevelRequirment = levelRec;
            this.Count = count;
            this.isItemAttack = IsAnItemAttack;
            this.ItemForAttack = item;
            
        }

    }
    public void ConstructDataLists()
    {
        // Item Types
        Items.Add(new Item(0, "", "", 0, 0, 0, 0, 0, 0, 0, Item.ItemType.Empty, 0, 0, false, 0, "", Item.EquiptType.none));
        Items.Add(new Item(1, "Healing", "Heals 10 health", 10, 0, 0, 0, 0, 0, 0, Item.ItemType.Usable, 10, 0, false, 1, "", Item.EquiptType.none));
        Items.Add(new Item(2, "Sword", "Strong Sword of health",10, 10, 0, 0, 0, 0, 0, Item.ItemType.Equiptable, 10, 0, false, 1, "", Item.EquiptType.MainAttacker));
        Items.Add(new Item(3, "Shield", "Strong Shild of Striength", 10, 0, 10, 0, 0, 0, 0, Item.ItemType.Equiptable, 10, 0, false, 1, "", Item.EquiptType.MainShield));
        Items.Add(new Item(4, "Healing", "Passage Key", 10, 0, 0, 0, 0, 0, 0, Item.ItemType.Quest, 10, 0, false, 1, "", Item.EquiptType.none));

        // Player Types
        Players.Add(new Player(0, "Hero", "Kind Hero", 50, 50, 10, 10, 5, 10, Player.AblityTypes.Attacker,ElementType.Normal,50));

        // Enemy Types
        Enemies.Add(new Enemy(0, "Bober", "SmallMonster", 10, 10, 10, 10, 2, 1, Enemy.AblityTypes.Defender,0,ElementType.Air,30));

        // Attack Types
        Attacks.Add(new Attack(0, "Bombard", "hits really hard", 20, 20, 0, Attack.EffectType.none, ElementType.Normal,1,20,false,Items[1]));
    }
}
