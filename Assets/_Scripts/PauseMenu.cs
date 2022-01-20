using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject Bullet;
    public GameObject Bullet2;
    public GameObject MainMenu;
    public static bool gameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }
    public void Resume()
    {
          pauseMenuUI.SetActive(false);
          Time.timeScale = 1f;
          gameIsPaused = false;
          Cursor.visible=false;
          Bullet.SetActive(true);
    }
    void Pause()
    {
          pauseMenuUI.SetActive(true);
          Time.timeScale = 0f;
          gameIsPaused = true;
          Cursor.visible=true;
          Bullet.SetActive(false);
    }

    public void LoadMenu()
    {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
             Time.timeScale = 1f;
    }
    public void Quit()
    {
         Application.Quit();
    }
}

