using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject player;

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
                    Destroy(gameObject);
                   
                    Debug.Log(other.name);
                    break;
                }
                if (other.CompareTag("Blocking"))
                {
                    Destroy(gameObject);
                    Debug.Log(other.name);
                    break;
                }
            }
        }
        transform.position = newPosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals("Zombie"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
