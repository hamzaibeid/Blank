using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   //Variables
    public float health;
    public float pointsToGive;
     public GameObject player;


   //Methods

   public void Start()
   {
       player = GameObject.FindWithTag("player_0");
   }
   
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
        player.GetComponent<Player>().points += pointsToGive;
    }
}
