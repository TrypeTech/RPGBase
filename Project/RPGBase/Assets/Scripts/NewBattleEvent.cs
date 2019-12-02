using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum BattleState { START, PLAYERTURN,ATTACK,SPECIALATTACK,ITEM, ENEMYTURN, WON, LOST }
public class NewBattleEvent : MonoBehaviour {

    //Start 
    // player turn
    // enemyturn
    // won
    // lost

    // Instatiate Actor Prefabs and set the stats
    // set all the menus and buttons
    // Enable players to chose first
    // Do battle turns Fastest actor gose first
    // Do battle check to see if someone died or there is no more battle turns
    // reset battle phase
    // if all on one party side is dead end battle and enable battle stats hud

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public List<Transform> playerActorTransforms = new List<Transform>();
    public List<Transform> enemyActorTransforms = new List<Transform>();

    public List<GameObject> ActorPrefabs = new List<GameObject>();
    
    TempActor playerUnit;
    TempActor enemyUnit;

    public Text dialogueText;

    // pannels
    [Header("Pannels")]
      public GameObject BattleOptionPannel;
    public GameObject ActorToAttackPannel;
      public GameObject AttackOptionPannel;
    //  public GameObject SpecialOptionPannel;
    //  public GameObject ItemOptionsPannel;
    // public GameObject StatsPannel;

    [Header("EnemyChoiceButtons")]
    public List<GameObject> EnemyChoiceButtons = new List<GameObject>();


    private List<ActorAttack> AttackOrderList = new List<ActorAttack>();
    private List<GameObject> PlayerActors = new List<GameObject>();
    private List<GameObject> EnemyActors = new List<GameObject>();
    public BattleState state;

    public int PlayerTurns;
    public int CurrentPlayerTurn;

    public int EnemyTurns;
    public int currentEnemyTurn;

	// Use this for initialization
	void Start () {
      state = BattleState.START;
      StartCoroutine(SetupBattle(2,3));
	}
	
	



    // On begin the battle setting up 
    public IEnumerator SetupBattle(int players, int enemies)
    {
        DisableAllPannels();
        int ActorCount = 0;
        // add the enemies in battle
        for(int i = 0; i < enemies; i++)
        {
            // setup data for the enemy
            ActorPrefabs.Add(Instantiate(enemyPrefab, enemyActorTransforms[i]));
            ActorPrefabs[i].gameObject.GetComponentInChildren<BattleHud>().SetHUD();
            ActorPrefabs[i].gameObject.GetComponentInChildren<TempActor>().actor = TempActor.ActorType.Enemy;
            dialogueText.text = " A wild  " + ActorPrefabs[i] + " Aproches";
            // spawn monster effect // sound effect
            yield return new WaitForSeconds(0.5f);
        }
        // add the player in battle
        for (int i = 0 ; i < players; i++)
        {
            // setup data for the player
            ActorPrefabs.Add(Instantiate(playerPrefab, playerActorTransforms[i]));
            ActorPrefabs[enemies + i].gameObject.GetComponentInChildren<BattleHud>().SetHUD();
            yield return new WaitForSeconds(0.5f);
        }


      //   GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
      //   playerUnit = playerGO.GetComponent<TempActor>();
      //   playerUnit.gameObject.GetComponentInChildren<BattleHud>().SetHUD();
      //   GameObject enemyGO =   Instantiate(enemyPrefab, enemyBattleStation);
      //   enemyUnit = enemyGO.GetComponent<TempActor>();
      //   enemyUnit.gameObject.GetComponentInChildren<BattleHud>().SetHUD();
      //   playerHUD.SetHUD();
      //   enemyHUD.SetHUD();

        /// setup begin battle start time
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        BeginBattleChoice();
    }




    // ........................................................................................................On Battle Begin

