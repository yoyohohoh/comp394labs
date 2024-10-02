using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
