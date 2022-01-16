using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [Header("Input settings: ")]
    public int playerID;
    Player player;

    [Space]
    [Header ("Charachter attributes:")]
    public float Movement_Base_Speed = 5f;
    public float CrossHair_Distance = 1.0f;
    public float Aiming_Base_Penalty;
    public float Bullit_base_speed = 1.0f;
   

    [Space]
    [Header("Charachter statistics")]
    public Vector3 movementDirection;
    public float movementspeed;
    public bool endOfAiming;
    public bool isAiming;
    public float points;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public GameObject crossHair;
    public Animator anim;

    [Space]
    [Header("Prefabs")]
    public GameObject bulletPrefab;

  
   void Start(){
       Cursor.visible=false;
   }
   
     void Update()
    {
        
        ProccesInputs();
        Move();
        Animate();
        Aim();
        Shoot();
         
    }

    private void ProccesInputs()
    {
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0.0f);
        movementspeed = Mathf.Clamp(movementDirection.magnitude,0.0f,1.0f);
        movementDirection.Normalize();
        endOfAiming = Input.GetButtonUp("Fire1");
        isAiming = Input.GetButton("Fire1");
        if (isAiming)
        {
            movementspeed += Aiming_Base_Penalty;
        }
    }


   void Animate()
    {
        if(movementDirection != Vector3.zero)
        {
            anim.SetFloat("Horizontal", movementDirection.x);
            anim.SetFloat("Vertical", movementDirection.y);
            anim.SetFloat("Magnitude", movementDirection.magnitude);
            anim.SetBool("isMoving", true);
        }
        else 
        {
            anim.SetBool("isMoving",false);
        }
       // anim.SetFloat("Speed", movementspeed);
    }
    void Move()
    {
        rb.velocity = movementDirection * movementspeed * Movement_Base_Speed;
    }
    void Aim()
    {
      
      Vector3 mouseWolrdPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWolrdPosition.z =0f;
       crossHair.transform.position = mouseWolrdPosition;
     
    }
    RaycastHit2D hit;
    void Shoot()
    {
        
        Vector2 shootingDirection = crossHair.transform.localPosition;
        shootingDirection.Normalize();
        
        if (endOfAiming)
        {
          
            GameObject Bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bulletScript = Bullet.GetComponent<Bullet>();
            bulletScript.velocity = shootingDirection * Bullit_base_speed;
            bulletScript.player = gameObject;
            Bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(Bullet, 3.0f);
            
            
        }

}
}
