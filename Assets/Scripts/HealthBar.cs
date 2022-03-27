using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField] private Slider Slider;

    [SerializeField] private Gradient Gradient;

    [SerializeField] private Image Fill;
    // [SerializeField] private Vector3 Offset;
    
    void Update()
    {
        // Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }

    public void SetHealth(float health)
    {
        Slider.value = health;
        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }

    public void SetMaxHealth(float maxHealth)
    {
        Slider.maxValue = maxHealth;
        Slider.value = maxHealth;
        Gradient.Evaluate(1f);
    }
}
