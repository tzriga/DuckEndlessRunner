using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAirplane : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
