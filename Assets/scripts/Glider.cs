using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{

    public bool goingUp;
    public bool isEnemy2;
    public bool isEnemy3;
    public float speed;

    private int direction = 1; // 1 is right, -1 is left
    public float minTime = 1f; //minimum random timer
    public float maxTime = 3f; //max random timer
    private float timer;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetRandomTimer(); //start the random timer for left-right movement
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy3)
        {

            transform.Translate(Vector3.up * (speed * 1 / 8) * Time.deltaTime);
            transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
            if (transform.position.x >= 9.5f || transform.position.x <= -9.5f)
                sideToSide();


        }
        else
        {
            if (goingUp)
            {
                if (isEnemy2) //check if this is the second enemy, then uses enemy2's movement 
                {
                    timer -= Time.deltaTime; //tick down the timer
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                    transform.Translate(Vector3.left * direction * speed * Time.deltaTime); //starts moving left
                    if (timer <= 0)
                    {
                        sideToSide(); //when timer runs out, flip direction
                    }
                }
                else
                {
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                }
            }
            else if (goingUp == false)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
    if (transform.position.y >= gameManager.verticalScreenSize * 1.25f || transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
    {
      Destroy(this.gameObject);
    }
    }

    void sideToSide()
    {
        //flips the direction (should only apply to enemy2 or 3)
        direction *= -1;
        SetRandomTimer();
    }

    void SetRandomTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }
}