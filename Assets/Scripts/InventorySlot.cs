using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public GameObject inventoryPortrait;
    public TextBasedInteractionsManager textBasedInteractionRef;
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
        textBasedInteractionRef = FindAnyObjectByType<TextBasedInteractionsManager>();
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
        else if (whoIAm == InventorySlotType.CartaManga)
        {
            textBasedInteractionRef.FillTextContainer("La carta dice: kdfjhgslkdfjgh");
        }
        else if (whoIAm == InventorySlotType.Tuberia)
        {
            textBasedInteractionRef.FillTextContainer("Si hago click sobre un enemigo podré golpearle con esto");
        }
    }
}
