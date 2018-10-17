using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultitem = false;

    public virtual void Use()
    {
        //Use Item
        // Something Might Happen

        Debug.Log("Using " + name);
    }

}