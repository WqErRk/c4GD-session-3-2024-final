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
    public bool dashCooling = false;
    public GameObject Sword;

    public Camera mainCam;
    //debug variables
    public bool swordActive;

    // Start is called before the first frame update
    void Start()
    {
        Sword.SetActive(false);
        speed = normalSpeed;
        swordActive = false;
    }
    //Invicnibility timer for dash
    IEnumerator dashTimer(){
        isInvincible = true;
        speed = dashSpeed;
        yield return new WaitForSeconds(0.25f);
        speed = normalSpeed;
        isInvincible = false;
        StartCoroutine(dashCooldown());    
    }

    IEnumerator dashCooldown(){
        dashCooling = true;
        yield return new WaitForSeconds(1);
        dashCooling = false;
    }

    //handle enemy collision
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.CompareTag("Enemy") && !isInvincible){
            health -= 15;
            Rigidbody2D enemyRigidbody = coll.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    //handle spike and projectile collision
    void OnTriggerEnter2D(Collider2D other){
        if((other.gameObject.CompareTag("Spike") | other.gameObject.CompareTag("Projectile")) && !isInvincible){
            health -= 15;
            }
        }

    IEnumerator swordTimer(){
        Sword.SetActive(true);
        yield return new WaitForSeconds(FindObjectOfType<SwordScript>().weapontimer);
        Sword.SetActive(false);
    }
        

    // Update is called once per frame
    void Update(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.position += (Vector3.right * horizontalInput * Time.deltaTime * speed);
        verticalInput = Input.GetAxisRaw("Vertical");
        transform.position += (Vector3.up * verticalInput * Time.deltaTime * speed);
        
        Vector3 playerpos = transform.position;
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); 

        Vector3 direction = mousePos - playerpos;
        Vector3 rotation = playerpos - mousePos;
        
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        Vector3 addSpace = new Vector3(playerpos.x, playerpos.y + 1, playerpos.z + 1);
        transform.rotation = Quaternion.Euler(0, 0, rot);

        if (Input.GetKeyDown(KeyCode.Space) && !dashCooling){StartCoroutine(dashTimer());}
        if (Input.GetKeyDown(KeyCode.F) && !swordActive){ 
            StartCoroutine(swordTimer());
        }
            
    }
}

