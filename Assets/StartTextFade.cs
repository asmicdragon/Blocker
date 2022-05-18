using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartTextFade : MonoBehaviour
{
    TMP_Text text;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
   
        GetComponent<TMP_Text>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FadeScreen.instance.fadeOut)
        {
            anim.SetBool("Play", true);
        }
    }
}
