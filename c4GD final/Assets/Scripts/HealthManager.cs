using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //handle enemy collision
    void OnCollisionEnter2D(Collision2D coll){
        TakeDamage(15);
        if(coll.gameObject.CompareTag("Enemy") && !isInvincible){
            Rigidbody2D enemyRigidbody = coll.gameObject.GetComponent<Rigidbody2D>();
            VFXManager.instance.ShakeCam(0.3f, 0.7f);
        }
    }

    //handle spike and projectile collision
    void OnTriggerEnter2D(Collider2D other){
        TakeDamage(15);
        if(other.gameObject.CompareTag("Projectile") && !isInvincible){
            VFXManager.instance.ShakeCam(0.3f, 0.7f);
            }
        }
    public void TakeDamage(float damage){
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }
    public void Heal(float healingAmount){
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0 , 100);

        healthBar.fillAmount = healthAmount / 100f;

    }
}
