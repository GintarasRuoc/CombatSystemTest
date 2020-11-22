using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class enemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
   
    public void changeFill(float pct)
    {
        Debug.Log(pct);
        foregroundImage.fillAmount = pct;
    }
}
