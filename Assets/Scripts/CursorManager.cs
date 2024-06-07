using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D [] cursorTextures;
    //0 Cursor Normal NO VA CENTRADO
    //1 cursor ojo SI VA CENTRADO
    //2 Si es el rombo,SI VA CENTRADO 

    private Vector2 cursorHotsPot;

    public void ChangeCursor(int cursorId, bool centred) 
    {
        if (centred)
        {
            cursorHotsPot = new Vector2(cursorTextures[cursorId].width / 2, cursorTextures[cursorId].height / 2);
        }
        else
        {
            cursorHotsPot = Vector2.zero;
        }

        Cursor.SetCursor(cursorTextures[cursorId], cursorHotsPot,CursorMode.Auto);

       
    }

    private void Start()
    {
        
    }
}
