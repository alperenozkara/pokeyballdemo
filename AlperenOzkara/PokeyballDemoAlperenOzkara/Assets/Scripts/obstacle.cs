using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    private generate_level gl;
    void Start()
    {
        gl = GameObject.FindGameObjectWithTag("levelgenerator").GetComponent<generate_level>();
    }

   
    void Update()
    {
        //IF OBSTACLE SPAWN HEIGHTER THEN TOWER DESTROY IT !
        if (transform.position.y >= gl.maxTowerLenght) {
            Destroy(gameObject);
        }
    }
    void OnTriggerStay(Collider obje)
    {
        //IF OBSTACLE SPAWN IN ANOTHER OBSTACLE CHANGE POS
        if (obje.gameObject.tag == "dangersurface" || obje.gameObject.tag == "metalsurface")
        {
            transform.position = new Vector3(0, Random.Range(5,gl.maxTowerLenght),0);
        }
    }
}
