using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Image[] images;
    [SerializeField] TMP_Text ghostText;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] TMP_Text deathText;
    [SerializeField] TMP_Text pauseText;
    [SerializeField] TMP_Text timerText;

    

    private int direction = -1;
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private int jumpCount = 0;

    AudioSource source;
    public AudioClip spell;
    public int speed;
    public float JumpHeight;
    public bool isJumping = false;

    float timeRemaining = 300;
    int lives = 3;
    int ghosts = 0;
    int totalGhosts = -1;
    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator.SetFloat("Move X",0);
        animator.SetFloat("Move Y",1);
        if (totalGhosts == -1)
        {
            totalGhosts = GameObject.FindGameObjectsWithTag("Ghost").Length;
        }
        ghostText.text = ghosts + "/" + totalGhosts;
    }

    // Update is called once per frame
    void Update()
    {
        //Third-Party Code: [https://www.youtube.com/watch?v=POq1i8FyRyQ&list=PLQw75EK0UKKAwWK18gXvz7fvvwCB5n-ue&ab_channel=RehopeGames]
        //The below code was used to make a timer that counts down from 5 minutes 
        //It sets the time remaining to zero if the time remaining goes below zero
        //If this isn't set then the timer will keep going below zero
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining < 0)
        {
            timeRemaining = 0;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:0}:{1:00}",minutes,seconds);

        Vector2 position = transform.position;
        float moveBy = Input.GetAxis("Horizontal");
        position.x = position.x + speed * moveBy * Time.deltaTime;
        transform.position = position;
        playAnimation(moveBy);

        if (jumpCount <2  && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight)), ForceMode2D.Impulse);
            jumpCount++;
        }
        if (moveBy != 0)
        {
            direction = moveBy < 0 ? -1 : 1;
             
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            source.PlayOneShot(spell, 0.5f);
            GameObject projectileObject = Instantiate(projectilePrefab,
                rigidbody2D.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(new Vector2(direction, 0), 300);
            

        }
        //Third Party Code: [https://www.youtube.com/watch?v=ROwsdftEGF0&list=PLQw75EK0UKKAwWK18gXvz7fvvwCB5n-ue&index=3&ab_channel=GameDevBeginner]
        //I learned about the concept of Time scale from the above video to allow the game to pause
        //correctly and allow me to show up a message during the pause screen
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                pauseText.text = string.Empty;
            }
            else
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                pauseText.text = "\n\nTutorial\r\n\r\nMovement: A & S or Left and Right arrow keys\r\n\r\nJump: Spacebar\r\n\r\nFire Spell: F \r\n\r\nPause: P \r\n\r\nCollect rats to regain health";
            }
        }
        
        if (lives == 0)
        {
            Time.timeScale = 0;
            deathText.text = "You died!\nPress Enter to try again";
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }
        }
        
        if (totalGhosts == ghosts)
        {
            SceneManager.LoadScene("End");
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
        if (col.collider.name.Contains("Ghost"))
        {
            removeLife();
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (isJumping)
        {
            isJumping = false;
            jumpCount = 0;
        }
    }

    public void AddGhost()
    {
        ghosts++;
        ghostText.text = ghosts + "/" + totalGhosts;
    }

    public void updateLivesUI()
    {
        switch (lives)
        {
            case 0:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = false;
                break;
            case 1:
                images[0].enabled = true;
                images[1].enabled = false;
                images[2].enabled = false;
                break;
            case 2:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = false;
                break;
            case 3:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = true;
                break;
        }
    }

    public void addLife ()
    {
        lives++;
        updateLivesUI();
    }
    public void removeLife()
    {
        lives--;
        updateLivesUI();
    }
}
