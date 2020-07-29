using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private WeaponController weaponScript;
    private Vector2 targetPos;

    public float lifetime;
    public float speed;

    public void Start()
    {
        weaponScript = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>();
        targetPos = weaponScript.targetVect;
        Destroy(gameObject, lifetime);
    }

    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)
        {
            Destroy(gameObject);
        }
    }
}
