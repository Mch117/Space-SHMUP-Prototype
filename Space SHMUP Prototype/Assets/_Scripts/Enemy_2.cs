using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{

    public float speed;

    public float stoppingDistance;

    public float retreatDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;
    
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Transform player;
    public int scoreValue = 10; // Point earned for destroying
   
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hero").transform;

        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
        }
        
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }


        if (timeBtwShots <= 0)
        {
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            TempFire();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
    }
    
    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity =  Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        
    }
    
    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGo = coll.gameObject;

        if (otherGo.tag == "ProjectileHero")
        {
            ScoreScript.score += scoreValue;
            Destroy(otherGo);
            Destroy(gameObject);
            
            Debug.Log("Hit");
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: "+otherGo.name);
        }
    }
}
