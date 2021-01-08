using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject damageBar;
    public float damage = 500;
    private float health = 1000;
    private const float healthBarMovement = 1.42f;
    private float remainingHealth;


    private Slider sliderH;

    Vector3 damScale;

    void Awake() {
        sliderH = GetComponent<Slider>();
    }

    public void updateBar(float remainingHealth) {
        Debug.Log("Updated with: " + remainingHealth);
        sliderH.value = remainingHealth;
    }
}
