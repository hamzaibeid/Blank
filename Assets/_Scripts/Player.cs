using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public float speed = 5f;
    public Rigidbody2D rb;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Reset moveDelta
        moveDelta = new Vector3(x, y, 0).normalized;

        // Swap sprite direction,wether your going righ or left
        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }
        else if(moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-4,4,1);
        }
        Move();

        //make sure that we can move in this direction, by casting a box there first,if the box returns null , we're free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Actor","Blocking"));
        if(hit.collider == null)
        {
            //make the sprite move
            transform.Translate(0,moveDelta.y * Time.deltaTime,0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //make the sprite move
            transform.Translate(moveDelta.x * Time.deltaTime, 0,0);
        }

    }
    void Move()
    {
        rb.velocity = new Vector2(moveDelta.x * speed, moveDelta.y * speed);
    }

}
