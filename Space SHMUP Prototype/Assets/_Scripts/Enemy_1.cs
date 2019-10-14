﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
   public float speed;
   private Transform target;

   private void Start()
   {
      target = GameObject.FindGameObjectWithTag("Hero").GetComponent<Transform>();
   }

   private void Update()
   {
      if (Vector2.Distance(transform.position, target.position) > 3)
      {
         transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
      }
   }
   
   private void OnCollisionEnter(Collision coll)
   {
      GameObject otherGo = coll.gameObject;

      if (otherGo.tag == "ProjectileHero")
      {
         Destroy(otherGo);
         Destroy(gameObject);
      }
      else
      {
         print("Enemy hit by non-ProjectileHero: "+otherGo.name);
      }
   }
      
}
