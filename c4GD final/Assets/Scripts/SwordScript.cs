using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public int damage;
    public int knockback;
    public string RangedEnemyController = "RangedEnemyController";
    public string EnemyController = "EnemyController";
    public float weapontimer;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")){
            EnemyController enemyScript = other.GetComponent<EnemyController>();
            RangedEnemyController rangedEnemyScript = other.GetComponent<RangedEnemyController>();
            if (enemyScript) {
                enemyScript.health -= damage;
            } else if (rangedEnemyScript){
                rangedEnemyScript.health -= damage;
            }
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();
       }
    }

   

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        transform.position += (playerPos - transform.position);
    }
}
