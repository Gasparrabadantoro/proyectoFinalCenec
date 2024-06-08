using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteractionPoint : MonoBehaviour
{
    public GameObject puzzleTuberiasPack;
    private void Start()
    {
      
    }

    public void CallPuzzle()
    {
        puzzleTuberiasPack.SetActive(true);
    }

    public void ClosePuzzle() 
    {
        puzzleTuberiasPack.SetActive(false);
    }

    public void FinishPuzzle() 
    {
       Invoke(nameof(CallPuzzleFinishAction), 0.5f);
    }

    public void CallPuzzleFinishAction() 
    {
        puzzleTuberiasPack.SetActive(false);
        Destroy(this.gameObject);
    }
}
