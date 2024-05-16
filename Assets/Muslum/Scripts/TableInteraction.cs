using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteraction : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

       InteractWithTable();

    }

    private void InteractWithTable()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        
        if (hit.collider.gameObject.transform.CompareTag("Table") && Input.GetMouseButton(0))
        {
            rb.useGravity = true;
            Debug.Log("Table Clicked");
        }
    }
}

