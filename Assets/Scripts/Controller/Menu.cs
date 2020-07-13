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
    public GameObject tutorialPanel;


    private void Start() {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        StartCoroutine("getKeyValue");
    }

    public void sceneToLoad(string nameScene)
    {
        switch(nameScene)
        {
            case "mainMenu":
                StartCoroutine("getKeyValue");
                menuPanel.SetActive(true);
                inGamePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case "inGame":
                StopCoroutine("getKeyValue");
                _GameController.startCoroutinesInGame();
                menuPanel.SetActive(false);
                inGamePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                break;
            case "gameOver":
                StartCoroutine("getKeyValue");
                menuPanel.SetActive(false);
                inGamePanel.SetActive(false);
                gameOverPanel.SetActive(true);
                break;
        }
        SceneManager.LoadScene(nameScene);
    }

    public string nameCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void exitGame()
    {
        Application.Quit();
    }

    public void showTutorial()
    {
        menuPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    public void closeTutorial()
    {
        menuPanel.SetActive(true);
        tutorialPanel.SetActive(false);
    }

    IEnumerator getKeyValue()
    {
        yield return new WaitForEndOfFrame();

        switch(nameCurrentScene())
        {
            case "mainMenu":
                if(Input.GetKey(KeyCode.P))
                {
                    sceneToLoad("inGame");
                }
                if(Input.GetKey(KeyCode.T))
                {
                    showTutorial();
                }
                if(Input.GetKey(KeyCode.R))
                {
                    closeTutorial();
                }
                if(Input.GetKey(KeyCode.E))
                {
                    exitGame();
                }
                break;
            case "gameOver":
                if(Input.GetKey(KeyCode.P))
                {
                    sceneToLoad("inGame");
                }
                if(Input.GetKey(KeyCode.M))
                {
                    sceneToLoad("mainMenu");
                }
                break;
        }
        StartCoroutine("getKeyValue");
    }

}
