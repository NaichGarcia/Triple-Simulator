using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isGameActive = true;
    [HideInInspector] public bool scoredTriple;
    

    public void ScoredTriple()
    {
        scoredTriple = true;

        Debug.Log("Triple!");
    }

    public void GameOver()
    {
        if(!isGameActive) { return; }

        isGameActive = false;
        Debug.Log("Game Over");
    }
}
