using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    private float startStamina = 100;
    public float currentStamina;
    public Slider sliderStamina;
    float reg = 0.13f;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = startStamina;
    }

    // Update is called once per frame
    void Update()
    {
        RegenStamina(reg);
    }

    public void TakeStamina(float amount){
        currentStamina -= amount;
        sliderStamina.value = currentStamina;
    }

    public void RegenStamina(float regen){
        currentStamina += regen;
        sliderStamina.value = currentStamina;

        if(currentStamina >= 100){
            currentStamina = 100;
        }
    }
}
