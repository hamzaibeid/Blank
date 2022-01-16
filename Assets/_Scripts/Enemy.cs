﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   //Variables
    public float health;
     


   //Methods
    public void Update()
    {
        if(health <=0 )
        {
            Die();
        }

    }

    public void Die()
    {
        Debug.Log("Enemy" +this.gameObject.name + "has died!");
        Destroy(this.gameObject);
    }
}
