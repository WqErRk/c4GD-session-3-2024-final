using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float normalSpeed;
    public float speed;
    public bool isInvincible = false;
    public float dashSpeed = 10f;
    public float health = 100f;

    //debug variables

    // Start is called before the first frame update
    void Start()
    {
        speed = normalSpeed;
    }
    //Invicnibility timer for dash
    IEnumerator dashTimer(){
        isInvincible = false;
        speed = dashSpeed;
        yield return new WaitForSeconds(0.5f);
        speed = normalSpeed;
        isInvincible = true;    
    }

    //handle enemy collision
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.CompareTag("Enemy") | coll.gameObject.CompareTag("Projectile")){
            health -= 15;
        }
    }

    //handle spike collision
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Spike")){
            health -= 15;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        
        if (Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(dashTimer());
            
        }
    }
}
