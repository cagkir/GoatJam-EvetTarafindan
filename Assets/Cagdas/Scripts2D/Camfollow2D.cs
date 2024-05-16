using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfollow2D : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float camup, camdown, camdistance;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;
    Boolean left = false;
    public Boolean cliff = false, cliff2 = false, flyaway = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        left = target.gameObject.GetComponent<Move2D>().left;


        

        

        if (!flyaway)
        {
            if (!cliff)
            {
                if (!left)
                {
                    Vector3 offset = new Vector3(camdistance, camup, -10);
                    Vector3 targetPos = target.position + offset;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
                }
                if (left)
                {
                    Vector3 offset = new Vector3(-camdistance, camup, -10);
                    Vector3 targetPos = target.position + offset;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
                }
            }
            if (cliff)
            {
                if (!left)
                {
                    Vector3 offset = new Vector3(camdistance, camdown, -10);
                    Vector3 targetPos = target.position + offset;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
                }
                if (left)
                {
                    Vector3 offset = new Vector3(-camdistance, camdown, -10);
                    Vector3 targetPos = target.position + offset;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
                }
            }
            if (cliff2)
            {
                if (!left)
                {
                    Vector3 offset = new Vector3(0, 0, -10);
                    Vector3 targetPos = target.position + offset;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
                }
                if (left)
                {
                    Vector3 offset = new Vector3(0, 0, -10);
                    Vector3 targetPos = target.position + offset;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
                }
            }
        }

    }

    private void LateUpdate()
    {
        if (flyaway)
        {
            Vector3 offset = new Vector3(8, 0, -10);
            Vector3 targetPos = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);
        }
    }

}
