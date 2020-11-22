using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void exit()
    {
        Application.Quit();
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }
}
