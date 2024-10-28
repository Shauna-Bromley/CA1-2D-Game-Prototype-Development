using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TMP_Text ghostText;
    [SerializeField] GameObject projectilePrefab;
    private int direction = -1;
    public int speed;
    public float JumpHeight;
    public bool isJumping = false;
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    int ghosts = 0;
    int totalGhosts = -1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator.SetFloat("Move X",0);
        animator.SetFloat("Move Y",1);
        if (totalGhosts == -1)
        {
            totalGhosts = GameObject.FindGameObjectsWithTag("Ghost").Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        float moveBy = Input.GetAxis("Horizontal");
        position.x = position.x + speed * moveBy * Time.deltaTime;
        transform.position = position;
        playAnimation(moveBy);
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)), ForceMode2D.Impulse);

        }
        if (moveBy != 0)
        {
            direction = moveBy < 0 ? -1 : 1;
             
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectileObject = Instantiate(projectilePrefab,
                rigidbody2D.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(new Vector2(direction, 0), 300);

        }


    }

    private void playAnimation(float moveBy)
    {
        if (moveBy == 0)
        {
            animator.SetFloat("Move X", 0);

        }
        else if (moveBy < 0)
        {
            animator.SetFloat("Move X", -1);
            animator.SetFloat("Move Y", -1);
        }
        else
        {
            animator.SetFloat("Move X", 1);
            animator.SetFloat("Move Y", 1);
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isJumping)
        {
            isJumping = false;
        }
    }

    public void AddGhost()
    {
        ghosts++;
    }
}
