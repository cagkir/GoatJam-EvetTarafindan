using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenumanager : MonoBehaviour
{
    public GameObject oynabuton, ayarlarbuton, cýkýsbuton, muzikackapa, sensayarý, geritusu, muzikbut, sensbut, oyunismi, buton1im, buton2im, buton3im, geriim;
    public TextMeshProUGUI textMeshPro;
    public int sens, musicint;
    public Boolean music;

    private void Awake()
    {
        //mainbuttons
        oynabuton.SetActive(true);
        ayarlarbuton.SetActive(true);
        cýkýsbuton.SetActive(true);
        oyunismi.SetActive(true);

        //ayarlarbutonlarý
        //muzikackapa.SetActive(false);
        //sensayarý.SetActive(false);
        geritusu.SetActive(false);
        muzikbut.SetActive(false);
        sensbut.SetActive(false);
    }
    private void Update()
    {
       
        sens = (int)sensayarý.GetComponent<Slider>().value;
        textMeshPro.text = sens.ToString();
        PlayerPrefs.SetInt("sens", sens);
        music = muzikackapa.GetComponent<Toggle>().isOn;
        if (music)
        {
            musicint = 1;
            PlayerPrefs.SetInt("music", musicint);
        }
        if (!music)
        {
            musicint = 0;
            PlayerPrefs.SetInt("music", musicint);
        }
    }
    public void startgame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
    public void ayarlarmenu()
    {
        //mainbuttons
        oynabuton.SetActive(false);
        ayarlarbuton.SetActive(false);
        cýkýsbuton.SetActive(false);
        oyunismi.SetActive(false);

        //ayarlarbutonlarý
        //muzikackapa.SetActive(true);
        //sensayarý.SetActive(true);
        geritusu.SetActive(true);
        muzikbut.SetActive(true);
        sensbut.SetActive(true);
        geriim.GetComponent<Animator>().SetBool("glowdown", true);
    }
    public void cýkýs()
    {
        Application.Quit();
    }
    public void ayarlarmenukapat()
    {
        //mainbuttons
        oynabuton.SetActive(true);
        buton1im.GetComponent<Animator>().SetBool("glowdown", true);
        ayarlarbuton.SetActive(true);
        buton2im.GetComponent<Animator>().SetBool("glowdown", true);
        cýkýsbuton.SetActive(true);
        buton3im.GetComponent<Animator>().SetBool("glowdown", true);
        oyunismi.SetActive(true);

        //ayarlarbutonlarý
        //muzikackapa.SetActive(false);
        //sensayarý.SetActive(false);
        geritusu.SetActive(false);
        muzikbut.SetActive(false);
        sensbut.SetActive(false);
    }
}
