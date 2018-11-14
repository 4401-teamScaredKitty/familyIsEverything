using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;

    public GameObject inventoryWindow;

    public static bool InventoryIsOpen = false;

    Inventory inventory;

    InventorySlot [] slots;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
   //     inventory.onItemChangeCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryIsOpen)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        //If inventory is closed resume game speed
        inventoryWindow.SetActive(false);
        Time.timeScale = 1f;
        InventoryIsOpen = false;
    }

    void Pause()
    {
        //freeze game time if inventory open
        inventoryWindow.SetActive(true);
        Time.timeScale = 0f;
        InventoryIsOpen = true;
    }

    void UpdateUI()
    {
        for(int i = 0; i<slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
