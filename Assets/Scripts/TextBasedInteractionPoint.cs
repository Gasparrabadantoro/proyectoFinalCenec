using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBasedInteractionPoint : MonoBehaviour
{
   private TextBasedInteractionsManager interactionManagerReference;
    public string thisInteractionPointText;
    private void Start()
    {
        interactionManagerReference = FindObjectOfType<TextBasedInteractionsManager>();
    }

    public void ClickOnInteractionPoint() 
    {
        print("Ha llegado a clickinteractionpoint");
        interactionManagerReference.FillTextContainer(thisInteractionPointText);
    }

}
