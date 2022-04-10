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
    private bool timerActive = false;

    private void Update()
    {
        if (timerActive)
        {
            if (timer < popUpLifeSpan)
            {
                timer += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                timer = 0f;
                timerActive = false;
            }
        }
       
    }

    public void ActivatePopUpWithTimer(string text)
    {
        this.text.text = text;
        gameObject.SetActive(true);
        timerActive = true;
    }
    
    public void ActivatePopUp(string text)
    {
        if (!timerActive && !gameObject.activeSelf)
        {
            this.text.text = text;
            gameObject.SetActive(true);
        }
    }

    public void DisablePopUp()
    {
        if (!timerActive)
        {
            gameObject.SetActive(false);
        }
    }
    
}
