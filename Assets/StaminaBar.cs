using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    private float maxStamina = 100;
    private float currentStamina;

    public bool enoughStamina = true;

    public bool usingStamina = false;

    public static StaminaBar instance;
    private void Awake() {

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;

    }    
    public void UseStamina(float amount){

        if(currentStamina - amount >= 0){
            currentStamina -= amount;
            staminaBar.value = currentStamina;

        } else {
            enoughStamina = false;
            Debug.Log("not enough stamina");
        }
    }

    public void RechargeStamina(float amount){
        if(currentStamina <= 0 || usingStamina == false){

            staminaBar.value += amount * Time.deltaTime;
            currentStamina = staminaBar.value;
        }
        if(currentStamina >= 30){
            enoughStamina = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.gameManager.gameOver){
            this.gameObject.SetActive(false);
        }
        RechargeStamina(10);
    }
}
