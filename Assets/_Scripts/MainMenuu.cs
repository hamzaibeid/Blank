using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuu : MonoBehaviour
{   

          
    
    public void PlayGame()
    {   
       
       GameObject varGameObject = GameObject.FindWithTag("Cam2");
      GameObject.FindWithTag("Cam").SetActive(false);
      varGameObject.GetComponent<EnemySpawner>().enabled = true;
      Cursor.visible=false;
    }
    

    public void Exit()
    {
           Application.Quit();
    }

}
