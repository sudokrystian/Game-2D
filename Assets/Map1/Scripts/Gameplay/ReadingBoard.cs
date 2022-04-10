using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadingBoard : MonoBehaviour
{
    public PopUpWindow popUpWindow;
    [SerializeField] private string bilboardMessage = "Message";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Character2DController>();
        if (player)
        {
            popUpWindow.ActivatePopUp(bilboardMessage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        popUpWindow.DisablePopUp();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Character2DController>();
        if (player)
        {
            popUpWindow.ActivatePopUp(bilboardMessage);
        }
    }
}
