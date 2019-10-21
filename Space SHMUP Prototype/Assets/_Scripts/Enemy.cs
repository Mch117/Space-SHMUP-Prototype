using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 20f; // the speed in m/s

    public float fireRate = 0.3f; // seconds/shot

    public float health = 10;

    public int scoreValue = 2; // Point earned for destroying

    private BoundsCheck bndCheck;
    
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    //This is a Property: A method that acts like a field

    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();

        if (bndCheck != null && bndCheck.offDown)
        {
            //We're off the bottom, so destroy this GameObject
            Destroy(gameObject);
            //Score();
                
                
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGo = coll.gameObject;

        if (otherGo.tag == "ProjectileHero")
        {
            ScoreScript.S.UpdateScore();
            Destroy(otherGo);
            Destroy(gameObject);
            
            //Debug.Log("Score increase");

        }
        else
        {
            print("Enemy hit by non-ProjectileHero: "+otherGo.name);
        }
    }

   
}
