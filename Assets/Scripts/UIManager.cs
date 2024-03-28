using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text maxScore;
    [SerializeField] TMP_Text scoreActual;
    [SerializeField] GameObject panelLow;
    private void Update()
    {
        if (GameManager.Instance.murio)
        {
             panelLow.SetActive(true);
        }
    }
    public void UpdateTextScore(int actualScore, int highScore)
    {
       maxScore.text = highScore.ToString();
       scoreActual.text = actualScore.ToString();
    }
}
