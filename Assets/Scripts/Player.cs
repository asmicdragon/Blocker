using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0.05f,4.4f);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {
        float _verticalMovement = -1.5f;
        float _horizontalInput = Input.GetAxis("Horizontal");
        //Movement method, using Vector2 as it is a 2D Game
        // only making horizontal input so that the player cant move up and down aswell

        Vector2 direction = new Vector2(_horizontalInput,0);
        Vector2 goingDown = new Vector2(0,_verticalMovement);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.Translate(goingDown * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x,-7.8f, 7.8f),transform.position.y);
   
    }
    void OnTriggerEnter(Collider other) {

        if(other.tag == "Floor"){

            Destroy(this.gameObject);
        }
    }
}
