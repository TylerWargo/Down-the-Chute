using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int enemiesRemaining;
    public int trashRemaining = 1;
    private int totalRemaining;

    public PlayerController playerScript;
    public Text toxText;
    public Text chuteText;

    public GameObject orangeLights;
    public GameObject greenLights;

    public Animator signAnim;

    public GameObject door;
    public Transform doorOpenLoc;
    public float doorOpenSpeed;

    public bool hasEnded = false;

    private void Awake()
    {
        InvokeRepeating("EndCheck", 0.5f, 0.5f);
    }

    public void Update()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        FindRemaining();
        UpdateText();
    }

    public void FindRemaining()
    {
        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
        trashRemaining = GameObject.FindGameObjectsWithTag("Trash").Length;
        totalRemaining = enemiesRemaining + trashRemaining;
    }

    public void EndCheck()
    {
        if (totalRemaining <= 0)
        {
            signAnim.SetBool("levelCleared", true);
            hasEnded = true;
            ClearLights();
            MoveDoor();
        }
    }

    public void UpdateText()
    {
        toxText.text = "(Toxicity: " + playerScript.currentToxicity.ToString("F2") + "/" + playerScript.maxToxicity + ")";
        chuteText.text = "[Chamber " + playerScript.currentChute + "]";
    }

    public void ClearLights()
    {
        orangeLights.SetActive(false);
        greenLights.SetActive(true);
    }

    public void MoveDoor()
    {
        door.transform.position = Vector2.MoveTowards(door.transform.position, doorOpenLoc.position, doorOpenSpeed * Time.deltaTime);
    }
}