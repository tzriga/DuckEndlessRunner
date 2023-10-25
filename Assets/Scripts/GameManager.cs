using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float obstacleVelocity;
    public float defaultVelocity = -5f;
    public float velocityDelta = 0.5f;
    public List<SegmentScript> easySegments;
    public List<SegmentScript> mediumSegments;
    public List<SegmentScript> hardSegments;
    public int currentScore = 0;

    public float fixedDeltaTime = .001f;
    

    public SegmentScript getSegment() {
        if (currentScore < 250) {
            return easySegments[Random.Range(0, easySegments.Count)];
        } else if (currentScore >= 250 && currentScore < 1000) {
            int rng = Random.Range(0, 8);
            if (rng < 2) {
                return easySegments[Random.Range(0, easySegments.Count)];
            } else if (rng >= 2 && rng < 6) {
                return mediumSegments[Random.Range(0, mediumSegments.Count)];
            } else {
                return hardSegments[Random.Range(0, hardSegments.Count)];
            }
        } else if (currentScore >= 1000 && currentScore < 4000) {
            int rng = Random.Range(0, 8);
            if (rng < 2) {
                return easySegments[Random.Range(0, easySegments.Count)];
            } else if (rng >= 2 && rng < 4) {
                return mediumSegments[Random.Range(0, mediumSegments.Count)];
            } else {
                return hardSegments[Random.Range(0, hardSegments.Count)];
            }
        } else {
            int rng = Random.Range(0, 8);
            if (rng < 2) {
                return mediumSegments[Random.Range(0, mediumSegments.Count)];
            } else {
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
    }

    public float getObstacleVelocity() {
        return obstacleVelocity;
    }

    public float getVelocityDelta() {
        return velocityDelta;
    }

    public void IncreaseObstacleVelocity()
    {
        obstacleVelocity -= velocityDelta;
    }

    public void setCurrentScore(int value) {
        currentScore = value;
    }
}
