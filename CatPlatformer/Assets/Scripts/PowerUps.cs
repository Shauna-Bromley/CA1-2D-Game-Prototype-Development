using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] PlayerController player;
    bool pickUpActive = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pickUpActive == false && other.gameObject.tag == "Player" && this.tag == "powerUp")
        {
            StartCoroutine(Timer(2,other.gameObject));
            Destroy(this.gameObject);
            pickUpActive = true;
            
            
        }
    }

    IEnumerator Timer (float time, GameObject thisGO)
    {
        yield return new WaitForSeconds(time);
        if (pickUpActive)
        {
            pickUpActive = false;

        }
    }
}
