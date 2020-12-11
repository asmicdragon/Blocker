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
        transform.position = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        //Movement method, using Vector2 as it is a 2D Game
        Vector2 direction = new Vector2(_horizontalInput,0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x,-7.0f, 7.0f),transform.position.y);
   
    }
}
