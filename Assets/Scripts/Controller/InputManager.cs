using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    void Start()
    {
        StartCoroutine("getKeyValue");
    }

    IEnumerator getKeyValue()
    {
        yield return new WaitForEndOfFrame();

        switch(Menu.Instance.nameCurrentScene())
        {
            case "mainMenu":
                if(Input.GetKey(KeyCode.P))
                {
                    Menu.Instance.sceneToLoad("inGame");
                }
                if(Input.GetKey(KeyCode.T))
                {
                    Menu.Instance.showTutorial();
                }
                if(Input.GetKey(KeyCode.R))
                {
                    Menu.Instance.closeTutorial();
                }
                if(Input.GetKey(KeyCode.E) && !Menu.Instance.getIsTutorial())
                {
                    UIManager.Instance.Quit();
                }
                break;
            case "gameOver":
                if(Input.GetKey(KeyCode.P))
                {
                    Menu.Instance.sceneToLoad("inGame");
                }
                if(Input.GetKey(KeyCode.M))
                {
                    Menu.Instance.sceneToLoad("mainMenu");
                }
                break;
        }
        StartCoroutine("getKeyValue");
    }
}