    // setup how many players there are and how many player turns
    public void BeginBattleChoice()
    {
        PlayerActors.Clear();
        EnemyActors.Clear();
        PlayerTurns = 0;
        EnemyTurns = 0;
        currentEnemyTurn = 0;
        CurrentPlayerTurn = 0;
       
        // add how many player that are there and are still alive
        for (int i = 0; i < ActorPrefabs.Count; i++)
        {
            if (ActorPrefabs[i].gameObject.GetComponent<TempActor>().actor == TempActor.ActorType.Player)
            {
                PlayerActors.Add(ActorPrefabs[i]);
            }
        }
        // setup how many player turns there are
        for (int i = 0; i < PlayerActors.Count; i++)
        {
            PlayerTurns++;
        }

        // add how many enemies there are
        // add how many player that are there and are still alive
        for (int i = 0; i < ActorPrefabs.Count; i++)
        {
             if (ActorPrefabs[i].gameObject.GetComponent<TempActor>().actor == TempActor.ActorType.Enemy)
            {
                EnemyActors.Add(ActorPrefabs[i]);
            }
        }
        // setup how many player turns there are
        for (int i = 0; i < EnemyActors.Count; i++)
        {
            EnemyTurns++;
        }

        // start stuff
        PlayerTurn();
    }

    // ........................................................................................................On Battle Choices

    //......................................................................................................... Players Turn

        // Check if there are any more players turns if not go to enemy turn
        // Set the current players turn enable the battle pannels in the actors hud Enable player choice function
    void PlayerTurn()
    {


        Debug.Log("In Player Turn");
            ActorToAttackPannel.gameObject.SetActive(false);
            BattleOptionPannel.gameObject.SetActive(true);
          //  SetPlayerChoices(PlayerActors[CurrentPlayerTurn].gameObject);
        CurrentPlayerTurn++;
        dialogueText.text = "Choose an Action:";
        
    }


    // fill the buttons and pannels with the current players information
    public void SetPlayerChoices(GameObject Actor)
    {
       // int playerAttackDamage = PlayerActors[CurrentPlayerTurn].gameObject.GetComponent<TempActor>().damage;
        SetEnemyAttackChoises(Actor, 20);
    }


    // Set On player Attack button select
    public void SetEnemyAttackChoises(GameObject player, int Damage)
    {

        Debug.Log("in Enemy Attack Choices");
        ActorToAttackPannel.gameObject.SetActive(true);
        BattleOptionPannel.gameObject.SetActive(false);

        for (int i = 0; i < EnemyActors.Count; i++)
        {
            if (i > EnemyChoiceButtons.Count)
            {
                EnemyChoiceButtons[i].gameObject.SetActive(false);
            }
            else
            {
                EnemyChoiceButtons[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                //  EnemyChoiceButtons[i].GetComponentInChildren<Button>().onClick.AddListener(() => StartCoroutine(AttackEnemy(ActorPrefabs[i], Damage)));
                GameObject enemyActor = EnemyActors[i].gameObject;
                int damage = Damage;
                EnemyChoiceButtons[i].GetComponentInChildren<Button>().onClick.AddListener(() => SetActorsAttackOrder(player, enemyActor, damage));
                Debug.Log("attach choice in");
            }
        }

        Debug.Log("Attack Choice out");
    }


    // when player press button to attack
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        // chose enemy to attack
        // SetPlayerChoices(PlayerActors[CurrentPlayerTurn]);
       // SetPlayerChoices(PlayerActors[CurrentPlayerTurn - 1].gameObject);

        SetPlayerChoices(PlayerActors[CurrentPlayerTurn - 1].gameObject);
        
    }


