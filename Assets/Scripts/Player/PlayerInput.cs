using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    private Player _Player;

    private float dirX;
    private float dirY;

    void Start()
    {
        _Player = GetComponent<Player>();
    }

    void Update()
    {
        _Player.arenaControl(_Player.control);

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

        if(Input.GetKeyUp(GameController.Instance.keyToUseToChangeSide()))
        {
            changeControl();
        }

        if(Input.GetButtonDown("Fire1") && _Player.control)
        {
            GameController.Instance.setFx(0);
            _Player.shot();
        }
    }

    public void changeControl()
    {
        _Player.control = !_Player.control;
    }
}
