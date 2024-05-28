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
    }
}
