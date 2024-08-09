using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public float health = 100;
    public float bounceback;
    public bool bouncing = false;
    private Animator anim;
    private Vector3 lookDirection;
    private Rigidbody2D rb;
    public float Timer;
    public bool isSpawning;
    public bool isSpitting;
    public GameObject spitBall;
    public GameObject[] enemyPrefab;
    public int spawnAmount;
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
                rb.velocity = (-lookDirection * bounceback);
;            }
            
        } else {
            rb.velocity = new Vector2(0,0);
        }
        print(System.Math.Round(Timer, 0, System.MidpointRounding.AwayFromZero));
        print(System.Math.Round(Timer, 0, System.MidpointRounding.AwayFromZero) % 20);
        if (System.Math.Round(Timer, 0, System.MidpointRounding.AwayFromZero) % 20 == 0){
            var rnd = new System.Random();
            int randbool = rnd.Next(0, 2);
            // 0 --> Spitting
            // 1 --> Summoning
            if (randbool == 0){
                isSpitting = true;
                Vector3 lookDirection = (player.transform.position - transform.position).normalized;
                Vector3 rotation = player.transform.position - transform.position;
                float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                Instantiate(spitBall, transform.position, Quaternion.Euler(0, 0, rot));
            } else {
                isSpawning = true;
                for (int i = 0; i < spawnAmount; i++){
                    Vector3 randomSpawnPos = new Vector3 (rnd.Next(54, 68), rnd.Next(-2, 3), 0);
                    while (Physics2D.OverlapPoint(randomSpawnPos, 7) | Physics2D.OverlapPoint(randomSpawnPos, 14)){
                        randomSpawnPos = new Vector3 (rnd.Next(54, 68), rnd.Next(-2, 3), 0);
                    }
                    Instantiate(enemyPrefab[rnd.Next(0, 2)], randomSpawnPos, transform.rotation);
                }
            }

        }
        

        if (health <= 0){
            Destroy(gameObject);
        }
        anim.SetFloat("xSpeed", lookDirection.x * speed);
        anim.SetFloat("ySpeed", lookDirection.y * speed);
        anim.SetBool("isSpawning", isSpawning);
        anim.SetBool("isSpitting", isSpitting);

        Timer += Time.deltaTime;
    }
}
