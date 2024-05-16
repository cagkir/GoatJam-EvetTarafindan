using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openclose : MonoBehaviour
{
    public GameObject normal, glow;
    float sayac;

    private void Awake()
    {
        glow.GetComponent<Animator>().SetBool("glowdown", true);
    }
    public void open()
    {
        glow.GetComponent<Animator>().SetBool("glowdown", false);
    }
    public void close()
    {
        glow.GetComponent<Animator>().SetBool("glowdown", true);
    }
    
}
