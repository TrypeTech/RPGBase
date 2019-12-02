using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventTrigger : MonoBehaviour {

    // develop way to setup field attributs and art for diffrent areas 
    // in battle

    public enum AreaType
    {
        area1,
        area2,
        area3,
        area4
    }
    public int LevelLimit = 5;

    public AreaType area;
    public GameDataHolder Gamedata;
    public BattleEventHandler battleEvent;

    // Use this for initialization
    void Start()
    {
        Gamedata = FindObjectOfType<GameDataHolder>();
        battleEvent = FindObjectOfType<BattleEventHandler>();

        area = AreaType.area1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PlayerEnteredBattleTrigger...");
            // gets the player that when in and disables movement
               StartBattle(other.gameObject.GetComponent<PlayerController>());
        }
    }
   

    public void StartBattle(PlayerController move)
    {
        Debug.Log("Hit the trigger");

        List<GameDataHolder.Enemy> EnamyList = new List<GameDataHolder.Enemy>();

        // set up for different areas
        if (area == AreaType.area1)
        {
            for (int i = 0; i < Gamedata.Enemies.Count; i++)
            {
                if (Gamedata.Enemies[i].Etype == GameDataHolder.ElementType.Normal)
                {
                    EnamyList.Add(Gamedata.Enemies[i]);
                }
            }

            // StartBattle here
           battleEvent.BeginBattleStart(EnamyList, move, LevelLimit);
        }
    }
}
