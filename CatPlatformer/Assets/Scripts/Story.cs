using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    //Third Party Code [https://www.youtube.com/watch?v=NnPDJfvLeWQ&list=PLQw75EK0UKKAwWK18gXvz7fvvwCB5n-ue&index=4&ab_channel=StupidaZZle]
    //I learned about the timeline feature in Unity from the above video 
    //And implemented the OnEnable method to load the next scene after the timeline
    private void OnEnable()
    {
        SceneManager.LoadScene("GameScene");
    }
}
