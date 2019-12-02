using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


    // stats
    public int Level;
    public int Speed;
    public int Attack;
    public int Defence;
    public int Power;
    public int HP;
    public int Currenthealth;
   
    public int Money;
    // level stats
    public int CurrentLevelUpPoints;
    public int PointsToLevel;
    public int LevelUpPointsMultiplayer;

    GameDataHolder Gamedata;

    public List<GameDataHolder.Player> players = new List<GameDataHolder.Player>();

   
	// Use this for initialization
	void Start () {
        Gamedata = FindObjectOfType<GameDataHolder>();
        StartNewPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void StartNewPlayer()
    {
        players.Add(Gamedata.Players[0]);
        Level = players[0].Level;
        Speed = players[0].Speed;
        Attack = players[0].Attack;
        Defence = players[0].Defence;
        Power = players[0].Power;
     //   HP = players[0].HP;
        Currenthealth = HP;
        Money = 0;

        CurrentLevelUpPoints = 0;
        PointsToLevel = 0;
        LevelUpPointsMultiplayer = 2;

    }



    public void AddPlayers()
    {

    }


}
