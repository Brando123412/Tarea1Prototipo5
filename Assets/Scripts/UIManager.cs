using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text maxScore;
    [SerializeField] TMP_Text scoreActual;
    private void Start()
    {
        
    }
    public void UpdateTextScore(int actualScore, int highScore)
    {
       maxScore.text = highScore.ToString();
       scoreActual.text = actualScore.ToString();
    }
}
