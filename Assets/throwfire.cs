using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwfire : MonoBehaviour
{

    int firstthrow;
    int nextthrowwait;
    float sayac = 0, baslangic = 0;
    Boolean saymayabasla = false;
    [SerializeField] GameObject fireball, player;

    // Start is called before the first frame update
    void Start()
    {
        firstthrow = UnityEngine.Random.Range(1, 3);
        nextthrowwait = UnityEngine.Random.Range(4, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if(baslangic >= firstthrow)
        {
            saymayabasla = true;
        }
        if(sayac > nextthrowwait)
        {
            throwfire1();
            sayac = 0;
        }
    }

    private void FixedUpdate()
    {
        if(baslangic < firstthrow)
        {
            baslangic += Time.deltaTime;
        }
        
        if (saymayabasla)
        {
            sayac += Time.deltaTime;
        }
    }

    void throwfire1()
    {
        GameObject fireball1 = Instantiate(fireball);
        fireball1.transform.position = transform.position;
        Vector3 direction_to_model = player.transform.position - fireball1.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction_to_model, Vector3.up);
        fireball1.transform.rotation = rotation;

    }

}
