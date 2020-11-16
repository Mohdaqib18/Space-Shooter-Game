using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameover;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameover == true)
        {
            SceneManager.LoadScene(1);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
               
    }


    public void GameOver()
    {
        _isGameover = true;
   
    }
}
