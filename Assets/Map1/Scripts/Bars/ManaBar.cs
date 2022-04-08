using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Slider Slider;

    [SerializeField] private Gradient Gradient;

    [SerializeField] private Image Fill;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetMana(float mana)
    {
        Slider.value = mana;
        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }

    public void SetMaxMana(float maxMana)
    {
        Slider.maxValue = maxMana;
        Slider.value = maxMana;
        Gradient.Evaluate(1f);
    }
}
