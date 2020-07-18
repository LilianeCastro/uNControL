using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoSingleton<GameController>
{
    public Player _PlayerA;
    public Player _PlayerB;

    private bool statusGame;
    private int totalPurified;
    private int totalCorrupted;
    private int score;
    private int highscore;
    public KeyCode[] keyToChange;

    [Header("Prefabs")]
    public GameObject shotPrefab;
    public GameObject corrupterPrefab;
    public GameObject purifierPrefab;

    [Header("Spawn Purifier")]
    public float minXPurifier;
    public float maxXPurifier;
    public float speedPurifier;

    [Header("Spawn Corrupter")]
    public float minXCorrupter;
    public float maxXCorrupter;
    public float speedCorrupter;

    [Header("Spawn In Arena")]
    public float minYArena;
    public float maxYArena;
    public int scoreToChangeSpeed;
    public float increaseSpeedEnemy;

    private bool control = true;
    private KeyCode keyToUse;

    public override void Init()
    {
        base.Init();

        startCoroutinesInGame();

    }

    public void startCoroutinesInGame()
    {
        //HUD Sound? Clear Data?
        //PlayerPrefs.SetInt("highscore", 0);
        zeroScore();
        updateHighScore();
        textKeyCodeToChange();

        StartCoroutine("spawnEnemyToDarkSide");
        StartCoroutine("spawnEnemyToLightSide");
        StartCoroutine("unControlSide");

        statusGame = true;
    }

    public void setStatusGame(bool newStatus)
    {
        statusGame = newStatus;
    }

    public void setStatusEndGame()
    {
        updateScore();
        setStatusGame(false);
        changeScene("gameOver");

        textKeyCodeToChange();
    }

    void updateScore()
    {
        score = totalCorrupted + totalPurified;
        if(score > PlayerPrefs.GetInt("highscore"))
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
            UIManager.Instance.setGameOverHighScore(highscore);

            updateHighScore();
        }

        UIManager.Instance.setHighScore(PlayerPrefs.GetInt("highscore"));
        UIManager.Instance.setGameOverHighScore(PlayerPrefs.GetInt("highscore"));
        UIManager.Instance.setGameOverScore(score);


    }

    public void zeroScore()
    {
        totalPurified = 0;
        totalCorrupted = 0;
        score = 0;

    }

     private void updateHighScore()
    {
        if(PlayerPrefs.GetInt("highscore") == 0)
        {
            UIManager.Instance.setHighScore(0);
        }
        else
        {
            UIManager.Instance.setHighScore(PlayerPrefs.GetInt("highscore"));
        }
    }

    public void setTotalTextInCanvas(string nameEnemy)
    {
        if(nameEnemy.Equals("purifier"))
        {
            totalPurified += 1;
            UIManager.Instance.setTotalPurified(totalPurified);
        }
        else
        {
            totalCorrupted += 1;
            UIManager.Instance.setTotalCorrupted(totalCorrupted);
        }
    }

    public void changeScene(string sceneName)
    {
        Menu.Instance.sceneToLoad(sceneName);
    }

    public string getNameCurrentScene()
    {
        return Menu.Instance.nameCurrentScene();
    }

    public KeyCode keyToUseToChangeSide()
    {
        return keyToUse;
    }

    public void textKeyCodeToChange()
    {
        int posKey = Random.Range(0, keyToChange.Length);
        keyToUse = keyToChange[posKey];

        UIManager.Instance.setTextKeyCodeControl(keyToUse.ToString());
    }

    public void setFx(int id)
    {
        Sound.Instance.playFx(id);
    }

    public void setDeathFx()
    {
        Sound.Instance.playDeathRandom();
    }

    public bool getUncontrolSide(string playerTag)
    {
        if(playerTag=="purifier")
        {
            return control;
        }
        else
        {
            return !control;
        }
    }

    public float getSpeedPurifier()
    {
        return speedPurifier + (increaseSpeedEnemy * (Mathf.Floor(speedPurifier / scoreToChangeSpeed)));;
    }

    public float getSpeedCorrupter()
    {

        return speedCorrupter + (increaseSpeedEnemy * (Mathf.Floor(totalCorrupted / scoreToChangeSpeed)));
    }

    private void spawnEnemy(float minX, float maxX, GameObject prefabToSpawn)
    {
        if(Random.Range(0, 100) > 50)
        {
            spawnInX(minX, maxX, prefabToSpawn);
        }
        else
        {
            spawnInY(minX, maxX, prefabToSpawn);
        }
    }

    private void spawnInX(float minX, float maxX, GameObject prefabToSpawn)
    {
        float posY = Random.Range(0, 2) == 0 ? minYArena : maxYArena;
        Instantiate(prefabToSpawn, new Vector3(Random.Range(minX, maxX), posY), transform.rotation);
    }

    private void spawnInY(float minX, float maxX, GameObject prefabToSpawn)
    {
        float posX = Random.Range(0, 2) == 0 ? minX : maxX;
        Instantiate(prefabToSpawn, new Vector3(posX, Random.Range(minYArena, maxYArena)), transform.rotation);
    }


    IEnumerator unControlSide()
    {
        float timeToChangeControl = Random.Range(3, 10);
        yield return new WaitForSeconds(timeToChangeControl);

        control = !control;

        _PlayerA.control = control;
        _PlayerB.control = !control;

        StartCoroutine("unControlSide");
    }

    IEnumerator spawnEnemyToDarkSide()
    {
        yield return new WaitForSeconds(Random.Range(3, 10));
        if(statusGame)
        {
            spawnEnemy(minXPurifier, maxXPurifier, purifierPrefab);
            StartCoroutine("spawnEnemyToDarkSide");
        }
    }

    IEnumerator spawnEnemyToLightSide()
    {
        yield return new WaitForSeconds(Random.Range(3, 10));
        if(statusGame)
        {
            spawnEnemy(minXCorrupter, maxXCorrupter, corrupterPrefab);
            StartCoroutine("spawnEnemyToLightSide");
        }
    }

}
