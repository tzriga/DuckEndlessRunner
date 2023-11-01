using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject redbull;
    [SerializeField]
    private float obstacleVelocity;
    public float defaultVelocity = -5f;
    public float velocityDivider;
    public List<SegmentScript> easySegments;
    public List<SegmentScript> waterSegments;
    public List<SegmentScript> hardSegments;
    public int currentScore = 0;

    public float fixedDeltaTime = .001f;

    public bool waterLevel = false;
    

    public SegmentScript getSegment() {
        if (currentScore < 300) {
            return easySegments[Random.Range(0, easySegments.Count)];
        } else if (currentScore >= 300 && currentScore < 1000) {
            int rng = Random.Range(0, 8);
            if (rng < 2) {
                waterLevel = false;
                return easySegments[Random.Range(0, easySegments.Count)];
            } else if (rng >= 2 && rng < 6) {
                if(!waterLevel){
                    GameObject temp = Instantiate(redbull, new Vector3(15f, 0, 0), transform.rotation);
                    temp.GetComponent<Rigidbody2D>().velocity = new Vector3(getObstacleVelocity(), 0,0);
                    waterLevel = true;
                }
                return waterSegments[Random.Range(0, waterSegments.Count)];
            } else {
                waterLevel = false;
                return hardSegments[Random.Range(0, hardSegments.Count)];
            }
        } else if (currentScore >= 1000 && currentScore < 4000) {
            int rng = Random.Range(0, 8);
            if (rng < 2) {
                waterLevel = false;
                return easySegments[Random.Range(0, easySegments.Count)];
            } else if (rng >= 2 && rng < 4) {
                if(!waterLevel){
                    GameObject temp = Instantiate(redbull, new Vector3(15f, 0, 0), transform.rotation);
                    temp.GetComponent<Rigidbody2D>().velocity = new Vector3(getObstacleVelocity(), 0,0);
                    waterLevel = true;
                }
                return waterSegments[Random.Range(0, waterSegments.Count)];
            } else {
                waterLevel = false;
                return hardSegments[Random.Range(0, hardSegments.Count)];
            }
        } else {
            int rng = Random.Range(0, 8);
            if (rng < 2) {
                if(!waterLevel){
                    GameObject temp = Instantiate(redbull, new Vector3(15f, 0, 0), transform.rotation);
                    temp.GetComponent<Rigidbody2D>().velocity = new Vector3(getObstacleVelocity(), 0,0);
                    waterLevel = true;
                }
                return waterSegments[Random.Range(0, waterSegments.Count)];
            } else {
                waterLevel = false;
                return hardSegments[Random.Range(0, hardSegments.Count)];
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Time.fixedDeltaTime = fixedDeltaTime;
        obstacleVelocity = defaultVelocity;
        Application.targetFrameRate = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            Application.Quit();
        }
        IncreaseObstacleVelocity();
    }

    void FixedUpdate()
    {
        IncreaseObstacleVelocity();
    }

    public float getObstacleVelocity() {
        return obstacleVelocity;
    }

    public float getVelocityDivider() {
        return velocityDivider;
    }

    public void IncreaseObstacleVelocity()
    {
        obstacleVelocity -= Time.deltaTime / velocityDivider;
    }

    public void setCurrentScore(int value) {
        currentScore = value;
    }
}
