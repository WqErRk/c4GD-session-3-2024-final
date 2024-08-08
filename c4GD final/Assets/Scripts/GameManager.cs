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
    public TMP_Text scoreText;
    public GameObject loseScreen;
    public GameObject startScreen;
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
     public void GameOver(){
        isGameActive = false;
        loseScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
