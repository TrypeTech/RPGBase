using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempActor : MonoBehaviour {

    // add  Canvas to the prefab of the player
    // add BattleHud script to it



    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int speed;
    public enum ActorType
    {
        Player,
        Enemy
    }
    public ActorType actor;

    BattleHud hud;

    public void Start()
    {
        hud = gameObject.GetComponentInChildren<BattleHud>();
        
    }
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        
        if (currentHP <= 0)
            return true;
        else
            return false;

        hud.SetHP(currentHP);
    }

   public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}
