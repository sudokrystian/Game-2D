using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    public GameObject canvasParent;
    private TextMeshProUGUI textMesh;
    public int damage;
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        textMesh.text = Convert.ToString(damage);
    }
}
