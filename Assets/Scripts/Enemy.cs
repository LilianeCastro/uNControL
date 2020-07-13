using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _Player;

    public string nameGameObjectPlayerToFollow;
    public string tagToCompare;

    public float speed;

    void Start()
    {
        _Player = GameObject.Find(nameGameObjectPlayerToFollow).GetComponent<Player>();
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
            _Player.playerDied();
            Destroy(other.gameObject);
        }

        if(other.CompareTag("shot"))
        {
            _Player.destroyedTheEnemyCalled(this.tag);
            Destroy(this.gameObject);
        }

    }
}
