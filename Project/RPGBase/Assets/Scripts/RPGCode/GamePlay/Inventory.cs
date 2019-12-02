using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{

    // Add This script to the GameManager 
    // Add The Group of Item slots into the Slots list in the hiarky it will be added in code to the items slots
    // set your items up in the GameDataHolder in the constructStats function
    
    // For this Inventory to work you need Game Inventory Pannel holder, inside of it you need a collection of slots and Image for Item display and Text for item Description

    
    
  


    [Header("ItemSlots Put Here")]
    public int MaxItemsInInventoryCount = 10;
    public List<GameObject> Slots = new List<GameObject>();

    [Header("InventorySlots")]
    public List<GameObject> ItemSlots = new List<GameObject>();
    public List<GameDataHolder.Item> PlayerInventory = new List<GameDataHolder.Item>();
    public GameObject InventoryPannel;

    [Header("Info Tab data")]
    public Text ItemDescriptionText;
    public Image ItemIconDisplay;
    public bool inInventory;

    // Refrences to player data and movement gam
    // data refrence

    PlayerController playerControlls;
    Player player;
    GameDataHolder Gamedata;
    ThirdPersonCam cam;

    public int SelectedItem;

    // Use this for initialization
    void Start () {
        InventoryPannel.gameObject.SetActive(false);
        playerControlls = FindObjectOfType<PlayerController>();
        Gamedata = FindObjectOfType<GameDataHolder>();
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<ThirdPersonCam>();
        SelectedItem = 0;
        MaxItemsInInventoryCount = Slots.Count;
        ConstructInventory();
        addItem(1);
        addItem(1);
        addItem(1);
        addItem(2);
        addItem(3);
    }



    // Update is called once per frame
    void Update()
    {

        // Toggle on and off inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            playerControlls = FindObjectOfType<PlayerController>();


            if (inInventory == true && playerControlls.canMove == false)
            {
                playerControlls.canMove = true;
                cam.canMove = true;
                inInventory = false;
                InventoryPannel.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }

            // if inventory is off turn it on
            else if (inInventory == false && playerControlls.canMove == true)
            {
                playerControlls.canMove = false;
                cam.canMove = false;
                inInventory = true;
                InventoryPannel.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ConstructSlots();
            }

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            addItem(1);
        }

    }


        // Inventory Stuff...........................................................................................................
        // Adds Item to inventory
        // adds item by id to the player inventory
        public void addItem(int ItemId)
    {
       
        // player already has items just add another one
        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            if (PlayerInventory[i].ID == ItemId)
            {
                PlayerInventory[i].Count += 1;

                // reconstruct the enventory so the change  can be displayed
                if (inInventory == true)
                {
                    ConstructSlots();
                }
                return;
            }

        }



        if (Gamedata.Items[ItemId] != null)
        {
            for (int i = 0; i < MaxItemsInInventoryCount; i++)
            {
                if (PlayerInventory[i].ID == 0)
                {
                    PlayerInventory.Insert(i, Gamedata.Items[ItemId]);

                    // reconstruct the inventory so the change can be displayed
                    if (inInventory == true)
                    {
                        ConstructSlots();
                    }
                    Debug.Log("Added Item " + PlayerInventory[i].Name);
                    break;
                }

                if (i == MaxItemsInInventoryCount)
                {
                    // Player inventory is full
                    Debug.Log("Player Inventory is full");
                }
            }

        }


    }

    // removes item at index number
    public void RemoveItem(int index)
    {

        PlayerInventory.RemoveAt(index);
        PlayerInventory.Add(Gamedata.Items[0]);
        ConstructSlots();

    }


    public void ConstructSlots()
    {
      
        for(int i = 0; i < MaxItemsInInventoryCount; i++)
        {

            if (PlayerInventory[i].Equipted == true)
            {
                ItemSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerInventory[i].Name + ":" + "EPTD" + " " + PlayerInventory[i].Count.ToString();
            }
            else if (PlayerInventory[i].Equipted == false)
            {
                ItemSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerInventory[i].Name + " " + PlayerInventory[i].Count.ToString();
            }
            // add icon that should be in your resources folder that should be indexed in you database where it is with itemLoc
            if (ItemSlots[i].gameObject.GetComponentInChildren<Image>().sprite != null)
            {
                ItemSlots[i].gameObject.GetComponentInChildren<Image>().sprite = PlayerInventory[i].Icon as Sprite;
            }

            // this add function to button so when it is click use the useItem function << There are some issue with this causeing double items 
            // selected and I need to create a whole new function for this because it handles selecting and choseing items diffrently then other method of using item.
            // but will add work around later
            ItemSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            int index = i;

          ItemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => useItem(index));
            
                 
        }
        ItemSlots[0].gameObject.GetComponent<Button>().Select();



    }

   
    public void ConstructInventory()
    {
        for(int i = 0; i < MaxItemsInInventoryCount; i++)
        {
            PlayerInventory.Add(Gamedata.Items[0]);
            ItemSlots.Add(Slots[i]);
        }

       
    }



    //Use the item that is selected
    public void useItem(int index)
    {



        //if item is equiptable equipt the item

        if (PlayerInventory[index].Type == GameDataHolder.Item.ItemType.Equiptable && PlayerInventory[index].Equipted == true)
        {
            Debug.Log("equipted item named " + PlayerInventory[index].Name);
            // remove stat changes
            ItemSlots[index].gameObject.GetComponentInChildren<Text>().text = PlayerInventory[index].Name + " " + PlayerInventory[index].Count.ToString();
            PlayerInventory[index].Equipted = false;
            player.Attack -= PlayerInventory[index].AttackPlus;
            player.Defence -= PlayerInventory[index].DefencePlus;
            player.HP -= PlayerInventory[index].HPPlus;
        }

        else if (PlayerInventory[index].Type == GameDataHolder.Item.ItemType.Equiptable && PlayerInventory[index].Equipted == false)
        {

            Debug.Log("Unequipted item named " + PlayerInventory[index].Name);
            // apply state changes
            // display changes
            ItemSlots[index].gameObject.GetComponentInChildren<Text>().text = PlayerInventory[index].Name + ":EPTD" + " " + PlayerInventory[index].Count.ToString();
            PlayerInventory[index].Equipted = true;
            player.Attack += PlayerInventory[index].AttackPlus;
            player.Defence += PlayerInventory[index].DefencePlus;
            player.HP += PlayerInventory[index].HPPlus;



        }


        // if item is usable use the item
        if (PlayerInventory[index].Type == GameDataHolder.Item.ItemType.Usable)
        {
            // apply the state changes
            Debug.Log("Usable item used named " + PlayerInventory[index].Name);
            player.Attack += PlayerInventory[index].AttackPlus;
            player.Defence += PlayerInventory[index].DefencePlus;
            // player.HealthStat += playerInventory[index].StaminaStat;
            player.Currenthealth += PlayerInventory[index].HealthPlus;

            if (player.Currenthealth > player.HP)
            {
                player.Currenthealth = player.HP;
            }
            // remove item if count is less than one
            PlayerInventory[index].Count -= 1;

            // display that the number decreased
            ItemSlots[index].gameObject.GetComponentInChildren<Text>().text = PlayerInventory[index].Name + " " + PlayerInventory[index].Count.ToString();
            if (PlayerInventory[index].Count <= 0)
            {
                Debug.Log("Removed" + PlayerInventory[index].Name);
                RemoveItem(index);
                ItemSlots[index].gameObject.GetComponent<Button>().Select();
                SelectedItem = 0;
            }



        }

        // if item is quiest item so unusable
        if (PlayerInventory[index].Type == GameDataHolder.Item.ItemType.Quest)
        {

            Debug.Log("this is Quest item");
        }


        if (PlayerInventory[index].ID == 0)
        {
            Debug.Log("Emtpy Item");
        }
        else
        {

        }
       ItemSlots[index].gameObject.GetComponent<Button>().Select();

        ItemDescriptionText.text = PlayerInventory[index].Description;
    }

    
}
