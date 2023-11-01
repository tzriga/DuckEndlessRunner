using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public float jumpForce;
    public float flyForce;
    private bool grounded = false;
    public GameManager gameManager;
    private bool started = false;
    private bool died = false;
    [SerializeField]
    private bool flying = false;
    public Timer timer;
    private int previousPowerUpTime = 0;
    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject endScreen;
    public AudioSource jump;
    public AudioSource deathSound;
    private float distanceTraveled;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text highScoreText;

    public TMPro.TMP_Text highScoreTextStart;

    // Start is called before the first frame update
    void Start()
    {
        gameScreen.SetActive(false);
        endScreen.SetActive(false);
        startScreen.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        highScoreTextStart.text += PlayerPrefs.GetInt("highScore");
        Time.timeScale = 0;
        distanceTraveled = 0;
        animator.SetBool("Grounded",false);
    }

    // Update is called once per frame
    void Update()
    {
        if(died)
        {
            if(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        } else {
            if (started)
            {
                
                if(flying) {
                    if((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) && rb.position.y < 12) {
                        rb.AddForce(new Vector2(0,flyForce));
                    }
                } else {
                    if(Input.GetButton("Jump") || Input.GetMouseButton(0)) {
                        if(grounded) {
                            rb.AddForce(new Vector2(0,jumpForce));
                            jump.PlayOneShot(jump.clip, 1.0f);
                            grounded = false;
                            animator.SetBool("Grounded",false);
                        }  
                    } else if(rb.velocity.y > 0){
                        Debug.Log("2");
                        rb.velocity = new Vector2(0,0);
                    }
                }
                
                distanceTraveled += gameManager.getObstacleVelocity() * Time.deltaTime;
                timer.setScore(Mathf.RoundToInt(-1 * distanceTraveled * 2));
                gameManager.setCurrentScore(timer.getScore());
            }
            else
            {
                if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
                {
                    Time.timeScale = 1;
                    started = true;
                    timer.StartTimer();
                    gameScreen.SetActive(true);
                    startScreen.SetActive(false);
                }
            }
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        flying = false;
        if (timer.getScore() > timer.getHighScore())
        {
            timer.setHighScore(timer.getScore());
            PlayerPrefs.SetInt("highScore", timer.getHighScore());
            PlayerPrefs.Save();
        }
        Debug.Log("High Score: " + PlayerPrefs.GetInt("highScore"));
        scoreText.text += timer.getScore();
        highScoreText.text += timer.getHighScore();
        timer.EndTimer();
        Time.timeScale = 0;
        started = false;
        died = true;
        deathSound.PlayOneShot(deathSound.clip, 1.0f);
        gameScreen.SetActive(false);
        endScreen.SetActive(true);

    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") {
            
            grounded = true;
            animator.SetBool("Grounded",true);
            Debug.Log("Ground");
        }
        
        if(collision.gameObject.tag == "RedBull")
        {
            Debug.Log("Red Bull");
            Destroy(collision.gameObject);
            flying = true;
            animator.SetBool("Flying",true);
            
        } else if(collision.gameObject.tag == "Obstacle") {
            Die();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "FlyEnd") {
            Debug.Log("FLYING");
            flying = false;
            animator.SetBool("Flying",false);
        }
    }

   

}
