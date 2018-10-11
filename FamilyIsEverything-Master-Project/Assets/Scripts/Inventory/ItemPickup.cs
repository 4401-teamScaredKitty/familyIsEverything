using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;

    public override void Interact()
    {
        base.Interact();            //Interacts with the "Item"

        PickUp();
    }

    void PickUp()
    {

        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);        //Picks Up Item and adds to Inventory

        if (wasPickedUp)
            Destroy(gameObject);        //Destroys Game Object from Scene but not Inventory
    }

}