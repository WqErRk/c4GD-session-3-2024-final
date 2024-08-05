using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 5){
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(lookDirection * speed * Time.deltaTime);
        }
        
    }
}
