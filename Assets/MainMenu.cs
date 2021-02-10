using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public int width;
    public int height;
    public bool setFullScreen;
    private void Start() {
        
    }

    public void PlayGame(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void QuitGame(){

        Debug.Log("Quit!");
        Application.Quit();
    }
    public void BackToMenu(){
        SceneManager.LoadScene(0);
    }
    public void Restart(){
        SceneManager.LoadScene(1);
    }
    public void SetResolution(){
        Screen.SetResolution(width, height, setFullScreen);
        Debug.Log(""+ width + "x" + height + " FullScreen: " + setFullScreen);
        
    }

    public void PauseGame(){
        Time.timeScale = 0;
    }
    public void ResumeGame(){
        Time.timeScale = 1;
    }
}
