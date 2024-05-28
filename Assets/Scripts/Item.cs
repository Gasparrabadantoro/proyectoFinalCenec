using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventorySlot;

public class Item : MonoBehaviour
{
    public InventorySlotType whoIAm;

    public InventoryController controllerItem;

    // Start is called before the first frame update
    void Start()
    {
        controllerItem=FindObjectOfType<InventoryController>();
    }

    // Update is called once per frame
    public void PickUpThisObject()
    {
        controllerItem.PickUpObject(whoIAm);

        Destroy(this.gameObject);
    }
}
