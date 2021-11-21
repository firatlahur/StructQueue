using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float movementSpeed;

    void Update()
    {
        transform.Translate(0f,0f,movementSpeed * Time.deltaTime);
    }
}
