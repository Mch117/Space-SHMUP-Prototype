using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero      S; //Singleton             //a

    [Header("Set in Inspector")]

    //These fields control the movement of the ship

    public float speed = 30;

    public float rollMult = -45;

    public float pitchMult = 30;

    public float gameRestartDelay = 2f;

    public GameObject projectilePrefab;
    public float projectileSpeed = 40;


    [Header("Set Dynamically")] 
    
    [SerializeField]
    
    private float _shieldLevel = 1;
    
    //The variable holds a reference to the last triggering GameObject
    private GameObject lastTriggerGo = null;

    private void Awake()
    {
        if (S == null)
        {
            S = this; //Set the Singleton              //a
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pull information from the Input class
        float xAxis = Input.GetAxis("Horizontal");           //b
        float yAxis = Input.GetAxis("Vertical");            //b
        
        
        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        
        // Rotate the ship to make it feel more dynamic        //c
        transform.rotation = Quaternion.Euler(yAxis*pitchMult, xAxis*rollMult, 0);
        
        //Allow the ship to fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }
    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: "+go.name);
        
        //Make sure it's not the same triggering go as last time
        if (go == lastTriggerGo)
        {
            return;
        }

        lastTriggerGo = go;

        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else
        {
            print("Triggered by non-Enemy: "+go.name);
        }
    }

    public float shieldLevel
    {
        get { return (_shieldLevel); }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            //If the shield is going to be set to less than zero

            if (value < 0)
            {
                Destroy(this.gameObject);
                
                //Tell Main.s to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
