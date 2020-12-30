using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTheTrigger : MonoBehaviour
{
    public StartingBlock startingBlock;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
        if(other.name == "StartingBlock"){
            startingBlock.Stop();
            startingBlock._verticalMovement = 0;
            Debug.Log("Trigger with StartingBlock!");

        } else if(other.name == "Stack") {

            Debug.Log("Trigger!");

        }
    }
}
