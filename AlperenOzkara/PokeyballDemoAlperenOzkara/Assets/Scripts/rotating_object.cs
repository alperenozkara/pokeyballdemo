using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotating_object : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 100f * Time.deltaTime);//ROTATE OBJECT :)
    }
}
