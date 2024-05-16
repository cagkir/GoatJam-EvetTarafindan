using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    float rotationX = 0;
    float rotationY = 0;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        rotationY = Mathf.Clamp(rotationY, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
    private void FixedUpdate()
    {
        
    }
}
