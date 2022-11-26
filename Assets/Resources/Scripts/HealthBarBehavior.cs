using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    public Slider healthBar;
    public Color lowHealthColor;
    public Color highHealthColor;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // locking the healthbar to the parent object (the enemy) by updating the 
        // position each frame.
        healthBar.transform.position = Camera.main.WorldToScreenPoint
            (transform.parent.position + offset);
    }

    public void SetHealth(float health, float maxHealth)
    {
        healthBar.gameObject.SetActive(health < maxHealth);
        healthBar.value = health;
        healthBar.maxValue = maxHealth;

        healthBar.fillRect.GetComponentInChildren<Image>().color = 
            Color.Lerp(lowHealthColor, highHealthColor, healthBar.normalizedValue);
    }
}
