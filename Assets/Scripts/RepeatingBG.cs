using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{
    [SerializeField] private float verticalSize;
    private Vector2 offSet;
    void Update()
    {
        if(transform.position.y < -verticalSize)
        {
            repeatBackground();
        }
    }

    private void repeatBackground()
    {
        offSet = new Vector2(0, verticalSize * 2);
        transform.position = (Vector2)transform.position + offSet;
    }
}
