using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoSingleton <Menu>
{
    public GameObject menuPanel;
    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public GameObject tutorialPanel;

    private bool isTutorial;

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

        Sound.Instance.changeSong(nameScene);
        SceneManager.LoadScene(nameScene);
    }

    public string nameCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void showTutorial()
    {
        menuPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        isTutorial = true;
    }

    public void closeTutorial()
    {
        menuPanel.SetActive(true);
        tutorialPanel.SetActive(false);
        isTutorial = false;
    }

    public bool getIsTutorial()
    {
        return isTutorial;
    }

}
