using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
   //Variables
    public float health;
    public float damage;
    public float pointsToGive;
     public GameObject player;
     public HealthBar healthBar ;

private void Reset(){
    GetComponent<BoxCollider2D>().isTrigger=true;
}
private void OnTriggerEnter2D(Collider2D collison){
    if(collison.gameObject.tag=="player_0"){
        player.GetComponent<Player>().health-=damage;
        healthBar.SetHealth( player.GetComponent<Player>().health);
        if(player.GetComponent<Player>().health<=0){
            Destroy(player.gameObject);
            
        }
    }
}


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

