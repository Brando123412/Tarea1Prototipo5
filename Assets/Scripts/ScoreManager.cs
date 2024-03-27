using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int actualScore=0;
    [SerializeField] int highScore=0;
    [SerializeField] UIManager UMI;

    private void Update()
    {
        /*if (GameManager.Instance.murio)
        {
            ResetScoreApple();
        }*/
        if (GameManager.Instance.comio)
        {
            UpdateScore();
        }

    }
    public void UpdateScore()
    {
        actualScore++; 
        if (actualScore > highScore)
        {
            highScore++;
        }
        UMI.UpdateTextScore(actualScore, highScore);
        GameManager.Instance.comio = false;
    }
    public void ResetScoreApple()
    {
        UMI.UpdateTextScore(0, highScore);
        actualScore = 0;
    }
}
