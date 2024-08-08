using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public static GameManager instance;
    public bool isGameActive = true;
    public TMP_Text Health;
    public float health = 100f;
    public GameObject loseScreen;
    public GameObject startScreen;
    public bool isInvincible = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isGameActive = false;
        startScreen.SetActive(true);

    }
     public void StartGame(){
        instance = this;
        isGameActive = true;
        startScreen.SetActive(false);
        loseScreen.SetActive(false);
        print("afsadfasdfasfs");
    }
     public void UpdateHealth(int health){
    
    }
    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
     public void GameOver(){
        isGameActive = false;
        loseScreen.SetActive(true);
    }
    void OnCollisionEnter2D(Collision2D coll){
        print(isInvincible);
        if(coll.gameObject.CompareTag("Enemy") && !isInvincible){
            health -= 15;
            Rigidbody2D enemyRigidbody = coll.gameObject.GetComponent<Rigidbody2D>();
            print("coliided");
            VFXManager.instance.ShakeCam(0.3f, 0.7f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
