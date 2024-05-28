using UnityEngine;
using static InventorySlot;

public class InventoryController : MonoBehaviour
{   
    public GameObject inventoryUI; // Asigna la UI del inventario en el inspector

    public InventorySlot[] inventorySlot;

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.I))
        {   
            bool isActive = !inventoryUI.activeSelf;
            inventoryUI.SetActive(isActive);

            // Pausa o reanuda el juego
            if (isActive)
            {
                Time.timeScale = 0f; // Pausa el juego
            }
            else
            {
                Time.timeScale = 1f; // Reanuda el juego
            }
        }
    }

    public void PickUpObject(InventorySlotType pickedObjectType)
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            if (inventorySlot[i].whoIAm == pickedObjectType)
            {
                inventorySlot[i].FillSlot();
            }
        }

    }

    
}
