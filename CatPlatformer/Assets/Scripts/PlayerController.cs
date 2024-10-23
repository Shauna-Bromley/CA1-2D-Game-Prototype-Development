using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int direction = 0;
    public int speed;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        float move = Input.GetAxis("Horizontal");
        position.x = position.x + speed * Time.deltaTime * move;
        transform.position = position;

        if (move != 0)
        {
            direction = move < 0 ? -1 : 1;
            animator.SetFloat("Move X",direction);
             
        }
        else
        {
            animator.SetFloat("Move X", 0);
        }
    }
}
