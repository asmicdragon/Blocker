using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{

    ParticleSystem system
    {
        get
        {
            if (_CachedSystem == null)
                _CachedSystem = GetComponent<ParticleSystem>();
            return _CachedSystem;
        }
    }
    private ParticleSystem _CachedSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(system){
            if(StartingBlock.CurrentBlock == null){
            system.Play();
            system.Stop();
            
            }
        }
    }

}
