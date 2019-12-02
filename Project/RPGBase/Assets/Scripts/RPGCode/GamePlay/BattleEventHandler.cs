using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class BattleEventHandler : MonoBehaviour {

    // setup List of battle players
    // each containing there own data

    [Header("Battle Peramiters")]
    public int MaxEnamiesInBattle = 3;

    // Mus Contain A HealthBar and Text for game data
    [Header("Battle Fase Character Actors")]
    public List<GameObject> PlayerActorsSlots = new List<GameObject>();
    public List<GameObject> EnemyActorsSlots = new List<GameObject>();
    public List<GameObject> PlayerToAttackSlots = new List<GameObject>();
    public List<GameObject> EnemyToAttackSlots = new List<GameObject>();

    public List<GameDataHolder.Enemy> EnemyFighters = new List<GameDataHolder.Enemy>();
    public List<GameDataHolder.Player> Playerfighters = new List<GameDataHolder.Player>();
    
    [Header("Battle Pannels")]
    public GameObject StartBattlePannel;
    public GameObject BattleChoicePannel;
    public GameObject PlayerAttackPannel;
    public GameObject PlayerSkillPannel;
    public GameObject BattleInfoPannel;
    public GameObject ItemChoicePannel;
    public GameObject StatsPannel;

    // slots
    [Header("Slots for Pannels")]
    public List<GameObject> BattleOptionsSlots = new List<GameObject>();
    public List<GameObject> AttackOptionsSLots = new List<GameObject>();
    public List<GameObject> SkillOptionsSlots = new List<GameObject>();
    public List<GameObject> ItemOptionsSlots = new List<GameObject>();


    private List<GameDataHolder.Item> PlayerItems = new List<GameDataHolder.Item>();
    public List<GameDataHolder.Attack> EnamyAttacks = new List<GameDataHolder.Attack>();
    public List<GameDataHolder.Attack> PlayerAttacks = new List<GameDataHolder.Attack>();
    public List<GameDataHolder.Attack> PlayerSkills = new List<GameDataHolder.Attack>();


    // Battle Event info Lists
    public List<BattleDamageInfo> BattleDamages = new List<BattleDamageInfo>();

    // data
    public List<int> Speeds = new List<int>();
    public bool inBattle;

    // stats
    [Header("Stats HP and Lv")]
    public Text playerStats;
    public Text enamyStats;

    public Text GameInfoText;
    public Text StatInfoText;

    // bars
    [Header("Health Bars")]
    public Slider enamyHealthBar;
    public Slider playerHealthBar;

    

    //Refrences 
    GameDataHolder GamData;
    PlayerController Controller;
    Inventory inventory;
    Player playerData;

    // Chosen Actors
    GameDataHolder.Player ChosenPlayer;
    GameDataHolder.Enemy ChosenEnemy;

    public enum BattleStats
    {
        BattleIntro,
        BattleChoice,
        BattleAttack,
        BattleSkillAttack,
        BattleItem,
        PlayerAttacking,
        EnamyAttacking,
        CalculateDamage,
        PlayerDies,
        EnamyDies,
        ExitBattle,
        BattleOver
    }
    public BattleStats battleState;

    int SelectedItem;
    int PlayerBattleChoiceTurn;
    int PlayerTurn;
    int CurrentAttackActive;

    // Use this for initialization
    void Start() {
        GamData = FindObjectOfType<GameDataHolder>();
        Controller = FindObjectOfType<PlayerController>();
        inventory = FindObjectOfType<Inventory>();
        playerData = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update() {

    }

    // Battle start ............................................................................................................Battle Start
    public void BeginBattleStart(List<GameDataHolder.Enemy> enemiesList, PlayerController player, int levelLimit)
    {
        PlayerTurn = 0;
        player.canMove = false;

        int Enemies = Random.Range(1, MaxEnamiesInBattle);

        // reset the enemy fighers 
        EnemyFighters.Clear();
        // may have to minus by 1



        // Remove all previous data from prior battle


        

        for (int i = 0; i < Enemies; i++)
        {
            // permaiters for the enemies that will be selected
            int enemy = Random.Range(0, enemiesList.Count);
            int enemyLv = Random.Range(levelLimit - 3, levelLimit);
            enemiesList[enemy].Level = enemyLv;
            EnemyFighters.Add(enemiesList[enemy]);
        }

        // for now there is just one player
        for (int i = 0; i < playerData.players.Count; i++)
        {
            Playerfighters.Add(playerData.players[i]);
        }
    }

 




    
    // Activate player Hud For player to make a choice
    // Show Player Health Hud For this Player
    // Get Access the the PlayerHolder in the battle scene
    public void DoPlayersTurn()
    {
        PlayerTurn += 1;
        // do halo around player to indicate player is chosen
       

        

    }



    // Activate Enemy Hud
    // Do eney attack and choice of player
    public void DoEnemiesTurn(GameDataHolder.Enemy enemy)
    {
        PlayerTurn += 1;

    }


    public void CheckIfThereIsANextPlayerChoice()
    {

    }
    //............................................................................................................Battle Choice
    // For each player Recycle the function to where each player has a turn to choose attack or item on them
    // ane recycle the slots for each one
    public void SetCurrentPlayerOptions()
    {
        // fill the Attack,Item,and skill lists data with the current player data
        // the when the player gose into battle select mode  and enable attack skill or item 
        // constructslots  functions will be filled with the current players data
        // then after the player choses its choice Do function check to see if there are any remaining players
        // that need a battle choice then commence battle event
        GameDataHolder.Player currentPlayer = Playerfighters[PlayerBattleChoiceTurn];

        for (int i = 0; i < GamData.Attacks.Count; i++)
        {
            // check all player attacks
            // set it to the player at begining soon
            if (currentPlayer.Etype == GamData.Attacks[i].Etype && GamData.Attacks[i].LevelRequirment <= currentPlayer.Level && GamData.Attacks[i].isItemAttack == false)
            {
                PlayerAttacks.Add(PlayerAttacks[i]);
            }
            // add skills list
            if (currentPlayer.Etype == GamData.Attacks[i].Etype && GamData.Attacks[i].isItemAttack == true)
            {
                for (int a = 0; a < inventory.PlayerInventory.Count; a++)
                {
                    if (inventory.PlayerInventory[a] == GamData.Attacks[i].ItemForAttack)
                    {
                        PlayerSkills.Add(GamData.Attacks[i]);
                    }
                }
            }

            PlayerBattleChoiceTurn += 1;
            if(PlayerBattleChoiceTurn > Playerfighters.Count)
            {
                // Begive battle Event Phase
                // Set PlayerBattleChoiceTurn to 0
                PlayerBattleChoiceTurn = 0;
            }
            // do for enamy Attacks
          //  if (attack.AttackList[i].type == Attacks.attacks.AttackType.normal && attack.AttackList[i].RecLevel <= TempEnamy.Level)
           // {
           //     EnamyAttacks.Add(attack.AttackList[i]);
          //  }
        }

        

        for (int i = 0; i < Playerfighters.Count; i++)
        {

            //  Setup battle face
            PlayerBattleChoiceTurn += 1;
            if(PlayerBattleChoiceTurn > Playerfighters.Count)
            {
                PlayerBattleChoiceTurn = 0;
                // start battle event fase
            }

            // Setup Slots

        }
        
    }

    
    //.................................................................................................... Attack Slot Construction
    public void ConstructAttackSlots()
    {
        for (int i = 0; i < PlayerAttacks.Count; i++)
        {
            AttackOptionsSLots[i].gameObject.GetComponentInChildren<Text>().text = PlayerAttacks[i].Name + " " + PlayerAttacks[i].Count + ":AM";
            int itemNumber = PlayerAttacks[i].ID;
            AttackOptionsSLots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
           // AttackOptionsSLots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoAttacks(itemNumber, true));
            SkillOptionsSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoAnAttack(itemNumber, 3f, false));
        }
    }


    //.................................................................................................... SKILL SLOT CONSTRUCTION
    // update items
    public void ConstructSkillSlots()
    {
        for (int i = 0; i < PlayerSkills.Count; i++)
        {

            SkillOptionsSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerSkills[i].Name + " " + PlayerSkills[i].Count.ToString() + ":AM";
            // add function to button when selected
            int itemNumber = PlayerSkills[i].ID;
            SkillOptionsSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            //SkillOptionsSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoSkillAttack(itemNumber));

            // set the function for seting the player attack
            SkillOptionsSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoAnAttack(itemNumber,3f,false));
        }
    }
    //set Item Select Slots 

    //.................................................................................................... ITEM SLOT CONSTRUCTION
    // done new every time
    public void ConstructItemSlots()
    {
        // update items
        PlayerItems.Clear();

        for (int i = 0; i < ItemOptionsSlots.Count; i++)
        {
            PlayerItems.Add(GamData.Items[0]);
        }

        int count = 0;

        for (int i = 0; i < inventory.PlayerInventory.Count; i++)
        {
            if (inventory.PlayerInventory[i].Type == GameDataHolder.Item.ItemType.Usable)
            {
                PlayerItems.Insert(count, inventory.PlayerInventory[i]);
                //  PlayerItems.Add(inventory.playerInventory[i]);
                count += 1;
                PlayerItems.RemoveAt(PlayerItems.Count - 1);
            }


        }
        // setup buttons

        for (int i = 0; i < PlayerItems.Count; i++)
        {

            ItemOptionsSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerItems[i].Name + " " + PlayerItems[i].Count.ToString() + ":AM";
            // add function to the button when selected
            int itemNumber = PlayerItems[i].ID;
            ItemOptionsSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
         //   if (PlayerItems[i].ID != 0)

                // initiate event fase change
             //   ItemOptionsSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoAttacks(itemNumber, false));
        }
    }


    //Battle Slot Function

    public void PlayerAttackPhase()
    {

    }

    public void ItemChoicePhase()
    {

    }
    public void PlayerSpecialAttackPhase()
    {

    }

    public void FleeBattle()
    {

    }


    // Battle Damage caluculations
    /*
 // pokemon battle calculation
 ((2A/5+2)*B* C)/D)/50)+2)*X)*Y/10)*Z)/255

A = attacker's Level
B = attacker's Attack or Special
C = attack Power
D = defender's Defense or Special
X = same-Type attack bonus(1 or 1.5)
Y = Type modifiers(40, 20, 10, 5, 2.5, or 0)
Z = a random number between 217 and 255

((2*LV/5+2)*ATKSTAT*ATKPOWER)/DEFENCESTAT)/50)+2)*SAMESTAT)*-SAMESTAT/10)*RANDOM)/255
((2*LV/5+2)*ATKSTAT*ATKPOWER)/DEFENCESTAT)/50)+2)*RANDOM)/255
*/


    public void SwitchBattleStates(BattleStats state)
    {

        battleState = state;
        switch (state)
        {
            case BattleStats.BattleIntro:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                //  BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
               // StartCoroutine(BattleIntro());


                break;
            case BattleStats.BattleChoice:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(true);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
                SelectedItem = 0;
                BattleOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();





                break;
            case BattleStats.BattleAttack:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(true);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
                ConstructAttackSlots();
                SelectedItem = 0;
                AttackOptionsSLots[SelectedItem].gameObject.GetComponent<Button>().Select();



                break;
            case BattleStats.BattleSkillAttack:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(true);
                ItemChoicePannel.gameObject.SetActive(false);
                ConstructSkillSlots();
                SelectedItem = 0;
                SkillOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();



                break;
            case BattleStats.BattleItem:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(true);

                ConstructItemSlots();
                SelectedItem = 0;
                ItemOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                break;
            case BattleStats.ExitBattle:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
                //  TempEnamy.Health = enamyHpStore;

              //  movement.canMove = true;
                inBattle = false;
             //   BattlePannel.gameObject.SetActive(false);


                break;
            case BattleStats.CalculateDamage:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);


                break;
            case BattleStats.PlayerDies:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
              //  StartCoroutine(Playerdies());


                break;
            case BattleStats.EnamyDies:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
               // StartCoroutine(EnamyDies());

                break;
            case BattleStats.BattleOver:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(true);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);

                break;
        }
    }
  




    public void DoBattleDamage()
    {


    }


    // Enable Battle choice For each player
    // Set Choice for each player based on speed

   

    public class BattleDamageInfo
    {
        public int OrderNumber { get; set; }
      
        public GameDataHolder.Attack AttackUsed { get; set; }
        public int PlayerTarget { get; set; }
        public int EnemyTarget { get; set; }
        public bool TargetIsEnemy { get; set; }

        public BattleDamageInfo(int order,GameDataHolder.Attack attackUsed,int playerTarget,int enemyTarget,bool targetIsEnemy)
        {
            this.OrderNumber = order;
            this.AttackUsed = attackUsed;
            this.PlayerTarget = playerTarget;
            this.EnemyTarget = enemyTarget;
            this.TargetIsEnemy = targetIsEnemy;

        }
    }



    // Chooses attack Function

    //..................................................................................................... Battle Data Info
    public void DoAnAttack(int AttackId, float time, bool PlayerIsTarget)
    {
        // Store What attack and To who
        // Create A Systematic way to determin attack pattern order

        BattleDamages.Add(new BattleDamageInfo(0, GamData.Attacks[AttackId],0, 0, PlayerIsTarget));
    }

    // Set player and Enamy Attack in list


    // choose player to attack button
    public void ChoosePersonToAttackSlots()
    {
        for(int i = 0; i < EnemyToAttackSlots.Count; i ++)
        {
            
            EnemyToAttackSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => SetPersonToAttack(i));
        }
    }

    // apply enemy to attack to current player attack
    public void SetPersonToAttack(int target)
    {
        // set player to attack in attack info
        BattleDamages[CurrentAttackActive].EnemyTarget = target;
        BattleDamages[CurrentAttackActive].TargetIsEnemy = true;
        // reset
        CurrentAttackActive += 1;
    }
    // Do attacks and events in order
    // Check if player has died

    // Go back to battle choice





    // Chooses the next players turn based on ............................................................................Choose Fighter turn
    public void ChooseFightersTurn()
    {
        // add enemy speeds
        for (int i = 0; i < EnemyFighters.Count; i++)
        {
            Speeds.Add(EnemyFighters[i].Speed);
        }

        // add player speeds
        for (int i = 0; i < EnemyFighters.Count; i++)
        {
            Speeds.Add(EnemyFighters[i].Speed);
        }

        // set list of speeds in order from least to fastest
        Speeds.Sort();
        // reverse list so fastest is first
        Speeds.Reverse();

        for (int i = 0; i < EnemyFighters.Count + Playerfighters.Count; i++)
        {
            //If is Enemy 
            if (EnemyFighters[i].Speed == Speeds[PlayerTurn])
            {
                // if is its speed do turn
                ChosenEnemy = EnemyFighters[i];
                
            }

            //If is Player
            else if (Playerfighters[i].Speed == Speeds[PlayerTurn])
            {
                // if is its speed do turn
                DoPlayersTurn();
                ChosenPlayer = Playerfighters[i];

            }
        }


      

    }

    public void ResetDataForBattleTurn()
    {
        PlayerTurn = 0;
        CurrentAttackActive = 0;
    }


    // do battle calculations

    IEnumerator DoBattleDamageInOrder()
    {
        // Display player i uses i attack on i
         
        yield return new WaitForSeconds(1f);
        // move attacker in direction of target and do attack

        yield return new WaitForSeconds(1f);
        // do battle damage
        //DoBattleCalculations()

       
        yield return new WaitForSeconds(1f);

        // check if there is another turn if not
        // check if everyone is dead
        // if not go back to battle choice
        PlayerTurn += 1;
        DoBattleDamage();
    }


    // calculates battle damage
    public void DoBattleCalculations(int Attacker, int target, int Attack, bool isPlayerAttacker)
    {

        if (isPlayerAttacker)
        {
            for (int i = 0; i < PlayerAttacks[Attacker].Count; i++)
            {
                if (PlayerAttacks[i].ID == Attack)
                {
                    // ............. Damage phase player stars here
                    // do effect here
                    // battle calculation
                    int crit = Random.Range(1, 8);
                    if (crit == 7)
                    {
                        crit = 2;
                    }
                    else
                    {
                        crit = 1;
                    }
                    int Damage = ((2 * Playerfighters[Attacker].Level / 5 + 2) * Playerfighters[Attacker].Attack * PlayerAttacks[Attacker].AttackPower) / EnemyFighters[target].Defence / 50 + 2 * Random.Range(217, 255) / 255;

                    //  int Damage = TempEnamy.Health + TempEnamy.Health * TempEnamy.Defence / 100 - player.AttackStat + player.AttackStat * PlayerAttacks[i].AtkPower / 100 * crit;
                    // check if enamy died
                    //tempenamy.current -= damage;

                    // alterd << i think auto kill do ubove change
                    EnemyFighters[target].CurrentHP -= Damage;


                    if (EnemyFighters[target].CurrentHP <= 0)
                    {
                        EnemyFighters[target].CurrentHP = 0;
                        // update the health bars
                        //  StartCoroutine(updateDamageBars());
                        // other stuf

                        SwitchBattleStates(BattleStats.EnamyDies);
                    }

                }
            }

        }
        else if (!isPlayerAttacker)
        {
            for (int i = 0; i < EnamyAttacks[Attacker].Count; i++)
            {
                if (PlayerAttacks[i].ID == Attack)
                {
                    // ............. Damage phase player stars here
                    // do effect here
                    // battle calculation
                    int crit = Random.Range(1, 8);
                    if (crit == 7)
                    {
                        crit = 2;
                    }
                    else
                    {
                        crit = 1;
                    }
                    int Damage = ((2 * EnemyFighters[Attacker].Level / 5 + 2) * EnemyFighters[Attacker].Attack * EnamyAttacks[i].AttackPower) / Playerfighters[target].Defence / 50 + 2 * Random.Range(217, 255) / 255;

                    //  int Damage = TempEnamy.Health + TempEnamy.Health * TempEnamy.Defence / 100 - player.AttackStat + player.AttackStat * PlayerAttacks[i].AtkPower / 100 * crit;
                    // check if enamy died
                    //tempenamy.current -= damage;

                    // alterd << i think auto kill do ubove change
                    Playerfighters[target].CurrentHealth -= Damage;


                    if (Playerfighters[target].CurrentHealth <= 0)
                    {
                        Playerfighters[target].CurrentHealth = 0;
                        // update the health bars
                        //  StartCoroutine(updateDamageBars());
                        // other stuf

                        SwitchBattleStates(BattleStats.EnamyDies);
                    }

                }
            }

        }
    }
}
