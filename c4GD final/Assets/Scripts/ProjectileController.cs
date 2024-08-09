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
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        Vector3 rotation = player.transform.position - transform.position;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        
    }

    void OnTriggerEnter2D(Collider2D coll){
        if (!coll.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
