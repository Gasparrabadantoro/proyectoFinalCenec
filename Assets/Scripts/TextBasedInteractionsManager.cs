using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBasedInteractionsManager : MonoBehaviour
{
    public GameObject textContainerObject;
    public TextMeshProUGUI textContainer;

    public void FillTextContainer(string textContent) 
    {
        print("Ha llegado a FillTextContainer " + textContent);
        textContainer.text = "";
        textContainer.text = textContent;
        TurnOnTextUI();
    }

    public void TurnOnTextUI() 
    {
        textContainerObject.SetActive(true);
        //bloquear que se pueda mover el jugador
        //bloquear cosas que no queires que ocurran
    }

    public void TurnOffTextUI()
    {
        textContainerObject.SetActive(false);
        
    }
}
