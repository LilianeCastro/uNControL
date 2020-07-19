using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Canvas Config")]
    public Text textKeyCodeToChangeSide;
    public Text textTotalPurified;
    public Text textTotalCorrupted;
    public Text textHighScore;
    public Text gameOverTotalPurified;
    public Text gameOverTotalCorrupted;
    public Text gameOverScore;
    public Text gameOverHighscore;


    public Button startBtn;
    public Button tutorialBtn;
    public Button returnToMenuBtn;
    public Button restartBtn;
    public Button menuBtn;
    public Button quitBtn;

    public override void Init()
    {
        base.Init();
        startBtn.onClick.AddListener(StartGame);
        tutorialBtn.onClick.AddListener(Tutorial);
        returnToMenuBtn.onClick.AddListener(ReturnToMenu);
        restartBtn.onClick.AddListener(Restart);
        menuBtn.onClick.AddListener(MenuGame);
        quitBtn.onClick.AddListener(Quit);
    }

    void StartGame()
    {
        zeroValuesInGame();
        Menu.Instance.sceneToLoad("inGame");
    }

    void Tutorial()
    {
        Menu.Instance.showTutorial();
    }

    void ReturnToMenu()
    {
        Menu.Instance.closeTutorial();
    }

    void Restart()
    {
        zeroValuesInGame();
        StartGame();
    }

    void MenuGame()
    {
        Menu.Instance.sceneToLoad("mainMenu");
    }

    public void Quit()
    {
        /*if (EditorApplication.isPlaying)
        {
            EditorApplication.ExitPlaymode();
        }*/

        Application.Quit();
    }

    //refactor totals and highscore values
    public void setGameOverHighScore(int value)
    {
        gameOverHighscore.text = $"Hi-Score: {value}";
    }

    public void setHighScore(int value)
    {
        textHighScore.text = $"Hi-Score: {value}";
    }

    public void setGameOverScore(int value)
    {
        gameOverScore.text = value.ToString();
    }

    public void zeroValuesInGame()
    {
        textTotalPurified.text = "0";
        textTotalCorrupted.text = "0";
        gameOverTotalPurified.text = "0";
        gameOverTotalCorrupted.text = "0";
    }

    public void setTotalPurified(int value)
    {
        textTotalPurified.text = value.ToString();
        gameOverTotalPurified.text = value.ToString();
    }

    public void setTotalCorrupted(int value)
    {
        textTotalCorrupted.text = value.ToString();
        gameOverTotalCorrupted.text = value.ToString();
    }

    public void setTextKeyCodeControl(string keyCodeToShow)
    {
        textKeyCodeToChangeSide.color = Color.green;
        textKeyCodeToChangeSide.text = keyCodeToShow;
    }



}
