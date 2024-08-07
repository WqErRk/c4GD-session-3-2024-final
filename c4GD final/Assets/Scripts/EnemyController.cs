using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public float health = 100;
    public float bounceback;
    public bool bouncing = false;
    public GameObject key;
    private Animator anim;
    private Vector3 lookDirection;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator bounceTimer(float seconds){
        bouncing = true;
        yield return new WaitForSeconds(seconds);
        bouncing = false;
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.CompareTag("Player")){
            StartCoroutine(bounceTimer(0.1f));
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Sword")){
            StartCoroutine(bounceTimer(0.25f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 5){
            lookDirection = (player.transform.position - transform.position).normalized;
            if (!bouncing){
                rb.velocity = (lookDirection * speed);
            } else if (bouncing){
                rb.velocity = (-lookDirection * speed);
;            }
            
        } else {
            rb.velocity = new Vector2(0,0);
        }

        if (health <= 0){
            var aliveEnemies = FindObjectsOfType<EnemyController>();
            var aliveRangedEnemies = FindObjectsOfType<RangedEnemyController>();
            int totalEnemies = aliveEnemies.Length + aliveRangedEnemies.Length;
            if (totalEnemies == 1){
                Instantiate(key, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        anim.SetFloat("xSpeed", lookDirection.x * speed);
        anim.SetFloat("ySpeed", lookDirection.y * speed);
        
    }
}
