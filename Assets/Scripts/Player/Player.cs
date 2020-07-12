using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D playerRb;

    private float speed = 2f;
    private float posX;
    private float posY;
    private float rotateAngle;
    private float x;
    private float y;
    private float speedShot;

    [Header("PlayerConfig")]
    public Transform posLookAt;
    public bool control;
    public string playerName;

    [Header("LimitConfig")]
    public Vector2 lefDiagonalLimit;
    public Vector2 rightDiagonalLimit;

    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        playerRb = GetComponent<Rigidbody2D>();
        speedShot = 5f;
    }

    public void playerDied()
    {
        _GameController.setStatusEndGame();
    }

    public void destroyedTheEnemyCalled(string enemyTag)
    {
        _GameController.setTotalTextInCanvas(enemyTag);
    }

    public void Move(Vector2 direction)
    {
        playerRb.velocity = direction * speed;

        posX = transform.position.x;
        posY = transform.position.y;

        if(transform.position.x < lefDiagonalLimit.x)
        {
            posX = lefDiagonalLimit.x;
        }
        else if(transform.position.x > rightDiagonalLimit.x)
        {
            posX = rightDiagonalLimit.x;
        }
        if(transform.position.y < lefDiagonalLimit.y)
        {
            posY = lefDiagonalLimit.y;
        }
        else if(transform.position.y > rightDiagonalLimit.y)
        {
            posY = rightDiagonalLimit.y;
        }

        transform.position = new Vector2(posX, posY);
    }

    public void Rotate(Vector2 direction)
    {

        if(direction.x == 0 && direction.y == 1)
        {
            rotateAngle = 0f;
            x = 0;
            y = 1;
        }

        if(direction.x == 1 && direction.y == 1)
        {
            rotateAngle = -45f;
            x = 1;
            y = 1;
        }

        if(direction.x == 1 && direction.y ==0)
        {
            rotateAngle = -90f;
            x = 1;
            y = 0;
        }

        if(direction.x == 1 && direction.y == -1)
        {
            rotateAngle = -135f;
            x = 1;
            y = -1;
        }

        if(direction.x == 0 && direction.y == -1)
        {
            rotateAngle = -180f;
            x = 0;
            y = -1;
        }

        if(direction.x == -1 && direction.y == -1)
        {
            rotateAngle = -225f;
            x = -1;
            y = -1;
        }

        if(direction.x == -1 && direction.y == 0)
        {
            rotateAngle = -270f;
            x = -1;
            y = 0;
        }

        if(direction.x == -1 && direction.y == 1)
        {
            rotateAngle = -315f;
            x = -1;
            y = 1;
        }

        transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y, rotateAngle);
    }

    public void setControl(bool stateControl)
    {
        control = stateControl;
    }

    public void shot()
    {
        GameObject shotTemp = Instantiate(_GameController.shotPrefab, posLookAt.position, posLookAt.rotation);

        if(x == 0 && y == 0)
        {
            shotTemp.GetComponent<Rigidbody2D>().AddForce(vectorToRotate(), ForceMode2D.Impulse);
        }
        else
        {
            shotTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(x * speedShot, y * speedShot), ForceMode2D.Impulse);
        }
    }

    private Vector2 vectorToRotate()
    {
        int tempX;
        int tempY;

        do{
            tempX  = Random.Range(-1, 2);
            tempY = Random.Range(-1, 2);
        }while(tempX == 0 && tempY ==0);

        return new Vector2(tempX * speedShot, tempY *speedShot);
    }

    IEnumerator getUncontrolSideInGameController()
    {
        yield return new WaitForSeconds(5f);
        if(this.tag=="purifier")
        {
            //control = _GameController.getControl();
        }
        else
        {
            //control = !_GameController.getControl();
        }
        //control = _GameController.getUncontrolSide(this.tag);
        StartCoroutine("getUncontrolSideInGameController");
    }

}
