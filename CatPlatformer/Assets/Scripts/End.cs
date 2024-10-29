using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    //brings you back to the main menu 
    private void OnEnable()
    {
        SceneManager.LoadScene("Menu");
    }
}
