using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private bool canMove = false;
    public bool isLarge;
    private float speed;
    public float moveInterval;
    public int currentHP;
    public int smallSlimeMaxHP;
    public int largeSlimeMaxHp;
    public int bubbleDamage;

    public float smallMinSpeed;
    public float smallMaxSpeed;
    public float largeMinSpeed;
    public float largeMaxSpeed;
    private bool canPredict;

    private GameObject playerGO;
    private Rigidbody2D playerRb;
    private Vector2 playerPos;
    private Vector2 predictedPos;

    public GameObject smallSlimePrefab;
    public GameObject largeSlimePrefab;

    public AudioSource smallHit;
    public AudioSource largeHit;

    private bool isTouching;

    public void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerRb = playerGO.GetComponent<Rigidbody2D>();
        SizeCheck();
        RandomizeMovement();
        StartCoroutine(MoveTimer());
    }

    public void Update()
    {
        DeathCheck();
    }

    public void FixedUpdate()
    {
        if (canMove == true && isLarge == false && isTouching == false)
        {
            Move();
        }

        if (isLarge == true && isTouching == false)
        {
            UpdatePos();
            Move();
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        isTouching = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            Destroy(other.gameObject);
            if (isLarge == true)
            {
                largeHit.Play();
            }
            else
            {
                smallHit.Play();
            }
            currentHP -= bubbleDamage;
        }
    }

    public void RandomizeMovement()
    {
        if (isLarge == false)
        {
            speed = Random.Range(smallMinSpeed, smallMaxSpeed);
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), largeSlimePrefab.GetComponent<BoxCollider2D>());
        }
        else
        {
            speed = Random.Range(largeMinSpeed, largeMaxSpeed);
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), smallSlimePrefab.GetComponent<BoxCollider2D>());
        }

        int predictChoice = Random.Range(1, 3);

        if (predictChoice == 1)
        {
            canPredict = false;
        }
        else
        {
            canPredict = true;
        }
    }

    public void Move()
    {
        if (canPredict == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, predictedPos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        }
    }

    public void UpdatePos()
    {
        playerPos = playerGO.transform.position;
        predictedPos = new Vector2(playerPos.x, playerPos.y) + playerRb.velocity * Time.deltaTime;
    }

    IEnumerator MoveTimer()
    {
        while (true)
        {
            canMove = !canMove;
            UpdatePos();
            yield return new WaitForSeconds(moveInterval);
        }
    }

    public void SizeCheck()
    {
        if (isLarge == true)
        {
            currentHP = largeSlimeMaxHp;
        }
        else
        {
            currentHP = smallSlimeMaxHP;
        }
    }

    public void DeathCheck()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
