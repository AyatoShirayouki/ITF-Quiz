using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

    public void playGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
