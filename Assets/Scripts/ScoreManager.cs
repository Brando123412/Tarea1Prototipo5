using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int actualScore=0;
    [SerializeField] int highScore=0;
    [SerializeField] UIManager UMI;
    private void Start()
    {
        highScore = MusicManagerPersistent.Instance.puntaje;
    }

    private void Update()
    {
        
        if (GameManager.Instance.comio)
        {
            UpdateScore();
        }
        if (GameManager.Instance.murio)
        {
            MusicManagerPersistent.Instance.PuntajeMax(highScore);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void IrAMenu()
    {
        SceneManager.LoadScene(0);
        //StartCoroutine(MiCorrutina1());
    }
    IEnumerator MiCorrutina1()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
}
