using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform firePoint;

    public Vector2 targetVect;

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        targetVect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(bubblePrefab, firePoint.position, firePoint.rotation);
    }
}
