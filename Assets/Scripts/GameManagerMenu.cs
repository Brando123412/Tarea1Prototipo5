using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
        MusicManagerPersistent.Instance.PlayRandomMusic();
    }
    public void GoToGame()
    {
        StartCoroutine(MiCorrutina1());
    }
    public void QuitGame()
    {
        StartCoroutine(MiCorrutina2());
    }
    IEnumerator MiCorrutina1()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
    IEnumerator MiCorrutina2()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    public void  SoundButton() {
        MusicManagerPersistent.Instance.soundButtons.Play();
    }
}
