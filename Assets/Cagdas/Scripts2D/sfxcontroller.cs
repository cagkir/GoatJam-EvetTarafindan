using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxcontroller : MonoBehaviour
{

    [SerializeField] AudioClip jump, coin, hurt, success;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.Stop();
    }

    public void jumped()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }
    public void gotcoin()
    {
        audioSource.clip = coin;
        audioSource.Play();
    }
    public void gothurt()
    {
        audioSource.clip= hurt;
        audioSource.Play();
    }
    public void successed()
    {
        audioSource.clip = success;
        audioSource.Play();
    }
}
