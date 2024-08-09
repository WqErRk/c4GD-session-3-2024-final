using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitBallController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private Vector3 lookDirection;
    private Animator anim;
    private float Timer;
    public bool isSplating;
    public bool isLanded;
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        Vector3 rotation = player.transform.position - transform.position;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        anim = GetComponent<Animator>();
        float startY = transform.position.y;
        
    }

    void OnCollisionEnter2D(Collision2D coll){
        

    }
    
    // Update is called once per frame
    void Update(){
        
        if (System.Math.Round(Timer, 0, System.MidpointRounding.AwayFromZero) == 2){
            if (System.Math.Round(Timer, 2, System.MidpointRounding.AwayFromZero) != 2.25){
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                transform.Translate(Vector2.down * (speed / 3) * Time.deltaTime);
                isSplating = true;
            } else if (System.Math.Round(Timer, 2, System.MidpointRounding.AwayFromZero) == 2.32){
                isLanded = true;

            }
            
        } else {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        anim.SetBool("isSplating", isSplating);
        anim.SetBool("isLanded", isLanded);
        Timer += Time.deltaTime;
    }
}