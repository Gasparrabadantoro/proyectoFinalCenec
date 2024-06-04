using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public GameObject inventoryPortrait;
    public enum InventorySlotType
    {
        CartaManga,
        Tuberia,
        Rombo
    }

    public InventorySlotType whoIAm;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillSlot()
    {
        inventoryPortrait.SetActive(true);

        if (whoIAm == InventorySlotType.Tuberia)
        {
            FindObjectOfType<PlayerController>().playerAnimator.SetBool("PipeOn",true);
        }

    }

    public void ClickContainedItem()
    {
        if (whoIAm == InventorySlotType.Rombo)
        {
            // Cambia el cursor al rombo
            FindObjectOfType<CursorManager>().ChangeCursor(2,true);

            // Apagame el inventario 

            bool isActive = !FindObjectOfType<InventoryController>().inventoryUI.activeSelf;
            FindObjectOfType<InventoryController>().inventoryUI.SetActive(isActive);

            // Ponme a True la variable de romboEquipado. 

            FindObjectOfType<PlayerController>().romboEquipado = true;
        }
    }
}
