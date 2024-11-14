using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("sample");
        SceneManager.LoadScene("SampleScene");
      
    }
    public void StartTestScene()
    {
        Debug.Log("Demo");
        SceneManager.LoadScene("DemoScene");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
