using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public List<GameObject> parts;
    public bool d;
    void Start()
    {
        //GET ALL CHILDOBJECTS TO LIST
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            parts.Add(transform.GetChild(i).transform.gameObject);
        }
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider coll) {
        
        if (coll.gameObject.tag == "ball")//IF BALL HIT THIS OBJECT
        {
            d = true;
            for (int i = 0; i < parts.Count; i++)
            {
                
                parts[i].GetComponent<Rigidbody>().isKinematic = false;
                parts[i].GetComponent<Rigidbody>().useGravity = true;
                parts[i].GetComponent<Rigidbody>().AddExplosionForce(200f, gameObject.transform.position, 500f, 30f); //DO IT EXPLOSIONFORCE
                
                Invoke("DeleteAll", 0.5f);//DELETE ALL 

            }
        }
    }

    void DeleteAll() {
        Destroy(gameObject);
    }
}
