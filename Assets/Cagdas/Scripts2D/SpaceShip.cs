using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    public Boolean flyaway = false;
    [SerializeField] Sprite idle, move;
    [SerializeField] GameObject blackout;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flyaway)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = move;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().sprite = idle;
        }

        if(transform.position.x > 230)
        {
            blackout.SetActive(true);
            blackout.GetComponent<Animator>().enabled = true;
        }


    }

}
