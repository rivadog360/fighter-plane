using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject JD_enemy2;
    public GameObject Troy_enemy3;
    public GameObject cloudPrefab;
    public GameObject powerupPrefab;
    public GameObject gameOverMenu;
    public GameObject audioPlayer;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI livesText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    private bool gameOver; 

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        scoreText.text = "Score: 0";
        gameOver = false;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("CreateEnemy2", 1, 5);
        InvokeRepeating("CreateEnemy3", 5, 7);
        StartCoroutine(SpawnPowerup());
        powerupText.text = "No powerups Yet!";
    }

    // Update is called once per frame
    void Update()
    {
      if(gameOver && Input.GetKeyDown(KeyCode.R))
      {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }  
    }

    void CreateEnemy()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0)); 
    }

    void CreateEnemy2()
    {
        Instantiate(JD_enemy2, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateEnemy3()
    {
        Instantiate(Troy_enemy3, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }
    void CreatePowerup()
    {
         Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-verticalScreenSize * .8f, 0f), 0), Quaternion.identity); 
    }
     IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5); 
        yield return new WaitForSeconds(spawnTime); 
        CreatePowerup();
        StartCoroutine(SpawnPowerup()); 
    }
   public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1: 
                powerupText.text = "Speed!";
                break; 
            case 2:
                powerupText.text = "Double Weapon!";
                break; 
            case 3:
                powerupText.text = "Triple Weapon!";
                break; 
            case 4: 
                powerupText.text = "Shield!";
                break; 
            default: 
                powerupText.text = "No powerups yet!";
                break;
        }
    }
    public void PlaySound(int whichSound)
    {
        switch(whichSound)
        {
            case 1: 
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound); 
                break; 
            case 2: 
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break; 
        }
    }
    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        gameOver = true;
    }
    public void AddScore(int amount)
{
    score += amount;
    scoreText.text = "Score: " + score;
}
}