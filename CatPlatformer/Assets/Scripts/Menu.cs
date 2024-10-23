using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
