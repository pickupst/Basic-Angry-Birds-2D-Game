using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public Sprite demageSprite;
    public int hitPoints = 2;
    public float damageImpactSpeed = 0.3f;

    SpriteRenderer SpriteRenderer;
    int currentHitPoints;
    float damageImpactSpeedSQR = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        currentHitPoints = hitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Plank")
        {
            currentHitPoints--;
        }

        if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSQR)
        {
            SpriteRenderer.sprite = demageSprite;
            return;
        }

        if (collision.gameObject.tag == "Player" || currentHitPoints <= 0)
        {
            Kill();
        }
        Debug.Log(currentHitPoints);
        
    }

    private void Kill()
    {

        SpriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;

        if (!GetComponent<ParticleSystem>().isPlaying)
        {
            GetComponent<ParticleSystem>().Play();
        }

    }
}
