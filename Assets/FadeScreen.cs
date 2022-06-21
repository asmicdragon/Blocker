using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen instance {get; set;}
    [SerializeField]
    public bool fadeIn, fadeOut;
    public Image image;
    Button button;
    GameObject fade;

    [HideInInspector]
   public bool playFade;
   float duration = 0.25f;

    void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        GetComponent<Image>().enabled = true;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        CheckForActive();
        playFade = false;
    }

    // Update is called once per frame
    void Update()
    {

        FadeIn();
        FadeOut();   
    }
    void FadeIn()
    {
        
        if(fadeIn && playFade && GetComponent<Image>().enabled == true) 
        {
            image.CrossFadeAlpha(1, duration, true);

            IEnumerator WaitForFade(){

            yield return new WaitForSecondsRealtime(1f);
            

                if(GameObject.Find("MainMenu") != null)
                {
                    MainMenu.instance.PlayGame();
                    

                }
            }
            StartCoroutine(WaitForFade());


        }
    }
    void FadeOut()
    {
        if(fadeOut && playFade && GetComponent<Image>().enabled == true)
        {
            image.CrossFadeAlpha(0, duration, true);

            IEnumerator WaitForFade(){yield return new WaitForSecondsRealtime(2f);this.gameObject.SetActive(false);}
            StartCoroutine(WaitForFade());
        }
    }
    void CheckForActive()
    {
        if(fadeIn)
        {
            image.CrossFadeAlpha(0,0, true);
        }
        if(fadeOut)
        {
            image.CrossFadeAlpha(1,0, true);
        }
    }
}
