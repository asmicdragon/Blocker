using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void ChangeSprite(){
        if(GameManager.gameManager.lives == 3){
            spriteRenderer.sprite = hearts[3]; 
        } else if(GameManager.gameManager.lives == 2){
            spriteRenderer.sprite = hearts[2]; 
        }else if(GameManager.gameManager.lives == 1){
            spriteRenderer.sprite = hearts[1]; 
        }else if(GameManager.gameManager.lives == 0){
            spriteRenderer.sprite = hearts[0]; 
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        ChangeSprite();
    }
}
