using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _Player;
    private Animator enemyAnim;
    private SpriteRenderer enemyRend;

    public string nameGameObjectPlayerToFollow;
    public string tagToCompare;

    public float speed;

    void Start()
    {
        _Player = GameObject.Find(nameGameObjectPlayerToFollow).GetComponent<Player>();

        enemyAnim = GetComponent<Animator>();
        enemyRend = GetComponent<SpriteRenderer>();

        if(this.tag=="purifier")
        {
            speed = GameController.Instance.getSpeedPurifier();
        }
        else
        {
            speed = GameController.Instance.getSpeedCorrupter();
        }
    }

    void Update()
    {
        if(_Player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _Player.transform.position, speed * Time.deltaTime);
            transform.up = _Player.transform.position - transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag(tagToCompare))
        {
            GameController.Instance.setStatusEndGame();
        }

        if(other.CompareTag("shot"))
        {
            GameController.Instance.setDeathFx();
            enemyAnim.SetTrigger("death");

            if(this.tag=="purifier")
            {
                enemyRend.color = Color.cyan;
            }
            else
            {
                enemyRend.color = Color.red;
            }

            _Player.destroyedTheEnemyCalled(this.tag);
            Destroy(this.gameObject, 0.3f);
        }

    }
}
