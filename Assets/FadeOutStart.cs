using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutStart : MonoBehaviour
{
    Image image;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        GetComponent<Image>().enabled = true;
        anim = GetComponent<Animator>();
        
        StartCoroutine(WaitForAnim1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitForAnim1()
    {
        anim.Play("FadeOut");
        yield return new WaitForSecondsRealtime(1f);
        GetComponent<Image>().enabled = false;

    }
    
}
