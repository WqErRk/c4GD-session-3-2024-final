using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject Gates;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.CompareTag("Player")){
            Gates.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
