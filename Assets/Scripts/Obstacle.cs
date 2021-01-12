using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static Obstacle obstacle {   get; set;   }
    public GameObject obstacleLeft;
    public GameObject obstacleRight;
    public float SpawnDirection;

    // Start is called before the first frame update
    private void Awake() {
        obstacleLeft.SetActive(true);
        obstacleRight.SetActive(true);
        obstacle = this;
    }
    private void Start() {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}   
    
