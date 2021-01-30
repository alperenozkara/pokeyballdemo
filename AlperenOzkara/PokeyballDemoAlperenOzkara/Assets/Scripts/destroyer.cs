using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    private generate_level gl;
    // Start is called before the first frame update
    void Start()
    {
        gl = GameObject.FindGameObjectWithTag("levelgenerator").GetComponent<generate_level>();
    }

    // Update is called once per frame
    void Update()
    {
        //IF SPAWN HEIGHTER THEN TOWER DESTROY IT 
        if (transform.position.y >= gl.maxTowerLenght)
        {
            Destroy(gameObject);
        }
    }
}
