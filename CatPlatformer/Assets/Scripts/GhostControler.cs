using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControler : MonoBehaviour
{
    [SerializeField] float fireTimer = 0.5f;
    float fireCountdown = 0;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] int strength;
    bool isAlive = true;
    Animator animator;
    public float distanceTime;
    public float speed;
    float direction = 1;
    float timeInDirection;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timeInDirection = distanceTime;
        animator.SetFloat("Move X",direction);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * Time.deltaTime * direction;
        transform.position = position;
        timeInDirection -= Time.deltaTime;
        if (timeInDirection < 0 )
        {
            direction *= -1;
            timeInDirection = distanceTime;
            animator.SetFloat("Move X", direction);
        }
        
    }
}
