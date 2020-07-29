using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform trans;
    private Vector2 pos;
    public float speed;

    public bool touchingEnemy;
    public bool touchingTrash;
    public float currentToxicity = 0;
    public float toxicityPerSecond;
    public float maxToxicity = 100;

    public Animator playerAnim;

    public AudioSource walkAud;

    public GameController gameControllScript;

    public GameObject exitGO;
    public int currentChute = 1;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
        gameControllScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        currentToxicity = PlayerPrefs.GetFloat("Toxicity");
        currentChute = PlayerPrefs.GetInt("Chute");
    }

    public void Update()
    {
        pos = trans.position;
        playerAnim.SetFloat("Velocity", rb.velocity.magnitude);
        DamageCheck();
        WalkingCheck();
        DeathCheck();
        EndCheck();

        PlayerPrefs.SetFloat("Toxicity", currentToxicity);
        PlayerPrefs.SetInt("Chute", currentChute);
    }

    public void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            touchingEnemy = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            touchingEnemy = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            touchingTrash = true;
        }

        if (other.gameObject.CompareTag("Exit"))
        {
            Application.LoadLevel("Main");
            currentChute++;
        }

        if (other.gameObject.CompareTag("Hatch"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Chute", 1);
            Application.LoadLevel("Main");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            touchingTrash = false;
        }
    }

    private void DamageCheck()
    {
        if (touchingEnemy == true || touchingTrash == true)
        {
            currentToxicity += toxicityPerSecond * Time.deltaTime;
        }
    }

    private void DeathCheck()
    {
        if (currentToxicity >= maxToxicity)
        {
            currentToxicity = maxToxicity;
            Application.LoadLevel("Start");
            Destroy(gameObject);
        }
    }

    private void WalkingCheck()
    {
        if (rb.velocity.magnitude >= 10)
        {
            walkAud.UnPause();
        }
        else
        {
            walkAud.Pause();
        }
    }

    private void EndCheck()
    {
        if (gameControllScript.hasEnded == true)
        {
            exitGO.SetActive(true);
        }
    }
}
