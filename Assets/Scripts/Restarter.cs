using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restarter : MonoBehaviour
{
    public Rigidbody2D projectile;
    public float resetSpeed = 0.5f;


    SpringJoint2D spring;
    float resetSpeedSQR;

    // Start is called before the first frame update
    void Start()
    {
        spring = projectile.GetComponent<SpringJoint2D>();
        resetSpeedSQR = resetSpeed * resetSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (spring == null && projectile.velocity.sqrMagnitude <= resetSpeedSQR)
        {
            Reset();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() == projectile)
        {
            Reset();
        }
    }

    private void Reset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