    // Do Attack to enemy Temp
    public IEnumerator AttackEnemy(GameObject EnemyToAttack, int Damage)
    {
        EnemyToAttack.gameObject.GetComponent<TempActor>().TakeDamage(Damage);
        // Damage the enemy
        bool isDead = EnemyToAttack.gameObject.GetComponent<TempActor>().TakeDamage(Damage); ;

        int enemyHp = EnemyToAttack.gameObject.GetComponent<TempActor>().currentHP;
        // update text
        EnemyToAttack.gameObject.GetComponentInChildren<BattleHud>().SetHP(enemyHp);
        dialogueText.text = "The attack is successful";
        yield return new WaitForSeconds(.2f);

        // Check if the enemy is dead
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
            ActorToAttackPannel.gameObject.SetActive(false);
            BattleOptionPannel.gameObject.SetActive(false);
        }
        else
        {
            // enemy turn
            state = BattleState.ENEMYTURN;
           // StartCoroutine(EnemyTurn());
            ActorToAttackPannel.gameObject.SetActive(false);
            BattleOptionPannel.gameObject.SetActive(false);
        }

    }




    // other player attack function Non use
    public IEnumerator PlayerAttack()
    {
        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        // update text
        enemyUnit.gameObject.GetComponentInChildren<BattleHud>().SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful";
        yield return new WaitForSeconds(2f);
        // Check if the enemy is dead
        if (isDead)
        {
            // end the battle
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            // enemy turn
            state = BattleState.ENEMYTURN;
            //StartCoroutine(EnemyTurn());
        }
        // Change state based on what happend
    }
    //........................................................................................................ Enemies Turn

        // set random enemy attack
        // set random player to attack
    IEnumerator EnemyTurn()
    {

        ActorToAttackPannel.gameObject.SetActive(false);
        dialogueText.text = EnemyActors[currentEnemyTurn].name + " attacks!";
        yield return new WaitForSeconds(1f);
        int playerToAttack = Random.Range(0, PlayerActors.Count);

        // add next enemy turn and set the batle calculations
        currentEnemyTurn += 1;
       // Debug.Log("Enemy Turn Count" + currentEnemyTurn + " " + "Enemies " + EnemyTurns);
     //   Debug.Log("PlayerTurns Turn Count" + CurrentPlayerTurn + " " + "Players " + PlayerTurns);
        int Damage = EnemyActors[currentEnemyTurn].gameObject.GetComponent<TempActor>().damage;
        SetActorsAttackOrder(EnemyActors[currentEnemyTurn], PlayerActors[playerToAttack], Damage);

       
    }

   


    // ........................................................................................................Battle turn calculations

    public void SetActorsAttackOrder(GameObject attacking, GameObject target, int Damage)
    {
        //  ActorToAttackPannel.gameObject.SetActive(false);

        Debug.Log("In set actor attack order");

        if (currentEnemyTurn == EnemyTurns -1 )
        {
            // Do battle Calculations
           StartCoroutine(DoBattleDamageCalculations());
            ActorToAttackPannel.gameObject.SetActive(false);
            Debug.Log("turnsOver");
        }

        if (CurrentPlayerTurn  >= PlayerTurns  )
        {
            int ActorSpeed = attacking.gameObject.GetComponent<TempActor>().speed;
            AttackOrderList.Add(new ActorAttack(attacking, target, Damage, ActorSpeed));

            StartCoroutine( EnemyTurn());
            ActorToAttackPannel.gameObject.SetActive(false);
            Debug.Log("Enemy turn");
        }
        
        else
        {
            int ActorSpeed = attacking.gameObject.GetComponent<TempActor>().speed;
            AttackOrderList.Add(new ActorAttack(attacking, target, Damage, ActorSpeed));
            PlayerTurn();
            Debug.Log("A Players Turn");
        }
       
    }


     IEnumerator DoBattleDamageCalculations()
    {
        Debug.Log("Doing Battle Calculations");
        dialogueText.text = "Doing battle Calculations";
        yield return new WaitForSeconds(2f);
       

        // this is how you order by a pacific varable
        // var ActorAtks = AttackOrderList.OrderBy(go => go.Speed);
        //  WPGroupList = WPGroupList.OrderBy(go => go.name).ToArray();
       // var orderedList = daValues.OrderBy(x => x[0]).ThenBy(y => y[1]).ThenBy(z => z[2]).ThenBy(w => w[3]).ToList(); // new method 
       // do by level next
         AttackOrderList = AttackOrderList.OrderBy(go => go.Speed).ToList();

        for(int i = 0; i < AttackOrderList.Count; i++)
        {

           

            GameObject Attacker = AttackOrderList[i].AttackingActor;
            GameObject Target = AttackOrderList[i].AttackerTarget;
            Debug.Log(Attacker.name);
            // if player has already died dont do his attack
            if (ActorPrefabs.Contains(Attacker) && ActorPrefabs.Contains(Target))
            {
                Debug.Log("Contains Attacker");
               // GameObject Target = AttackOrderList[i].AttackerTarget;
                int damage = AttackOrderList[i].Damage;
              //  Debug.Log("DAmage " + damage);
               // dialogueText.text = Attacker.name + " Is Attacking " + Target.name;

                yield return new WaitForSeconds(1f);
                // animation attack effect
                // sound effects
               StartCoroutine( DoDamage(Target, damage));

                // animation hit drawback effect
                // sound effects
            }

        }

        // after all things have finished  and no one has died 
        // do battle choices
        BeginBattleChoice();
    }

    // battle damage
    public IEnumerator DoDamage(GameObject Target, int Damage)
    {
        // set the damage of the enemies attack here
        //  bool isDead = PlayerActors[playerToAttack].gameObject.GetComponent<TempActor>().TakeDamage(EnemyActors[currentEnemyTurn].gameObject.GetComponent<TempActor>().damage);
        // playerUnit.gameObject.GetComponentInChildren<BattleHud>().SetHP(playerUnit.currentHP);
        // playerHUD.SetHP(playerUnit.currentHP);

        Debug.Log(Target.name + " Target Name: " + Target.GetComponent<TempActor>().currentHP);
        bool isDead = Target.GetComponent<TempActor>().TakeDamage(Damage);
        // undo this do batle calculations
        yield return new WaitForSeconds(0f);
        if (isDead)
        {

            
            //state = BattleState.LOST;
            ActorPrefabs.Remove(Target);

            if (PlayerActors.Contains(Target))
            {
                PlayerActors.Remove(Target);
            }
            else if (EnemyActors.Contains(Target))
            {
                EnemyActors.Remove(Target);
            }

            Target.gameObject.SetActive(false);
            if(PlayerActors.Count <= 0)
            {
                Debug.Log("Enemy Has Won");
            }
            else if(EnemyActors.Count <= 0)
            {
                Debug.Log("Player Has Won");
                
            }
            
            dialogueText.text = Target.name + " Has Died";

            
            //   EndBattle();
        }
        else
        {
            // nothing
            //state = BattleState.PLAYERTURN;
            //  PlayerTurn();
        }
        Debug.Log(Target.name + " Target Name: " + Target.GetComponent<TempActor>().currentHP);
    }
    // ........................................................................................................On player Die 


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You Won the Battle";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    // ........................................................................................................On Enemy die

    // On Check if won or lost

    // Stats

    // On Reset Battle Choice

    // Other data



    public IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);
        //playerHUD.SetHP(playerUnit.currentHP);
        playerUnit.gameObject.GetComponentInChildren<BattleHud>().SetHP(playerUnit.currentHP);
        dialogueText.text = "you healed!";
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
      //  StartCoroutine(EnemyTurn());
    }


    // disable the pannesl
    public void DisableAllPannels()
    {
        BattleOptionPannel.gameObject.SetActive(false);
        AttackOptionPannel.gameObject.SetActive(false);
        ActorToAttackPannel.gameObject.SetActive(false);
        //  SpecialOptionPannel.gameObject.SetActive(false);
        //  ItemOptionsPannel.gameObject.SetActive(false);
        //  StatsPannel.gameObject.SetActive(false);
    }

    // actack order class
    public class ActorAttack
    {

        public GameObject AttackingActor { get; set; }
        public GameObject AttackerTarget { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }

        public ActorAttack(GameObject attacker, GameObject target, int damage, int AtkrSpeed)
        {
            this.AttackingActor = attacker;
            this.AttackerTarget = target;
            this.Damage = damage;
            this.Speed = AtkrSpeed;
        }
    }
}
