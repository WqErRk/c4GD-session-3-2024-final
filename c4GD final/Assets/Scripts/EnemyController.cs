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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
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
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            if (!bouncing){
                transform.Translate(lookDirection * speed * Time.deltaTime);
            } else if (bouncing){
                transform.Translate(-lookDirection * speed * bounceback * Time.deltaTime)
;            }
            
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
        
    }
}
