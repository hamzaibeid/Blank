using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
   public GameObject player ;
   
  private void Start()
    {
        Transform bar = transform.Find("Bar");
        
    }
    public void SetSize(float sizeNormalized){
        bar.localScale = new Vector3(sizeNormalized, 1f ,1f);

    }
 void Update (){
    

 }

   
  
}
