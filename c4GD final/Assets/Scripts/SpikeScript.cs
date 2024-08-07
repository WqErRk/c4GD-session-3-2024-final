using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private Animator anim;
    public bool isCollided = false;
    private int damage = 30;
    public string RangedEnemyController = "RangedEnemyController";
    public string EnemyController = "EnemyController";
    public string PlayerController = "PlayerController";
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.CompareTag("Player") | coll.gameObject.CompareTag("Enemy")){
            isCollided = true;
            StartCoroutine(spikeDamageTimer(coll));
        }
        
    }
    void OnTriggerExit2D(Collider2D coll){
        if(coll.gameObject.CompareTag("Player") | coll.gameObject.CompareTag("Enemy")){
            isCollided = false;
        }
    }

    IEnumerator spikeDamageTimer(Collider2D other){
        yield return new WaitForSeconds(0.5f);
        if (isCollided){
            EnemyController enemyScript = other.gameObject.GetComponent<EnemyController>();
            RangedEnemyController rangedEnemyScript = other.gameObject.GetComponent<RangedEnemyController>();
            PlayerController playerScript = other.gameObject.GetComponent<PlayerController>();
            if (isCollided){
                if (enemyScript) {
                    enemyScript.health -= damage;
                } else if (rangedEnemyScript){
                    rangedEnemyScript.health -= damage;
                } else if (playerScript){
                    playerScript.health -= damage;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isCollided", isCollided);
    }
}
