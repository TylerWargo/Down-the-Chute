using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int currentHP;
    public int bubbleDamage;

    public void Update()
    {
        DamageCheck();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            currentHP -= bubbleDamage;
            Destroy(other.gameObject);
        }
    }

    public void DamageCheck()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
