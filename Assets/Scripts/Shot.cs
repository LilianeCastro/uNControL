using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("bound"))
        {
            Destroy(this.gameObject);
        }
    }
}
