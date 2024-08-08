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
    public bool swordActive = false;
    public GameObject rotationPoint;
    private Rigidbody2D rb;
    public GameObject Gates;
    public GameObject GateTriggers;
    public GameObject SpawnTriggers;
    public GameObject gameOverScreen;
    public GameObject vfxManager;

    public Camera mainCam;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Sword.SetActive(false);
        speed = normalSpeed;
        swordActive = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameOverScreen.SetActive(false);
    }
    //Invicnibility timer for dash
    IEnumerator dashTimer(){
        isInvincible = true;
        speed = dashSpeed;
        yield return new WaitForSeconds(0.25f);
        speed = normalSpeed;
        yield return new WaitForSeconds(1f);
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
            print("coliided");
            VFXManager.instance.ShakeCam(0.3f, 0.7f);
        }
    }

    //handle spike and projectile collision
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Projectile") && !isInvincible){
            health -= 15;
        } else if (other.gameObject.CompareTag("potion")){
            health = 100;
        }
        }

    IEnumerator swordTimer(){
        swordActive = true;
        Sword.SetActive(true);
        yield return new WaitForSeconds(FindObjectOfType<SwordScript>().weapontimer);
        Sword.SetActive(false);
        swordActive = false;
    }
        

    // Update is called once per frame
    void Update(){
        if (health > 0){
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
        
            Vector3 playerpos = transform.position;
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); 

            Vector3 direction = mousePos - playerpos;
            Vector3 rotation = playerpos - mousePos;
        
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            Vector3 addSpace = new Vector3(playerpos.x, playerpos.y + 1, playerpos.z + 1);
            rotationPoint.transform.rotation = Quaternion.Euler(0, 0, rot);

            if (Input.GetKeyDown(KeyCode.Space) && !dashCooling){StartCoroutine(dashTimer());}
            if (Input.GetKeyDown(KeyCode.Mouse0) && !swordActive){StartCoroutine(swordTimer());}
        } else {
            VFXManager vfxscript = vfxManager.GetComponent<VFXManager>();
            vfxscript.VFXOn = false;
            gameOverScreen.SetActive(true);
            rb.velocity = Vector2.zero;
        }
        
        anim.SetFloat("xSpeed", Mathf.Abs(horizontalInput * speed));
        anim.SetFloat("ySpeed", verticalInput * speed);
        anim.SetBool("swordActive", swordActive);
        anim.SetBool("isInvincible", isInvincible);
        mainCam.gameObject.transform.position = new Vector3(transform.position.x, mainCam.gameObject.transform.position.y, mainCam.gameObject.transform.position.z);
    }
}

