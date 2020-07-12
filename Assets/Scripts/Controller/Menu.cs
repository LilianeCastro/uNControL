using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private GameController _GameController;
    public GameObject menuPanel;
    public GameObject inGamePanel;
    public GameObject gameOverPanel;

    private void Start() {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    public void sceneToLoad(string nameScene)
    {
        switch(nameScene)
        {
            case "mainMenu":
                menuPanel.SetActive(true);
                inGamePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case "inGame":
                _GameController.startCoroutinesInGame();
                menuPanel.SetActive(false);
                inGamePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                break;
            case "gameOver":
                menuPanel.SetActive(false);
                inGamePanel.SetActive(false);
                gameOverPanel.SetActive(true);
                break;

        }
        SceneManager.LoadScene(nameScene);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
