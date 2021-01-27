using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    //Using the particle system as system
    public bool particleSystemPlayed = false;

    
    public static ParticleSystemController controller {get; set;}
    ParticleSystem system
    {
        get
        {
            if (_CachedSystem == null)
                _CachedSystem = GetComponent<ParticleSystem>();
            return _CachedSystem; //getting and using the _cached system into system
        }
    }
    private ParticleSystem _CachedSystem;

    void Start()
    {
        //calling the controller to this script to call it in other scripts
        controller = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(StartingBlock.CurrentBlock != null){
            //Takes the position of the currentblock
            transform.position = StartingBlock.CurrentBlock.transform.position;
        }

        if(StartingBlock.CurrentBlock == null && !particleSystemPlayed){
            // Plays the particle system if the conditions are met
            system.Play();
            
            StartCoroutine(WaitForParticlePlay());
            //Plays only when it turns to false, so this bool doesn't let the condition run a second time
            particleSystemPlayed = true;
        }
    }
    // Waits for the life time of the particles, just before and stops so that it doesnt emit a second time
    IEnumerator WaitForParticlePlay(){
        yield return new WaitForSecondsRealtime(2.98f);
        system.Stop();
    }
}
