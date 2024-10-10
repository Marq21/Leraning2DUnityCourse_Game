using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
