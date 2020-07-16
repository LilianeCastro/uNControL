using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    private Player _PlayerA;
    private Player _PlayerB;

    private Menu _Menu;
    private Sound _Sound;

    //private bool purifierCanControl;
    //private bool corrupterCanControl;

    private bool statusGame;
    private int totalPurified;
    private int totalCorrupted;
    private int score;
    private int highscore;

    [Header("Canvas Config")]
    public Text textKeyCodeToChangeSide;
    public KeyCode[] keyToChange;
    public Text textTotalPurified;
    public Text textTotalCorrupted;
    public Text textHighScore;
    public Text gameOverTotalPurified;
    public Text gameOverTotalCorrupted;
    public Text gameOverScore;
    public Text gameOverHighscore;

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

    void Start() {
        _Menu = FindObjectOfType(typeof(Menu)) as Menu;
        _Sound = FindObjectOfType(typeof(Sound)) as Sound;

        textKeyCodeToChange();
        _Sound.changeSong("mainMenu");
    }

    public void startCoroutinesInGame()
    {
        PlayerPrefs.SetInt("highscore", 0);
        zeroScore();
        updateHighScore();
        textKeyCodeToChange();

        StartCoroutine("spawnEnemyToDarkSide");
        StartCoroutine("spawnEnemyToLightSide");

        statusGame = true;
    }

    public void playerIsCreated()
    {
        if(GameObject.Find("PlayerA") != null && GameObject.Find("PlayerB") != null)
        {
            _PlayerA = GameObject.Find("PlayerA").GetComponent<Player>();
            _PlayerB = GameObject.Find("PlayerB").GetComponent<Player>();

            StartCoroutine("unControlSide");
        }
    }

    public bool getStatusGame()
    {
        return statusGame;
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
            gameOverHighscore.text = $"Hi-Score: {PlayerPrefs.GetInt("highscore")}";
            updateHighScore();
        }

        textHighScore.text = $"Hi-Score: {PlayerPrefs.GetInt("highscore")}";
        gameOverHighscore.text = $"Hi-Score: {PlayerPrefs.GetInt("highscore")}";
        gameOverScore.text = score.ToString();
    }

    public void zeroScore()
    {
        totalPurified = 0;
        textTotalPurified.text = "0";
        totalCorrupted = 0;
        textTotalCorrupted.text = "0";
        gameOverTotalPurified.text = "0";
        gameOverTotalCorrupted.text = "0";

        score = 0;
    }

     private void updateHighScore()
    {
        if(PlayerPrefs.GetInt("highscore") == 0)
        {
            textHighScore.text = $"Hi-Score: {0}";
        }
        else
        {
            textHighScore.text = $"Hi-Score: {PlayerPrefs.GetInt("highscore")}";
        }
    }

    public void setTotalTextInCanvas(string nameEnemy)
    {
        if(nameEnemy.Equals("purifier"))
        {
            totalPurified += 1;
            textTotalPurified.text = totalPurified.ToString();
            gameOverTotalPurified.text = totalPurified.ToString();
        }
        else
        {
            totalCorrupted += 1;
            textTotalCorrupted.text = totalCorrupted.ToString();
            gameOverTotalCorrupted.text = totalCorrupted.ToString();
        }
    }

    public void changeScene(string sceneName)
    {
        _Menu.sceneToLoad(sceneName);
    }

    public string getNameCurrentScene()
    {
        return _Menu.nameCurrentScene();
    }

    public KeyCode keyToUseToChangeSide()
    {
        return keyToUse;
    }

    public void changeTextKeyCodeToControl(string keyCodeToShow)
    {
        textKeyCodeToChangeSide.color = Color.green;
        textKeyCodeToChangeSide.text = keyCodeToShow;
    }

    public void textKeyCodeToChange()
    {
        int posKey = Random.Range(0, keyToChange.Length);
        keyToUse = keyToChange[posKey];

        changeTextKeyCodeToControl(keyToUse.ToString());
    }

    public void setFx(int id)
    {
        _Sound.playFx(id);
    }

    public void setDeathFx()
    {
        _Sound.playDeathRandom();
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

    /*void purifierControl(bool state)
    {
        purifierCanControl = state;
    }

    public bool getPurifierCanControl()
    {
        return purifierCanControl;
    }

    void corrupterControl(bool state)
    {
        corrupterCanControl = state;
    }

    public bool getCorrupterControl()
    {
        return corrupterCanControl;
    }*/

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
        Instantiate(prefabToSpawn, new Vector3(Random.Range(minX, maxX), posY), _PlayerA.transform.rotation);
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

        //purifierControl(control);
        //corrupterControl(!control);

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
