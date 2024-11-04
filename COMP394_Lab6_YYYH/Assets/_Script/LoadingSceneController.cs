using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(2);
    }
    public void WinGame()
    {
        SceneManager.LoadScene(3);
    }
    public void Setting()
    {
        SceneManager.LoadScene(4);
    }
}
