using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    private GameController _GameController;
    private Player _Player;

    private float dirX;
    private float dirY;

    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _Player = GetComponent<Player>();
    }

    void Update()
    {
        if(_Player.control)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxisRaw("Vertical");
            _Player.Move(new Vector2(dirX, dirY));
            _Player.Rotate(new Vector2(dirX, dirY));
        }
        else
        {
            _Player.Move(Vector2.zero);
        }

        if(Input.GetKeyUp(_GameController.keyToUseToChangeSide()))
        {
            changeControl();
        }

        if(Input.GetButtonDown("Fire1") && _Player.control)
        {
            _Player.shot();
        }
    }

    public void changeControl()
    {
        _Player.control = !_Player.control;
    }
}
