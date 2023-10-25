using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    private bool grounded = false;
    public GameManager gameManager;
    private bool started = false;
    private bool died = false;
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
    // Start is called before the first frame update
    void Start()
    {
        gameScreen.SetActive(false);
        endScreen.SetActive(false);
        startScreen.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 0;
        distanceTraveled = 0;
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
        }
        else
        {
            if (started)
            {
                if(Input.GetButton("Jump") || Input.GetMouseButton(0))
                {
                    if(grounded) {
                        rb.AddForce(new Vector2(0,jumpForce));
                        jump.PlayOneShot(jump.clip, 1.0f);
                        grounded = false;
                    }
                        
                } else if(rb.velocity.y > 0){
                    Debug.Log("2");
                    rb.velocity = new Vector2(0,0);
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
            Debug.Log("Ground");
        }
        if(collision.gameObject.tag == "PowerUp")
        {
            Debug.Log("Slow Down");
            Destroy(collision.gameObject);
            distanceTraveled -= 100;
        } else if(collision.gameObject.tag == "Obstacle") {
            Die();
        }
    }

   

}
