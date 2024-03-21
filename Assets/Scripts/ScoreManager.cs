using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int actualScore;
    int highScore;
    [SerializeField] UIManager UMI;
    private void Update()
    {
        if (GameManager.Instance.comio)
        {
            UpdateScore();
        }
        if (GameManager.Instance.murio)
        {
            ResetScoreApple();
        }
    }
    public void UpdateScore()
    {
        UMI.UpdateTextScore(actualScore, highScore);
    }
    public void ResetScoreApple()
    {
        UMI.UpdateTextScore(0, 0);
    }
}
