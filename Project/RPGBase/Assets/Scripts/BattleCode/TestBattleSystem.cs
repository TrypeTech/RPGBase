using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class TestBattleSystem : MonoBehaviour {


    // Start Players and enamies spawn with there stats set To there actors
    // Start Battle Choice System Player1 Then Player 2
    // Egnitiate battle attack item moves go first
    // Do Check if hit player has died after every attack
    // Do also check if all enemies or all players are dead
    // if so do battle over, then stats menu, then leave battle


    // Actors have a library of models and Models are visable when enabled
    // Actors also have a selection Icon item above the Actor and or a halo
    // and a partical library 
    // also a animation library

   // public Transform[] PlayerActorsPositions;
   // public Transform[] EnemyActorsPositions;

    public List<Transform> PlayerActorsPositions;
    public List<Transform> EnemyActorsPositions;

  //  public GameObject[] PlayerActors;
   // public GameObject[] EnemieActors;

    public List<GameObject> PlayerActors = new List<GameObject>();
    public List<GameObject> EnemyActors = new List<GameObject>();
    public Transform StoreActorTransform;
    
	// Use this for initialization
	void Start () {
        MoveActorsToThereStartPositions();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.T))
        {
            AttackTowardsEnemy(PlayerActors[0].gameObject, EnemyActors[0].gameObject);
        }
	}

    public void AttackTowardsEnemy(GameObject CurrentLoc,GameObject ToLocation)
    {


        CurrentLoc.gameObject.GetComponent<NavMeshAgent>().SetDestination(ToLocation.transform.position);
        Invoke("MoveActorsToThereStartPositions", 3f);

    }

    public void AttackAtEnemy(Transform CurrentLoc, Transform ToLocation)
    {

    }

   

    public void MoveActorsToThereStartPositions()
    {
         for(int i = 0; i < PlayerActors.Count; i ++)
        {
           // PlayerActors[i].transform.position = PlayerActorsPositions[i].transform.position;
          // if()
            PlayerActors[i].gameObject.GetComponent<NavMeshAgent>().SetDestination(PlayerActorsPositions[i].transform.position);
        }

        for (int i = 0; i < EnemyActors.Count; i++)
        {
           // EnemyActors[i].transform.position = EnemyActorsPositions[i].transform.position;
            EnemyActors[i].gameObject.GetComponent<NavMeshAgent>().SetDestination(EnemyActorsPositions[i].transform.position);
        }
    }

    
}
