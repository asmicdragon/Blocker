using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public bool destroy;
    // Start is called before the first frame update
    void Start()
    {
        destroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(destroy){
            Destroy(this.gameObject);
        }
    }
}
