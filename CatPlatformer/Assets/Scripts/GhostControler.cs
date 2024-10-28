using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControler : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] float fireTimer = 0.5f;
    float fireCountdown = 0;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] int strength;
    bool isAlive = true;
    Animator animator;
    public float distanceTime;
    public float speed;
    float direction = -1;
    float timeInDirection;
    float timeToDie = 1;
    float deathTime = 1;
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
        if (isAlive)
        {
            Vector2 position = transform.position;
            position.x = position.x + speed * Time.deltaTime * direction;
            transform.position = position;
            timeInDirection -= Time.deltaTime;
            if (timeInDirection < 0)
            {
                direction *= -1;
                timeInDirection = distanceTime;
                animator.SetFloat("Move X", direction);
            }
            RaycastHit2D hit = Physics2D.Raycast(position, new Vector2(direction, 0),
                5f, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<PlayerController>() != null)
                {
                    fire();
                }
            }
            fireCountdown -= Time.deltaTime;
        }
        else
        {
            timeToDie -= Time.deltaTime;
            if (timeToDie < 0)
            {
                animator.SetFloat("Move Y", direction);
                animator.SetFloat("Move X", 0);
                Destroy(gameObject);
                player.AddGhost();

            }
        }

        
    }

    public void fire ()
    {
        if (fireCountdown < 0)
        {
            fireCountdown = fireTimer;
            GameObject projectileObject = Instantiate(projectilePrefab,
                GetComponent<Rigidbody2D>().position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(new Vector2(direction,0), 300);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name.Contains("Spell"))
        {
            hit();
        }
    }

    public void hit()
    {
        strength--;
        if (strength <= 0)
        {
            isAlive = false;
            timeToDie = deathTime;
            animator.SetFloat("Move Y",direction);
            animator.SetFloat("Move X", 0);

        }
    }    

}
