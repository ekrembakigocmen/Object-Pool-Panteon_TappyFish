using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class Fish : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField]
    private float speed;

    private int angle;
    private int maxAngle = 22;
    private int minAngle = -20;

    public Score score;

    public GameManager gameManager;

    public Sprite fishDied;
    private SpriteRenderer sp;
    public Animator anim;

    bool touchedGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        /*Bu satirla basta 9f yukari, sonra asagi dogru gidiyor:  
         rb.velocity = new Vector2 (rb.velocity.x, 9f);   */

        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FishSwim();
    }


  
    void FixedUpdate()
    {
        FishRotate();
    }


    void FishSwim()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.gameOver)
        {
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }

    void FishRotate()
    {
        if (rb.velocity.y > 0)
        {
            //Yukari giderken baktigi yonun acisini artiriyor
            if (angle <= maxAngle)
            {
                angle += 4;
            }
        }
        //Hemen asagi bakmasin -0.1,.2..-1.1'e kadar bekleyip oyle acisini asagi  alsin diye bunu yapiyoruz
        else if (rb.velocity.y < -1.1)
        {
            if (angle > minAngle)
            {
                angle -= 2;
            }
        }

        if (touchedGround == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            score.Scored();
        }
        else if (collision.CompareTag("Column"))
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.gameOver != true)
            {
                gameManager.GameOver();
                GameOver();
            }
        }
        else
        {
            //gameOver fish;
            gameManager.GameOver();
            GameOver();       
        }
    }

    void GameOver()
    {
        touchedGround = true;
        transform.rotation = Quaternion.Euler(0, 0, -90);
        sp.sprite = fishDied;
        anim.enabled = false;
    }
}