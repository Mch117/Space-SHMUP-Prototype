﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a GameObject on screen
/// Note that this ONLY works for an orthographic Main Camera at [0, 0,0].
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    
    [Header("Set in Inspector")] 
    public float radius = 1f;

    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    
    public bool isOnScreen = true;
    
    public float camWidth;

    public float camHeight;

    [HideInInspector] 
    public bool offRight, offLeft, offUp, offDown;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
    
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;

        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
            //isOnScreen = false;

        }

        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
            //isOnScreen = false;
        }

        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
            //isOnScreen = false;
        }

        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
            // isOnScreen = false;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }

        //transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth *2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
