using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public float fireRate;
    public float startDelay;
    public GameObject projectiles;
    private float spawnTimer = 0;
    public float health = 100;
    public GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    IEnumerator SpawnProjectile(){
        yield return new WaitForSeconds(fireRate);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (System.Math.Abs(Vector3.Distance(transform.position, player.transform.position)) < 4){
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(-lookDirection * speed * Time.deltaTime);
        }
        if (spawnTimer < fireRate){
            spawnTimer += Time.deltaTime; 
        } else {
            spawnTimer = 0;
            //Call Spawn Function 
            if (Vector3.Distance(player.transform.position, transform.position) < 10){
                StartCoroutine(SpawnProjectile());
                Instantiate(projectiles, transform.position, transform.rotation);
            }
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
