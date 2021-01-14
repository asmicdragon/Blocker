using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBlockFlip : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameManager gameManager;
    public int score = 0;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
 
    }
    // Start is called before the first frame update
    void Start()
    {
        score  = gameManager.score;
        if(spriteRenderer != null && score%2==0){

            spriteRenderer.flipY = true;
            

        } else {

            spriteRenderer.flipY = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
