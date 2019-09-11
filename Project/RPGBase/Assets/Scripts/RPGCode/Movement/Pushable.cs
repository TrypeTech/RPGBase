using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    Rigidbody rig;
    public float moveSpeed = 5f;
    public bool canPush;
    public Transform player;
    public Vector3 moveDirection;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform ;
        anim = player.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canPush == true)
        {
            MoveObject();
        }
    }

    
    public void MoveObject()
    {
       // rig.AddForce(Vector3.right * moveSpeed * Time.deltaTime);
       // rig.velocity = new Vector3(1*moveSpeed * Time.deltaTime,0,0);
        rig.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            canPush = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
          

            canPush = false;
        }
    }


 
}
