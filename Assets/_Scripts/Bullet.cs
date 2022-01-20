using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject player;
    private GameObject triggerringEnemy;
    public float damage;

    
   

    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x,transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;
        RaycastHit2D[] hits =  Physics2D.LinecastAll(currentPosition, newPosition);
        Debug.DrawLine(currentPosition, newPosition,Color.red);
        foreach ( RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if (other != player)
            {
                if (other.CompareTag("Enemy"))
                {
                    triggerringEnemy = other.gameObject;
                    triggerringEnemy.GetComponent<Enemy>().health -= damage;
                    Destroy(gameObject);
                    if(triggerringEnemy.GetComponent<Enemy>().health <= 0){
                    triggerringEnemy.GetComponent<Enemy>().Die();
                    }
                    break;
                }
                if (other.CompareTag("Blocking"))
                {
                    Destroy(gameObject);
                    break;
                }
            }
        }
        transform.position = newPosition;
    }

  
}
