using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //Loads Next Scene
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();     //Quits Game when not in UnityEditor
    }
}
