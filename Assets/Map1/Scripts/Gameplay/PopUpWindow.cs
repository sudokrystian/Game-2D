using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWindow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    private float timer = 0;
    [SerializeField] private float popUpLifeSpan = 1f;
    private bool active = false;

    private void Update()
    {
        if (active)
        {
            if (timer < popUpLifeSpan)
            {
                timer += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                timer = 0f;
                active = false;
            }
        }
       
    }

    public void ActivatePopUp(string text)
    {
        this.text.text = text;
        gameObject.SetActive(true);
        active = true;
    }
    
}
