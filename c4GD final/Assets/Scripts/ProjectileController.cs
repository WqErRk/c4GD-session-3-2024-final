using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private Vector3 lookDirection;
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        
    }

    void OnTriggerEnter2D(Collider2D coll){
        if (!coll.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update(){
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        transform.Translate(lookDirection * speed * Time.deltaTime);
    }
}
