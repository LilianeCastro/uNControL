using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
    private Animator sideAnim;

    void Start()
    {
        sideAnim = GetComponent<Animator>();
    }

    public void setSideAnim(bool status)
    {
        sideAnim.SetBool("isControl", status);
    }

}
