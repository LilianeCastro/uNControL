using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    void OnEnable() {
        Invoke("Destroy", 2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable() {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("bound") || other.CompareTag("purifier") || other.CompareTag("corrupter"))
        {
            Destroy();
        }
    }
}
