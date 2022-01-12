using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar_ShootEmUp : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] Slider slider;

    // Setting the fill amount
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    // Setting the maximum health
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
    }

    // Getting the maximum health
    public float GetMaxHealth()
    {
        return slider.maxValue;
    }
}
