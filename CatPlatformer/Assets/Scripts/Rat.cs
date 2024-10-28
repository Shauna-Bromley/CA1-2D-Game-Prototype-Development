using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rat : MonoBehaviour
{
    [SerializeField] PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && this.tag == "Rat")
        {
            Destroy(this.gameObject);
            player.addLife();
        }
    }
}
